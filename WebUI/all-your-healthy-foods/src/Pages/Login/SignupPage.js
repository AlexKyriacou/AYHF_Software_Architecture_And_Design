import React, { useState } from "react";
import "./Login.css";
import { Link } from "react-router-dom";
import PasswordInput from "./PasswordInput";

const SignupInterface = () => {
    const [isAdmin, setIsAdmin] = useState(false);
    const handleAdminCheckboxChange = (event) => {
        setIsAdmin(event.target.checked);
    };

    const [password, setPassword] = useState("");
    const [passwordError, setPasswordError] = useState("");


    const isValidPassword = (password) => {
        const passwordValidationRegex = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        return passwordValidationRegex.test(password);
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (isValidPassword(password)) {
            setPasswordError("");
            // Proceed with form submission
        } else {
            setPasswordError("Password does not meet the policy requirements.");
        }
    };


    return (
        <div className="signup-container">
            <div className="signup-card">
                <h2 className="signup-title">Create Account</h2>
                <form className="signup-form">
                    <div className="signup-input-group">
                        <input type="text" placeholder="First name" className="signup-input" required />
                        <span className="input-space"></span>
                        <input type="text" placeholder="Last Name" className="signup-input" required />
                    </div>
                    <input type="text" placeholder="Mobile Number" className="signup-input" required />
                    <input type="email" placeholder="Email" className="signup-input" required />
                    <PasswordInput placeholder="Password" showInfo={true} onChange={(e) => setPassword(e.target.value)} />
                    {passwordError && <p className="password-error">{passwordError}</p>}
                    <PasswordInput placeholder="Confirm password" />
                    <div className="admin-checkbox-container">
                        <input
                            type="checkbox"
                            id="isAdmin"
                            checked={isAdmin}
                            onChange={handleAdminCheckboxChange}
                            className="signup-input"
                        />
                        <label htmlFor="isAdmin">I am an Admin</label>
                    </div>
                    <button type="submit" className="signup-button" onSubmit={handleSubmit}>Sign Up</button>
                </form>
                <Link to="/login">Already a member? Login</Link>
            </div>
        </div>
    );
};

export default SignupInterface;