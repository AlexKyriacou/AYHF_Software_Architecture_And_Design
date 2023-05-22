import React from "react";
import { Link } from "react-router-dom";
import OrderSummary from "../cart/OrderSummary";
import "./Checkout.css";

function PaymentPage() {
    return (
        <div className="checkout-container">
            <h2>Payment</h2>
            <OrderSummary extra={<div>
                <Link className="next" to="">Pay Now</Link>
            </div>} />
        </div>
    );
}

export default PaymentPage;
