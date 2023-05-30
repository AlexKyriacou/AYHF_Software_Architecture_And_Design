import React, {useContext, useState} from "react";
import {useNavigate} from "react-router-dom";
import OrderSummary from "../order/OrderSummary";
import {UserContext} from "../../AppContext";
import TextInputWithValidation from "../../components/TextInputWithValidation";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faShop, faTruckFast} from '@fortawesome/free-solid-svg-icons';
import SelectWithValidation from "../../components/SelectWithValidation";
import './Checkout.css';

function ShippingPage() {
    const navigate = useNavigate();

    const {loggedIn} = useContext(UserContext);
    const [address, setAddress] = useState("");
    const [suburb, setSuburb] = useState("");
    const [postcode, setPostcode] = useState("");
    const [selectedState, setSelectedState] = useState("");
    const [isPostcodeValid, setIsPostcodeValid] = useState(true);
    const [formErrorMessage, setFormErrorMessage] = useState("");
    const [deliveryOption, setDeliveryOption] = useState("delivery");
    const [saveShippingAddress, setSaveShippingAddress] = useState(false);

    let shipping = deliveryOption === "clickAndCollect" ? 0 : 10

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

    const handlePostcodeChange = (newPostcode) => {
        setPostcode(newPostcode);
        setIsPostcodeValid(validatePostcode(newPostcode, selectedState));
    };

    const handleStateChange = (newState) => {
        setSelectedState(newState);
        setIsPostcodeValid(validatePostcode(postcode, newState));
    };

    const handleDeliveryOptionChange = (option) => {
        setDeliveryOption(option);
        setFormErrorMessage("");
    };

    const handleLinkClick = () => {
        if (deliveryOption === "delivery") {
            const requiredFields = [address, suburb, postcode, selectedState];

            const isValid = requiredFields.every((value) => value);

            if (!isValid || !isPostcodeValid) {
                setFormErrorMessage(
                    "Please fill in your shipping details before proceeding to payment"
                );
                return;
            }
        }

        if (saveShippingAddress) {
            // TODO: Save address info against user
            // Call backend API to save shipping address
        }

        navigate("/payment", {state: shipping});
    };

    return (
        <div className="checkout-container">
            <h2>Shipping</h2>
            <h3>Please Select a Shipping Method</h3>
            <form className="page-form">
                <div className="delivery-option">
                    <label>
                        <input
                            type="radio"
                            name="deliveryOption"
                            value="delivery"
                            checked={deliveryOption === "delivery"}
                            onChange={() => handleDeliveryOptionChange("delivery")}
                        />
                        <FontAwesomeIcon icon={faTruckFast} aria-hidden="true"/> Delivery
                    </label>
                    <label>
                        <input
                            type="radio"
                            name="deliveryOption"
                            value="clickAndCollect"
                            checked={deliveryOption === "clickAndCollect"}
                            onChange={() => handleDeliveryOptionChange("clickAndCollect")}
                        />
                        <FontAwesomeIcon icon={faShop} aria-hidden="true"/> Click and Collect
                    </label>
                </div>
                {deliveryOption === "delivery" && (
                    <>
                        <TextInputWithValidation
                            placeholder="Address"
                            required={true}
                            value={address}
                            parentOnChange={setAddress}
                        />
                        <TextInputWithValidation
                            placeholder="Apartment, suite, etc. (optional)"
                        />
                        <div className="input-group">
                            <TextInputWithValidation
                                placeholder="Suburb"
                                required={true}
                                value={suburb}
                                parentOnChange={setSuburb}
                            />
                            <TextInputWithValidation
                                placeholder="Postcode"
                                required={true}
                                value={postcode}
                                parentOnChange={handlePostcodeChange}
                                regex={/^\d{4}$/}
                                regexErrorMsg="Postcode Must be exactly 4 digits long"
                                customErrorMsg={
                                    !isPostcodeValid
                                        ? "Invalid postcode for the selected state/territory"
                                        : ""
                                }
                            />
                        </div>
                        <SelectWithValidation
                            value={selectedState}
                            parentOnChange={handleStateChange}
                            placeholder="State/territory"
                            options={[
                                {label: "Australian Capital Territory", value: "ACT"},
                                {label: "New South Wales", value: "NSW"},
                                {label: "Northern Territory", value: "NT"},
                                {label: "Queensland", value: "QLD"},
                                {label: "South Australia", value: "SA"},
                                {label: "Tasmania", value: "TAS"},
                                {label: "Victoria", value: "VIC"},
                                {label: "Western Australia", value: "WA"}
                            ]}
                        />
                        <div className="save-shipping-address">
                            <label>
                                <input
                                    type="checkbox"
                                    checked={saveShippingAddress}
                                    onChange={(e) => setSaveShippingAddress(e.target.checked)}
                                />
                                Save shipping address to my account
                            </label>
                        </div>
                    </>
                )}
                {deliveryOption === "clickAndCollect" && (
                    <div className="click-and-collect-info">
                        <p>
                            You can Click & Collect for FREE from our Store on Glenferrie Road
                        </p>
                    </div>
                )}
                {formErrorMessage && (
                    <span className="error-message">{formErrorMessage}</span>
                )}
            </form>
            <OrderSummary
                step="shipping"
                extra={
                    <div>
                        <button
                            className={`secondary-button ${loggedIn ? "" : "disabled"}`}
                            onClick={handleLinkClick}
                        >
                            Go to Payment
                        </button>
                    </div>
                }
                shipping={shipping}
            />
        </div>
    );
}

export default ShippingPage;