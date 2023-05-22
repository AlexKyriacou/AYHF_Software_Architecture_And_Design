import React from "react";
import { Link } from "react-router-dom";
import OrderSummary from "../cart/OrderSummary";
import "./Checkout.css";

function ShippingPage() {
    return (
        <div className="checkout-container">
            <h2>Shipping</h2>
            <OrderSummary extra={<div>
                <Link className="next" to="/payment">Go to Payment</Link>
            </div>} />
        </div>
    );
}

export default ShippingPage;
