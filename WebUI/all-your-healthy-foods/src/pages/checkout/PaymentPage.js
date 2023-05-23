import React, { useContext, useState } from "react";
import { Link } from "react-router-dom";
import OrderSummary from "../cart/OrderSummary";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLock } from "@fortawesome/free-solid-svg-icons";
import { faPaypal, faCcMastercard } from "@fortawesome/free-brands-svg-icons";
import { UserContext } from "../../AppContext";
import { paypalPaymentData, creditCardPaymentData } from "../../testData/paymentData"
import './Checkout.css';

function clearInputs() {
    const inputs = [document.getElementById("email"),
    document.getElementById("password"),
    document.getElementById("cardHolderName"),
    document.getElementById("cardNumber"),
    document.getElementById("cvv")];

    inputs.forEach(element => {
        if (element) {
            element.value = "";
        }
    });

}

function validateForm(paymentMethod) {
    if (paymentMethod === "paypal") {
        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;

        if (!email || !password) {
            return "Please provide email and password for PayPal.";
        }

        const isValid = paypalPaymentData.some(
            ({ email: dbEmail, password: dbPassword }) =>
                email === dbEmail && password === dbPassword
        );

        if (!isValid) {
            return "Invalid Email Or Password, please try again.";
        }
    } else if (paymentMethod === "creditcard") {
        const cardHolderName = document.getElementById("cardHolderName").value;
        const cardNumber = document.getElementById("cardNumber").value;
        const cvv = document.getElementById("cvv").value;

        if (!cardHolderName || !cardNumber || !cvv) {
            return "Please provide all required information for Credit Card.";
        }

        const isValid = creditCardPaymentData.some(
            ({ cardHolderName: dbCardHolderName, cardNumber: dbCardNumber, cvv: dbCvv }) =>
                cardHolderName === dbCardHolderName && cardNumber === dbCardNumber && cvv === dbCvv
        );

        if (!isValid) {
            return "Invalid Credit Card information, please try again.";
        }
    } else {
        return "Please select a payment method.";
    }

    return ""; // Return empty string if form is valid
}

function PaymentPage() {
    const { loggedIn } = useContext(UserContext);
    const [paymentMethod, setPaymentMethod] = useState(""); // State to track the selected payment method
    const [formErrorMessage, setFormErrorMessage] = useState("");

    const handlePaymentMethodChange = (event) => {
        clearInputs();
        setFormErrorMessage("");
        setPaymentMethod(event.target.value);
    };

    const renderPaymentForm = () => {
        if (paymentMethod === "paypal") {
            return (
                <div className="input-group">
                    <input className="form-input" placeholder="Email" id="email" type="email" name="email" />
                    <input className="form-input" placeholder="Password" id="password" type="password" name="password" />
                </div>
            );
        } else if (paymentMethod === "creditcard") {
            return (
                <div className="input-group">
                    <input className="form-input" placeholder="Card Holder Name" id="cardHolderName" type="text" name="cardHolderName" />
                    <input className="form-input" placeholder="Card Number" id="cardNumber" type="text" name="cardNumber" />
                    <input className="form-input" placeholder="CVV" id="cvv" type="text" name="cvv" />
                </div>
            );
        } else {
            return null;
        }
    };

    const handleLinkClick = () => {
        const errorMessage = validateForm(paymentMethod);

        if (errorMessage) {
            setFormErrorMessage(errorMessage);
        } else {
            setFormErrorMessage("");
        }
    };

    return (
        <div className="checkout-container">
            <h2>Payment</h2>
            <h3>Please Choose a Payment Method</h3>
            <form className="payment-options">
                <div>
                    <input
                        type="radio"
                        id="paypal"
                        name="paymentMethod"
                        value="paypal"
                        checked={paymentMethod === "paypal"}
                        onChange={handlePaymentMethodChange}
                    />
                    <label htmlFor="paypal"><FontAwesomeIcon icon={faPaypal} aria-hidden="true" /> PayPal</label>
                </div>
                <div>
                    <input
                        type="radio"
                        id="creditcard"
                        name="paymentMethod"
                        value="creditcard"
                        checked={paymentMethod === "creditcard"}
                        onChange={handlePaymentMethodChange}
                    />
                    <label htmlFor="creditcard"><FontAwesomeIcon icon={faCcMastercard} aria-hidden="true" /> Credit Card</label>
                </div>
            </form>
            {renderPaymentForm()}
            {formErrorMessage && (<span className="error">{formErrorMessage}</span>)}
            <OrderSummary
                extra={
                    <div>
                        <Link
                            className={`link-button ${(loggedIn && paymentMethod) ? "" : "disabled"}`}
                            onClick={handleLinkClick}
                        >
                            <FontAwesomeIcon icon={faLock} aria-hidden="true" /> Pay Securely
                        </Link>
                    </div>
                }
            />
        </div>
    );
}

export default PaymentPage;
