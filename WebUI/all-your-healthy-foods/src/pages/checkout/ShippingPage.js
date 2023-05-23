import React, { useState, useContext } from "react";
import { Link } from "react-router-dom";
import OrderSummary from "../cart/OrderSummary";
import { UserContext } from "../../AppContext";
import './Checkout.css';

function ShippingPage() {
    const { loggedIn } = useContext(UserContext);
    const [postcode, setPostcode] = useState("");
    const [selectedState, setSelectedState] = useState("");
    const [isPostcodeValid, setIsPostcodeValid] = useState(true);
    const [formErrorMessage, setFormErrorMessage] = useState("");

    const validatePostcode = (postcode, state) => {
        const postcodeRanges = {
            ACT: /^(02[0-9]{2}|26[0-1][0-9]|29[0-2][0-9])$/, // 0200-0299, 2600-2619, 2900-2920
            NSW: /^(1[0-9]{3}|2[0-5][0-9]{2}|26[1-8][0-9]|29[2-9][0-9])$/, // 1000-1999, 2000-2599, 2619-2898, 2921-2999
            NT: /^(08[0-9]{2})$/, // 0800-0999
            QLD: /^(4[0-9]{3}|9[0-9]{3})$/, // 4000-4999, 9000-9999
            SA: /^(5[0-7][0-9]{2}|58[0-9]{2})$/, // 5000-5799, 5800-5999
            TAS: /^(7[0-7][0-9]{2}|78[0-9]{2})$/, // 7000-7799, 7800-7999
            VIC: /^(3[0-9]{3}|8[0-9]{3})$/, // 3000-3999, 8000-8999
            WA: /^(6[0-7][0-9]{2}|68[0-9]{2}|69[0-9]{2})$/ // 6000-6797, 6800-6999
        };

        if (postcode && state) {
            return postcodeRanges[state]?.test(postcode);
        }

        return true;
    };

    const handlePostcodeChange = (event) => {
        const newPostcode = event.target.value;
        setPostcode(newPostcode);
        setIsPostcodeValid(validatePostcode(newPostcode, selectedState));
    };

    const handleStateChange = (event) => {
        const newState = event.target.value;
        setSelectedState(newState);
        setIsPostcodeValid(validatePostcode(postcode, newState));
    };

    const handleLinkClick = (event) => {
        const requiredFields = [
            document.getElementById("address"),
            document.getElementById("suburb"),
            document.getElementById("state"),
            document.getElementById("postcode")
        ];

        const isValid = requiredFields.every((field) => field.value);

        if (!isValid || !isPostcodeValid) {
            event.preventDefault();
            setFormErrorMessage("Please fill in your shipping details before proceeding to payment");
        }
    };

    return (
        <div className="checkout-container">
            <h2>Shipping</h2>
            <form className="page-form">
                <input type="text" placeholder="Address" className="form-input" id="address" />
                <input type="text" placeholder="Apartment, suite, etc. (optional)" className="form-input" />
                <div className="input-group">
                    <input
                        type="text"
                        placeholder="Suburb"
                        id="suburb"
                        className="form-input"
                    />
                    <input
                        type="text"
                        placeholder="Postcode"
                        id="postcode"
                        className={`form-input ${isPostcodeValid ? "" : "invalid"}`}
                        value={postcode}
                        onChange={handlePostcodeChange}
                    />
                </div>
                <select
                    className="form-input"
                    id="state"
                    value={selectedState}
                    onChange={handleStateChange}
                >
                    <option value="">State/territory</option>
                    <option value="ACT">Australian Capital Territory</option>
                    <option value="NSW">New South Wales</option>
                    <option value="NT">Northern Territory</option>
                    <option value="QLD">Queensland</option>
                    <option value="SA">South Australia</option>
                    <option value="TAS">Tasmania</option>
                    <option value="VIC">Victoria</option>
                    <option value="WA">Western Australia</option>
                </select>
                {!isPostcodeValid && (
                    <span className="error">Invalid postcode for the selected state/territory</span>
                )}
                {formErrorMessage && (<span className="error">{formErrorMessage}</span>)}
            </form>
            <OrderSummary
                extra={
                    <div>
                        <Link
                            className={`link-button ${loggedIn ? "" : "disabled"}`}
                            to="/payment"
                            onClick={handleLinkClick}
                        >
                            Go to Payment
                        </Link>
                    </div>
                }
            />
        </div>
    );
}

export default ShippingPage;
