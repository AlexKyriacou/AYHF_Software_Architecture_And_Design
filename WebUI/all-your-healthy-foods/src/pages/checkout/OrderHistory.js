import React, { useContext, useEffect } from "react";
import { UserContext } from "../../AppContext";
import { useNavigate } from "react-router-dom";

const OrderHistory = () => {
    const { loggedIn } = useContext(UserContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!loggedIn) {
            navigate("/login", { replace: true });
        }
    }, [loggedIn, navigate]);

    return (
        <div>
            <p>These are your orders</p>
        </div>
    );
};

export default OrderHistory;
