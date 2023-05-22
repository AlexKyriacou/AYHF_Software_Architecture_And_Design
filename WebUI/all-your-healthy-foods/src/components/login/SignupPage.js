import React, { useState, useEffect } from "react";
import "./Login.css";
import { Link } from "react-router-dom";
import PasswordInput from "./PasswordInput";

const SignupPage = () => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [role, setRole] = useState("");

    const [passwordError, setPasswordError] = useState("");

    const isValidPassword = (password) => {
        const passwordValidationRegex = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        return passwordValidationRegex.test(password);
    };

    const doPasswordsMatch = (password, confirmPassword) => {
        return password === confirmPassword;
    };

    useEffect(() => {
        const generateUsername = () => {
            if (firstName.trim() === "" && lastName.trim() === "") {
                setUsername("");
            } else {
                const generatedUsername = `${firstName}.${lastName}`;
                setUsername(generatedUsername);
            }
        };

        generateUsername();
    }, [firstName, lastName]);

    const handleSubmit = (e) => {
        e.preventDefault();

        if (isValidPassword(password)) {
            if (doPasswordsMatch(password, confirmPassword)) {
                setPasswordError("");

                const user = {
                    name: firstName + " " + lastName,
                    username,
                    email,
                    password,
                    role
                };

                // TODO: Send the user object to the backend and get the backend response
                console.log("User: ", user);
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
                    <input
                        type="text"
                        placeholder="Username"
                        className="signup-input readonly"
                        required
                        value={username}
                        readOnly
                    />
                    <div className="signup-input-group">
                        <input
                            type="text"
                            placeholder="First name"
                            className="signup-input"
                            required
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                        />
                        <input
                            type="text"
                            placeholder="Last Name"
                            className="signup-input"
                            required
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                        />
                    </div>
                    <input
                        type="email"
                        placeholder="Email"
                        className="signup-input"
                        required
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <PasswordInput
                        placeholder="Password"
                        showInfo={true}
                        onChange={(e) => setPassword(e.target.value)}
                        passwordError={passwordError}
                    />
                    <PasswordInput
                        placeholder="Confirm password"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        passwordError={passwordError}
                    />
                    <select
                        className="signup-input"
                        required
                        value={role}
                        onChange={(e) => setRole(e.target.value)}
                    >
                        <option value="">Select a Role</option>
                        <option value="customer">Customer</option>
                        <option value="admin">Admin</option>
                    </select>
                    <button type="submit" className="primary-button">
                        Sign Up
                    </button>
                </form>
                <Link className="switch" to="/login">
                    Already a member? Login
                </Link>
            </div>
        </div>
    );
};

export default SignupPage;
