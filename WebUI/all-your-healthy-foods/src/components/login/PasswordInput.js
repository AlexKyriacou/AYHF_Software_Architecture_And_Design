import React, { useState, useEffect, useRef } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash, faInfoCircle } from "@fortawesome/free-solid-svg-icons";


const PasswordInput = ({ placeholder, showInfo, onChange, passwordError }) => {
    const [passwordShown, setPasswordShown] = useState(false);
    const [passwordPolicyVisible, setPasswordPolicyVisible] = useState(false);
    const passwordContainerRef = useRef(null);

    const togglePasswordVisibility = () => {
        setPasswordShown(!passwordShown);
    };

    const togglePasswordPolicyVisibility = () => {
        setPasswordPolicyVisible(!passwordPolicyVisible);
    };

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (passwordContainerRef.current && !passwordContainerRef.current.contains(event.target)) {
                setPasswordPolicyVisible(false);
            }
        };

        document.addEventListener("click", handleClickOutside);

        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    }, []);

    return (
        <div className="with-validation">
            <div className="password-container" ref={passwordContainerRef}>
                <input
                    type={passwordShown ? "text" : "password"}
                    placeholder={placeholder}
                    className="signup-input"
                    required
                    onChange={onChange}
                />
                <FontAwesomeIcon
                    icon={passwordShown ? faEyeSlash : faEye}
                    className="toggle-password password-icon"
                    onClick={togglePasswordVisibility}
                />
                <div className="password-info-container">
                    {showInfo && (
                        <FontAwesomeIcon
                            icon={faInfoCircle}
                            className="password-icon"
                            onClick={togglePasswordPolicyVisibility}
                        />
                    )}
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
            </div>
            {passwordError && <span className="password-error">{passwordError}</span>}
        </div>
    );
};

export default PasswordInput;