/**
 * This file contains the TextInputWithValidation component.
 * It exports a functional React component that renders a text input with validation and various options.
 * @module components/TextInputWithValidation
 */

import React, { useState } from "react";
import "./Input.css";

/**
 * Validates the input value based on the required flag and regex.
 * @function
 * @param {string} value - The current value of the input.
 * @param {boolean} required - The flag to indicate if the input is required.
 * @param {RegExp} regex - The regular expression to validate the input.
 * @param {string} regexErrorMsg - The error message when the input validation fails.
 * @returns {string} - The error message or empty string if valid.
 */
function validateInput(value, required, regex, regexErrorMsg) {
    if (!value && required) {
        return "Required";
    }
    if (regex) {
        const isValid = regex.test(value);
        return isValid ? "" : regexErrorMsg;
    }
    return "";
}

/**
 * A functional React component that renders a text input with validation and various options.
 * @function
 * @param {Object} props - The props for the component.
 * @param {string} props.type - The type of the input.
 * @param {string} props.placeholder - The placeholder text for the input.
 * @param {boolean} props.required - The flag to indicate if the input is required.
 * @param {RegExp} props.regex - The regular expression to validate the input.
 * @param {string} props.regexErrorMsg - The error message when the input validation fails.
 * @param {Function} props.parentOnChange - The function to handle input change.
 * @param {boolean} props.readonly - A flag to indicate if the input is read-only.
 * @param {string} props.customErrorMsg - The custom error message to display.
 * @returns {JSX.Element}
 */
function TextInputWithValidation({
    type = "text",
    placeholder,
    required,
    regex = "",
    regexErrorMsg = "",
    parentOnChange,
    readonly,
    customErrorMsg = ""
}) {
    const [value, setValue] = useState("");
    const [error, setError] = useState("");

    /**
     * Handles the change event of the text input.
     * @function
     * @param {Object} event - The change event object.
     */
    const handleChange = (event) => {
        const inputValue = event.target.value;
        const errorMessage = validateInput(inputValue, required, regex, regexErrorMsg);
        setValue(inputValue);
        setError(errorMessage);

        if (parentOnChange) {
            parentOnChange(inputValue);
        }
    };

    /**
     * Renders the text input with validation and various options.
     * @returns {JSX.Element}
     */
    return (
        <div className="input-with-validation">
            <input
                type={type}
                placeholder={placeholder}
                className={`text-input-validation ${readonly ? 'readonly' : ''}`}
                value={value}
                required={required}
                onChange={handleChange}
                readOnly={readonly}
            />
            {(error || customErrorMsg) && <span className="error-message">{error || customErrorMsg}</span>}
        </div>
    );
}

export default TextInputWithValidation;
