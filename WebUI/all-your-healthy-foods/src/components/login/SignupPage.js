import React, { useState } from "react";
import "./Login.css";
import { Link } from "react-router-dom";
import PasswordInput from "./PasswordInput";

const SignupPage = () => {
    // const [isAdmin, setIsAdmin] = useState(false);
    // const handleAdminCheckboxChange = (event) => {
    //     setIsAdmin(event.target.checked);
    // };

    const [password, setPassword] = useState("");

    const [passwordError, setPasswordError] = useState("");

    const isValidPassword = (password) => {
        const passwordValidationRegex = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        return passwordValidationRegex.test(password);
    };

    const [confirmPassword, setConfirmPassword] = useState("");

    const doPasswordsMatch = (password, confirmPassword) => {
        return password === confirmPassword;
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (isValidPassword(password)) {
            if (doPasswordsMatch(password, confirmPassword)) {
                setPasswordError("");
                // Proceed with form submission
            } else {
                setPasswordError("Passwords do not match");
            }
        } else {
            setPasswordError("Password does not meet the policy requirements");
        }
    };

    return (
        <div className="signup-container">
            <div className="signup-card">
                <h2 className="signup-title">Create Account</h2>
                <form className="signup-form" onSubmit={handleSubmit}>
                    <div className="signup-input-group">
                        <input type="text" placeholder="First name" className="signup-input" required />
                        <span className="input-space"></span>
                        <input type="text" placeholder="Last Name" className="signup-input" required />
                    </div>
                    <input type="text" placeholder="Mobile Number" className="signup-input" required />
                    <input type="email" placeholder="Email" className="signup-input" required />
                    <PasswordInput placeholder="Password" showInfo={true} onChange={(e) => setPassword(e.target.value)} passwordError={passwordError} />
                    <PasswordInput placeholder="Confirm password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} passwordError={passwordError} />
                    {
                        //Commenting this out until we come up with a better solution
                        /* <div className="admin-checkbox-container">
                            <input
                                type="checkbox" id="isAdmin" checked={isAdmin} onChange={handleAdminCheckboxChange} className="signup-input"
                            />
                            <label htmlFor="isAdmin">I am an Admin</label>
                        </div> */
                    }
                    <button type="submit" className="signup-button">Sign Up</button>
                </form>
                <Link className='switch' to="/login">Already a member? Login</Link>
            </div>
        </div>
    );
};

export default SignupPage;