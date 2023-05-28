import React, { useContext, useState } from "react";
import { Link } from "react-router-dom";
import OrderSummary from "../order/OrderSummary";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLock } from "@fortawesome/free-solid-svg-icons";
import { faCcMastercard, faPaypal } from "@fortawesome/free-brands-svg-icons";
import { CartContext, UserContext } from "../../AppContext";
import { creditCardPaymentData, paypalPaymentData } from "../../testData/paymentData"
import TextInputWithValidation from "../../components/TextInputWithValidation"
import PasswordInput from "../../components/PasswordInput"
import './Checkout.css';

function validateForm(paymentMethod, paymentDetails) {
    if (paymentMethod === "paypal") {
        if (!paymentDetails.email || !paymentDetails.password) {
            return "Please provide email and password for PayPal.";
        }

        const isValid = paypalPaymentData.some(
            ({ email, password }) =>
                paymentDetails.email === email && paymentDetails.password === password
        );

        if (!isValid) {
            return "Invalid Email Or Password, please try again.";
        }
    } else if (paymentMethod === "creditcard") {
        if (!paymentDetails.cardHolderName || !paymentDetails.cardNumber || !paymentDetails.cvv) {
            return "Please provide all required information for Credit Card.";
        }

        const isValid = creditCardPaymentData.some(
            ({ cardHolderName: dbCardHolderName, cardNumber: dbCardNumber, cvv: dbCvv }) =>
                paymentDetails.cardHolderName === dbCardHolderName && paymentDetails.cardNumber === dbCardNumber && paymentDetails.cvv === dbCvv
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
    const { clearCart, placeOrder } = useContext(CartContext);

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [cardHolderName, setCardHolderName] = useState("");
    const [cardNumber, setCardNumber] = useState("");
    const [cvv, setCVV] = useState("");
    const [paymentMethod, setPaymentMethod] = useState(""); // State to track the selected payment method
    const [formErrorMessage, setFormErrorMessage] = useState("");

    const [orderDetails, setOrderDetails] = useState({});

    const clearInputs = () => {
        setEmail("");
        setPassword("");
        setCardHolderName("");
        setCardNumber("");
        setCVV("");
    }

    const handlePaymentMethodChange = (event) => {
        clearInputs();
        setFormErrorMessage("");
        setPaymentMethod(event.target.value);
    };

    const renderPaymentForm = () => {
        if (paymentMethod === "paypal") {
            return (
                <div className="input-group">
                    <TextInputWithValidation
                        placeholder="Email"
                        regex={/^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/}
                        regexErrorMsg="Invalid Email"
                        value={email}
                        parentOnChange={setEmail}
                    />
                    <PasswordInput
                        placeholder="Password"
                        value={password}
                        onChange={setPassword}
                        checkPattern={false}
                    />
                </div>
            );
        } else if (paymentMethod === "creditcard") {
            return (
                <div className="input-group">
                    <TextInputWithValidation
                        placeholder="Card Holder Name"
                        regex={/^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$/}
                        regexErrorMsg="Invalid Character"
                        value={cardHolderName}
                        parentOnChange={setCardHolderName}
                    />
                    <TextInputWithValidation
                        placeholder="Card Number"
                        regex={/^5[1-5][0-9]{14}|^(222[1-9]|22[3-9]\\d|2[3-6]\\d{2}|27[0-1]\\d|2720)[0-9]{12}$/}
                        regexErrorMsg="Invalid Mastercard Number"
                        value={cardNumber}
                        parentOnChange={setCardNumber}
                    />
                    <TextInputWithValidation
                        placeholder="CVV"
                        regex={/^\d{3}$/}
                        regexErrorMsg="CVV Must be exactly 3 digits long"
                        value={cvv}
                        parentOnChange={setCVV}
                    />
                </div>
            );
        } else {
            return null;
        }
    };

    const handleLinkClick = (event) => {
        const paymentDetails = {
            email: email,
            password: password,
            cardHolderName: cardHolderName,
            cardNumber: cardNumber,
            cvv: cvv
        }
        const errorMessage = validateForm(paymentMethod, paymentDetails);

        if (errorMessage) {
            event.preventDefault();
            setFormErrorMessage(errorMessage);
        } else {
            setFormErrorMessage("");
            clearCart();
            placeOrder(orderDetails);
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
            {formErrorMessage && (<span className="error-message">{formErrorMessage}</span>)}
            <OrderSummary
                extra={
                    <div>
                        <Link
                            className={`link-button ${(loggedIn && paymentMethod) ? "" : "disabled"}`}
                            onClick={handleLinkClick}
                            to="/order-confirmation"
                        >
                            <FontAwesomeIcon icon={faLock} aria-hidden="true" /> Pay Securely
                        </Link>
                    </div>
                }
                shipping={10}
                setOrderDetails={setOrderDetails}
            />
        </div>
    );
}

export default PaymentPage;
