import React, { useState } from "react";
import "./Input.css"

function validateInput(value, required, regex, regexErrorMsg) {
    if (!value && required) {
        return "Required";
    }
    if (regex) {
        const isValid = regex.test(value);
        return isValid ? "" : regexErrorMsg;
    }
}

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
