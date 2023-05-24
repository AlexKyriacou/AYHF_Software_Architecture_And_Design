import React from "react";
import OrderSummary from "../cart/OrderSummary";
import './Checkout.css';

function OrderConfirmationPage() {
    return (
        <div className="checkout-container">
            <h2>Order Placed</h2>
            <OrderSummary />
        </div>
    );
}

export default OrderConfirmationPage;
