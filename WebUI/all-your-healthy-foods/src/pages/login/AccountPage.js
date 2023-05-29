import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { UserContext } from "../../AppContext";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";
import "./Login.css";

const AccountPage = () => {
    const { user, loggedIn, logout } = useContext(UserContext);
    const navigate = useNavigate();

    if (!loggedIn) {
        navigate("/login", { replace: true });
        return null;
    }

    const handleLogout = () => {
        logout();
        navigate("/"); // Redirect to home page after logout
    };

    return (
        <div>
            <h1>Welcome {user.name}</h1>
            <div className="manage-account-buttons">
                <Link className="link-button" to="/order-history">
                    My Orders
                </Link>
            </div>
            <button className="primary-button" onClick={handleLogout}>
                <FontAwesomeIcon className="logout-icon" icon={faSignOutAlt} /> Logout
            </button>
        </div>
    );
};

export default AccountPage;
