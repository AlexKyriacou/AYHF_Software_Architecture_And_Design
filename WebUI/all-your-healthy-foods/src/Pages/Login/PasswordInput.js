import React, { useState } from "react";

const PasswordInput = ({ placeholder, showInfo, onChange }) => {
    const [passwordShown, setPasswordShown] = useState(false);
    const [passwordPolicyVisible, setPasswordPolicyVisible] = useState(false);

    const togglePasswordVisibility = () => {
        setPasswordShown(!passwordShown);
    };

    const togglePasswordPolicyVisibility = () => {
        setPasswordPolicyVisible(!passwordPolicyVisible);
    };

    return (
        <div className="password-container">
            <input
                type={passwordShown ? "text" : "password"}
                placeholder={placeholder}
                className="signup-input"
                required
                onChange={onChange}
            />
            <span
                className={`toggle-password ${passwordShown ? "fa fa-eye-slash" : "fa fa-eye"}`}
                onClick={togglePasswordVisibility}
            />
            <div className="password-info-container">
                {showInfo && <span className="fa fa-info-circle" onClick={togglePasswordPolicyVisibility}></span>}
                {passwordPolicyVisible && (
                    <div className="password-policy">
                        <h3>Password Policy:</h3>
                        <ul>
                            <li>At least 8 characters long</li>
                            <li>Contains at least one uppercase letter (A-Z)</li>
                            <li>Contains at least one lowercase letter (a-z)</li>
                            <li>Contains at least one digit (0-9)</li>
                            <li>Contains at least one special character (e.g. !,@,#,$,%,&,*)</li>
                        </ul>
                    </div>
                )}
            </div>
        </div >
    );
};

export default PasswordInput;
