/**
 * This file contains the SelectWithValidation component.
 * It exports a functional React component that renders a select input with validation and various options.
 * @module components/SelectWithValidation
 */

import React, { useState } from "react";
import "./Input.css"

/**
 * Validates the select input value based on required flag.
 * @function
 * @param {string} value - The value of the input.
 * @param {boolean} required - The flag to indicate if the input is required.
 * @returns {string} The error message or empty string if valid.
 */
function validateInput(value, required) {
    if ((!value || value === "") && required) {
        return "Required";
    }
    return "";
}

/**
 * A functional React component that renders a select input with validation and various options.
 * @function
 * @param {Object} props - The props for the component.
 * @param {boolean} props.required - A flag to indicate if the input is required.
 * @param {string} props.placeholder - The placeholder text for the input.
 * @param {Function} props.parentOnChange - The function to handle input change.
 * @param {boolean} props.readonly - A flag to indicate if the input is read-only.
 * @param {Object[]} props.options - The list of options for the select input.
 * @param {string} props.options.label - The label text for the option.
 * @param {string} props.options.value - The value of the option.
 * @returns {JSX.Element}
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
     * Handles the change event of the select input.
     * @function
     * @param {Object} event - The change event object.
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

    /**
     * Renders the select input with validation and various options.
     * @returns {JSX.Element}
     */
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
