import React, {useState} from "react";
import "./Input.css"

/**
 * Validates input based on value and whether it's required or not
 * @param {string} value - input value to validate
 * @param {boolean} required - whether the input is required or not
 * @returns {string} - returns an error message if validation fails, otherwise undefined
 */
function validateInput(value, required) {
    if ((!value || value === "") && required) {
        return "Required";
    }
}

/**
 * Component that renders a select input with validation
 * @param {Object} props - component properties
 * @param {boolean} props.required - whether the input is required or not
 * @param {string} props.placeholder - input placeholder text
 * @param {function} props.parentOnChange - function to invoke with the input value on change
 * @param {boolean} props.readonly - whether the input is read-only or not
 * @param {array} props.options - array of select options to be rendered
 * @returns {JSX.Element} - returns JSX that renders the select input with validation
 */
function SelectWithValidation({
    required,
    placeholder,
    parentOnChange,
    readonly,
    options
}) {
    const [value, setValue] = useState("");
    const [error, setError] = useState("");

    /**
     * Event handler that updates component state and invokes parentOnChange function
     * @param {Object} event - DOM event object
     */
    const handleChange = (event) => {
        const inputValue = event.target.value;
        const errorMessage = validateInput(inputValue, required);
        setValue(inputValue);
        setError(errorMessage);

        if (parentOnChange) {
            parentOnChange(inputValue);
        }
    };

    return (
        <div className="input-with-validation">
            <select
                className={`text-input ${readonly ? 'readonly' : ''}`}
                value={value}
                required={required}
                onChange={handleChange}
                readOnly={readonly}
            >
                <option value="" disabled>{placeholder}</option>
                {options.map((option) => (
                    <option key={option.value} value={option.value}>{option.label}</option>
                ))}
            </select>
            {error && <span className="error-message">{error}</span>}
        </div>
    );
}

export default SelectWithValidation;
