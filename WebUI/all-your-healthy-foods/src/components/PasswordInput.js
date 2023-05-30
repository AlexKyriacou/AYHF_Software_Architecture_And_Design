/**
 * This file contains the PasswordInput component.
 * It exports a functional React component that renders a password input with validation and various options for password visibility.
 * @module components/PasswordInput
 */

import React, { useEffect, useRef, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash, faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import TextInputWithValidation from "./TextInputWithValidation"
import './Input.css'

/**
 * A functional React component that renders a password input with validation and various options for password visibility.
 * @function
 * @param {Object} props - The props for the component.
 * @param {string} props.placeholder - The placeholder text for the input.
 * @param {boolean} props.showInfo - A flag to show or hide the password policy.
 * @param {Function} props.onChange - The function to handle input change.
 * @param {boolean} props.checkPattern - A flag to check if the password meets the specified policy.
 * @param {string} props.value - The value of the input.
 * @param {string} props.passwordError - The custom error message for the password input.
 * @returns {JSX.Element}
 */
const PasswordInput = ({ placeholder, showInfo, onChange, checkPattern, value, passwordError }) => {
    const [passwordShown, setPasswordShown] = useState(false);
    const [passwordPolicyVisible, setPasswordPolicyVisible] = useState(false);
    const passwordContainerRef = useRef(null);
    const isEdgeBrowser = navigator.userAgent.indexOf("Edg") !== -1;

    /**
     * Toggles the visibility of the password.
     * @function
     */
    const togglePasswordVisibility = () => {
        setPasswordShown(!passwordShown);
    };

    /**
     * Toggles the visibility of the password policy.
     * @function
     */
    const togglePasswordPolicyVisibility = () => {
        setPasswordPolicyVisible(!passwordPolicyVisible);
    };

    /**
     * Adds a listener to hide the password policy when user clicks outside the password input container.
     */
    useEffect(() => {
        const handleClickOutside = (event) => {
            if (
                passwordContainerRef.current &&
                !passwordContainerRef.current.contains(event.target)
            ) {
                setPasswordPolicyVisible(false);
            }
        };

        document.addEventListener("click", handleClickOutside);

        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    }, []);

    /**
     * Renders the password input with validation and various options for password visibility.
     */
    return (
        <div className="password-container" ref={passwordContainerRef}>
            <TextInputWithValidation
                type={passwordShown ? "text" : "password"}
                placeholder={placeholder}
                required={true}
                parentOnChange={onChange}
                value={value}
                regex={checkPattern ? /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/ : ""}
                regexErrorMsg={checkPattern ? "Password does not meet the policy requirements" : ""}
                customErrorMsg={passwordError}
            />
            {!isEdgeBrowser && (
                <FontAwesomeIcon
                    icon={passwordShown ? faEyeSlash : faEye}
                    className="toggle-password password-icon"
                    onClick={togglePasswordVisibility}
                />
            )}
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
                            <li>
                                Contains at least one special character (e.g. !,@,#,$,%,&,*)
                            </li>
                        </ul>
                    </div>
                )}
            </div>
        </div>
    );
};

export default PasswordInput;
