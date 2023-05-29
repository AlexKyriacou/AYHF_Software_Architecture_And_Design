import React from "react";
import '../checkout/Checkout.css';
import { Link, useParams } from "react-router-dom";

function OrderConfirmationPage() {
    const { orderId } = useParams();

    return (
        <div className="checkout-container">
            <h2>Order Placed - #Order {orderId}</h2>
            <p>To view the progress of this order, please go to <span>
                <strong><Link to="/order-history">
                    My Orders
                </Link></strong>
            </span> page.</p>
        </div>
    );
}

export default OrderConfirmationPage;
