import React, { useContext } from "react";
import { Link } from "react-router-dom";
import OrderSummary from "../cart/OrderSummary";
import { UserContext } from "../../AppContext";
import './Checkout.css';

function CheckoutPage() {
    const { loggedIn } = useContext(UserContext);
    return (
        <div className="checkout-container">
            <h2>Checkout</h2>
            {!loggedIn && (
                <div>
                    <p>You need to login or signup to place an order</p>
                    <Link className="link-button" to={`/login?from=checkout`}>
                        Already a member? Login
                    </Link>
                    <p>OR</p>
                    <Link className="link-button" to={`/signup?from=checkout`}>
                        Not A member? Sign Up Now!
                    </Link>
                </div>
            )}
            <OrderSummary extra={<div>
                <Link
                    className={`link-button ${loggedIn ? "" : "disabled"}`}
                    to="/shipping"
                >
                    Go to Shipping
                </Link>
            </div>} />
        </div>
    );
}

export default CheckoutPage;
