import React, { useState } from "react";
import "./Input.css"

function validateInput(value, required) {
    if ((!value || value === "") && required) {
        return "Required";
    }
}

function SelectWithValidation({
    required,
    placeholder,
    parentOnChange,
    readonly,
    options
}) {
    const [value, setValue] = useState("");
    const [error, setError] = useState("");

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
