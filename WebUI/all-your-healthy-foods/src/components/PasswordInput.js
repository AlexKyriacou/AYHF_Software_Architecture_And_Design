import React, {useEffect, useRef, useState} from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faEye, faEyeSlash, faInfoCircle} from "@fortawesome/free-solid-svg-icons";
import TextInputWithValidation from "./TextInputWithValidation"
import './Input.css'

/**
 * Component for rendering password input field.
 * @param {Object} props - Object containing props for this component.
 * @param {string} props.placeholder - The placeholder text for the input field.
 * @param {boolean} props.showInfo - A boolean indicating whether to show a password policy info icon.
 * @param {function} props.onChange - A function to call when the input value changes.
 * @param {boolean} props.checkPattern - A boolean indicating whether to check the input pattern.
 * @param {string} props.value - The value of the input field.
 * @param {string} props.passwordError - The error message to show if the password policy is not met.
 * @returns {JSX.Element} - Returns the JSX for the PasswordInput component.
 */
const PasswordInput = ({placeholder, showInfo, onChange, checkPattern, value, passwordError}) => {
    const [passwordShown, setPasswordShown] = useState(false);
    /**
     * Hook for maintaining the visibility of the password policy.
     */
    const [passwordPolicyVisible, setPasswordPolicyVisible] = useState(false);
    /**
     * Ref for the password container.
     */
    const passwordContainerRef = useRef(null);
    /**
     * Boolean indicating whether the browser is Edge.
     */
    const isEdgeBrowser = navigator.userAgent.indexOf("Edg") !== -1;

    /**
     * Toggles the visibility of the password.
     */
    const togglePasswordVisibility = () => {
        setPasswordShown(!passwordShown);
    };

    /**
     * Toggles the visibility of the password policy.
     */
    const togglePasswordPolicyVisibility = () => {
        setPasswordPolicyVisible(!passwordPolicyVisible);
    };

    /**
     * Hook that runs whenever the component is mounted.
     */
    useEffect(() => {
        /**
         * Click Event listener for when the user clicks outside of the password container.
         * @param {Object} event - The click event.
         */
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
     * JSX for rendering the PasswordInput component.
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
