import React, {useEffect, useState} from "react";
import {Link, useNavigate} from "react-router-dom";
import PasswordInput from "../../components/PasswordInput";
import TextInputWithValidation from '../../components/TextInputWithValidation'
import "./Login.css";
import SelectWithValidation from "../../components/SelectWithValidation";
import axios from "axios";

const SignupPage = () => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [role, setRole] = useState("");
    const [passwordError, setPasswordError] = useState("");

    const navigate = useNavigate();

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

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (doPasswordsMatch(password, confirmPassword)) {
            setPasswordError("");

            const user = {
                name: firstName + " " + lastName,
                username,
                email,
                password,
                role
            };

            try {
                const response = await axios.post(
                    "https://localhost:7269/users/register",
                    user
                );

                if (response.status === 201) {
                    navigate("/login");
                } else {
                    setPasswordError(
                        "An unexpected error occurred while registering"
                    );
                }
            } catch (error) {
                setPasswordError(
                    "An unexpected error occurred while registering, please refresh and try again."
                );
            }
        } else {
            setPasswordError("Passwords do not match");
        }
    };

    return (
        <div className="page-container">
            <div className="page-card">
                <h2 className="page-title">Create Account</h2>
                <form className="page-form" onSubmit={handleSubmit}>
                    <input
                        type="text"
                        placeholder="Username"
                        className="text-input readonly"
                        required
                        value={username}
                        readOnly
                    />
                    <div className="input-group">
                        <TextInputWithValidation
                            placeholder="First Name"
                            required={true}
                            value={firstName}
                            regex={/^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$/}
                            regexErrorMsg="Invalid Character"
                            parentOnChange={setFirstName}
                        />
                        <TextInputWithValidation
                            placeholder="Last Name"
                            required={true}
                            value={lastName}
                            regex={/^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$/}
                            regexErrorMsg="Invalid Character"
                            parentOnChange={setLastName}
                        />
                    </div>
                    <TextInputWithValidation
                        placeholder="Email"
                        required={true}
                        value={email}
                        regex={/^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/}
                        regexErrorMsg="Invalid Email"
                        parentOnChange={setEmail}
                    />
                    <PasswordInput
                        placeholder="Password"
                        showInfo={true}
                        onChange={setPassword}
                        passwordError={passwordError}
                        checkPattern={true}
                    />
                    <PasswordInput
                        placeholder="Confirm password"
                        value={confirmPassword}
                        onChange={setConfirmPassword}
                        passwordError={passwordError}
                        checkPattern={true}
                    />
                    <SelectWithValidation
                        placeholder="Select a Role"
                        required={true}
                        value={role}
                        parentOnChange={setRole}
                        options={[
                            {label: "Customer", value: "customer"},
                            {label: "Admin", value: "admin"}
                        ]}
                    />
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
