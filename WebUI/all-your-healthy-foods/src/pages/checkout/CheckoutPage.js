import React from 'react';
import { Link } from "react-router-dom";
import CartSummary from '../cart/CartSummary';
import './Checkout.css';

function CheckoutPage() {
    return (
        <div className="checkout-container">
            <h2>Checkout</h2>
            <Link className='switch' to="/login">Already a member? Login</Link> | <Link className='switch' to='/signup'>Not A member? Sign Up Now!</Link>
            <CartSummary extra={<div>
                <Link className="next" to="/shipping">Go to Shipping</Link>
            </div>} />
        </div>
    );
}

export default CheckoutPage;
