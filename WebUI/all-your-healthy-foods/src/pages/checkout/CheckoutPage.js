import React from 'react';
import { Link } from "react-router-dom";
import OrderSummary from '../cart/OrderSummary';
import './Checkout.css';

function CheckoutPage({ isLoggedIn }) {
    isLoggedIn = false;
    return (
        <div className="checkout-container">
            <h2>Checkout</h2>
            <Link className='switch' to="/login">Already a member? Login</Link> | <Link className='switch' to='/signup'>Not A member? Sign Up Now!</Link>
            <OrderSummary extra={<div>
                <Link
                    className={`next ${isLoggedIn ? '' : 'disabled'}`}
                    to="/shipping"
                >
                    Go to Shipping
                </Link>
            </div>} />
        </div>
    );
}

export default CheckoutPage;
