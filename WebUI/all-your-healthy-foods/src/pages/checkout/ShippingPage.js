import React from 'react';
import { Link } from "react-router-dom";
import CartSummary from '../cart/CartSummary';
import './Checkout.css';

function ShippingPage() {
    return (
        <div className="checkout-container">
            <h2>Shipping</h2>
            <CartSummary extra={<div>
                <Link className="next" to="/payment">Go to Payment</Link>
            </div>} />
        </div>
    );
}

export default ShippingPage;
