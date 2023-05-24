import React, { useContext } from "react";
import { useParams } from "react-router-dom";
import { UserContext } from "../../AppContext";
import { Navigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";
import userData from "../../testData/userData"; //TO DELETE ONCE WE ESTABLISH BACKEND CONNECTION

const AccountPage = () => {
    const { username } = useParams();
    const user = userData.find(user => user.username === username);
    const { loggedIn, logout } = useContext(UserContext);

    return (
        <div>
            {!loggedIn && (
                <Navigate to="/login" replace={true} />
            )}
            <h1>Welcome {user.name}</h1>
            <button className="primary-button" onClick={logout}>
                <FontAwesomeIcon className="logout-icon" icon={faSignOutAlt} />&nbsp;Logout
            </button>
        </div>
    );
};

export default AccountPage;