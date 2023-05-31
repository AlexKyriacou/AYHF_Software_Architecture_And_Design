import React, {useState} from "react";
import "./Input.css"

/**
 * Validates input value based on required and regex pattern
 * @param {string} value - input value to validate
 * @param {boolean} required - whether the input is required or not
 * @param {RegExp} regex - optional regular expression that input value should match
 * @param {string} regexErrorMsg - error message to display when regex validation fails
 * @returns {string} - returns an error message if validation fails, otherwise empty string
 */
function validateInput(value, required, regex, regexErrorMsg) {
    if (!value && required) {
        return "Required";
    }
    if (regex) {
        const isValid = regex.test(value);
        return isValid ? "" : regexErrorMsg;
    }
}

/**
 * Component that renders an input with validation
 * @param {Object} props - component properties
 * @param {string} [props.type="text"] - input type
 * @param {string} props.placeholder - input placeholder text
 * @param {boolean} props.required - whether the input is required or not
 * @param {RegExp} [props.regex] - optional regular expression that input value should match
 * @param {string} [props.regexErrorMsg] - error message to display when regex validation fails
 * @param {function} props.parentOnChange - function to invoke with the input value on change
 * @param {boolean} [props.readonly] - whether the input is read-only or not
 * @param {string} [props.customErrorMsg] - custom error message to display when validation fails
 * @returns {JSX.Element} - returns JSX that renders the input with validation
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
     * Event handler that updates component state and invokes parentOnChange function
     * @param {Object} event - DOM event object
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
