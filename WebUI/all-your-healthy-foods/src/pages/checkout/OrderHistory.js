import React, {useContext} from "react";
import {UserContext} from "../../AppContext";
import {Navigate} from "react-router-dom";

const OrderHistory = () => {
    const {loggedIn} = useContext(UserContext);

    return (
        <div>
            {!loggedIn && (
                <Navigate to="/login" replace={true}/>
            )}
            <p>These are your orders</p>
        </div>
    );
};

export default OrderHistory;