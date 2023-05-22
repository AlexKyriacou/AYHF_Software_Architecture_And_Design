import React, { useState, useContext } from "react";
import { Link, Navigate } from "react-router-dom";
import PasswordInput from "./PasswordInput";
import { UserContext } from "../../AppContext";
import userData from "../login/users"; //TO DELETE ONCE WE ESTABLISH BACKEND CONNECTION
import "./Login.css";

const LoginPage = () => {
    const users = userData; //TODO: REPLACE WITH ACTUAL USERS ONCE WE ESTABLISH BACKEND CONNECTION
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [loginError, setLoginError] = useState("");
    const { login, loggedIn } = useContext(UserContext);

    const handleUsernameChange = (event) => {
        setUsername(event.target.value);
    };

    const handlePasswordChange = (event) => {
        setPassword(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        // TODO: Send the user object to the backend and get the backend response
        // (including whether the user exists, and if so what"s their role)
        const user = users.find((u) => u.username === username && u.password === password);

        if (user) {
            // Set the logged in user details using the login function from UserContext
            login(user);
            setLoginError("");
        } else {
            setLoginError("Invalid username or password");
        }
    };

    return (
        <div className="login-container">
            <div className="login-card">
                <h2 className="login-title">Login</h2>
                <form className="login-form" onSubmit={handleSubmit}>
                    {loginError && <span className="error">{loginError}</span>}
                    {loggedIn && (
                        <Navigate to="/" replace={true} />
                    )}
                    <input
                        type="text"
                        placeholder="Username"
                        className="login-input"
                        value={username}
                        onChange={handleUsernameChange}
                        required
                    />
                    <PasswordInput
                        placeholder="Password"
                        value={password}
                        onChange={handlePasswordChange}
                    />
                    <button type="submit" className="primary-button">Login</button>
                </form>
                <Link className="switch" to="/signup">Not Already a member? Sign Up Now!</Link>
            </div>
        </div>
    );
};

export default LoginPage;
