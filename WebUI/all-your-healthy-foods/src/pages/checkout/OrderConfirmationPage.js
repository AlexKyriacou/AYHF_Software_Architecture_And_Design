import React from "react";
import './Checkout.css';
import { Link } from "react-router-dom";

function OrderConfirmationPage() {
    return (
        <div className="checkout-container">
            <h2>Order Placed</h2>
            <p>To view the progress of this order, please go to <span>
                <strong><Link to="/order-history">
                    My Orders
                </Link></strong>
            </span> page.</p>
        </div>
    );
}

export default OrderConfirmationPage;
