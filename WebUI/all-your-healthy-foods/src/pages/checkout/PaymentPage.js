import React, {useContext, useState} from "react";
import {useLocation, useNavigate} from "react-router-dom";
import OrderSummary from "../order/OrderSummary";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLock} from "@fortawesome/free-solid-svg-icons";
import {faCcMastercard, faPaypal} from "@fortawesome/free-brands-svg-icons";
import {CartContext, UserContext} from "../../AppContext";
import {creditCardPaymentData, paypalPaymentData} from "../../testData/paymentData"
import TextInputWithValidation from "../../components/TextInputWithValidation"
import PasswordInput from "../../components/PasswordInput"
import axios from "axios";
import './Checkout.css';

/**
* Validates the payment form based on selected payment method.
* 
* @param {string} paymentMethod - Selected payment method.
* @param {Object} paymentDetails - An object containing payment details.
* @returns {string} An error message, if any.
*/
function validateForm(paymentMethod, paymentDetails) {
    if (paymentMethod === "paypal") {
        if (!paymentDetails.email || !paymentDetails.password) {
            return "Please provide email and password for PayPal.";
        }

        const isValid = paypalPaymentData.some(
            ({email, password}) =>
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
            ({cardHolderName: dbCardHolderName, cardNumber: dbCardNumber, cvv: dbCvv}) =>
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

/**
 * Renders the payment page with payment form based on selected payment method.
 * @returns {JSX.Element} The rendered PaymentPage component.
 */
function PaymentPage() {
    const {loggedIn, user} = useContext(UserContext);
    const {clearCart, cartItems} = useContext(CartContext);

    const location = useLocation();
    const shipping = location.state;

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [cardHolderName, setCardHolderName] = useState("");
    const [cardNumber, setCardNumber] = useState("");
    const [cvv, setCVV] = useState("");
    const [paymentMethod, setPaymentMethod] = useState(""); // State to track the selected payment method
    const [formErrorMessage, setFormErrorMessage] = useState("");

    const navigate = useNavigate();

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

    const handleLinkClick = async (event) => {
        event.preventDefault();

        const paymentDetails = {
            email: email,
            password: password,
            cardHolderName: cardHolderName,
            cardNumber: cardNumber,
            cvv: cvv
        };

        const errorMessage = validateForm(paymentMethod, paymentDetails);

        if (errorMessage) {
            setFormErrorMessage(errorMessage);
        } else {
            setFormErrorMessage("");

            try {
                const response = await axios.post("https://localhost:7269/orders", {
                    userId: user.id,
                    products: cartItems
                });

                clearCart();
                navigate(`/order-confirmation/${response.data.id}`);
            } catch (error) {
                console.error("Error occurred while placing the order:", error);
            }
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
                    <label htmlFor="paypal"><FontAwesomeIcon icon={faPaypal} aria-hidden="true"/> PayPal</label>
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
                    <label htmlFor="creditcard"><FontAwesomeIcon icon={faCcMastercard} aria-hidden="true"/> Credit Card</label>
                </div>
            </form>
            {renderPaymentForm()}
            {formErrorMessage && (<span className="error-message">{formErrorMessage}</span>)}
            <OrderSummary
                step="payment"
                extra={
                    <div>
                        <button
                            className={`secondary-button ${(loggedIn && paymentMethod) ? "" : "disabled"}`}
                            onClick={handleLinkClick}
                        >
                            <FontAwesomeIcon icon={faLock} aria-hidden="true"/> Pay Securely
                        </button>
                    </div>
                }
                shipping={shipping}
            />
        </div>
    );
}

export default PaymentPage;
