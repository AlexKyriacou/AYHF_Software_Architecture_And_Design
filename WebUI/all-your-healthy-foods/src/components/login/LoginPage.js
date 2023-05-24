import React, { useState, useContext } from "react";
import { Link, Navigate, useLocation } from "react-router-dom";
import PasswordInput from "./PasswordInput";
import { UserContext } from "../../AppContext";
import userData from "../../testData/users"; //TO DELETE ONCE WE ESTABLISH BACKEND CONNECTION
import TextInputWithValidation from '../TextInputWithValidation'
import "./Login.css";

const LoginPage = () => {
    const users = userData; //TODO: REPLACE WITH ACTUAL USERS ONCE WE ESTABLISH BACKEND CONNECTION
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [loginError, setLoginError] = useState("");
    const { login, loggedIn } = useContext(UserContext);

    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const from = queryParams.get("from");

    const handleUsernameChange = (username) => {
        setUsername(username);
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
        <div className="page-container">
            <div className="page-card">
                <h2 className="page-title">Login</h2>
                <form className="page-form" onSubmit={handleSubmit}>
                    {loginError && <span className="error">{loginError}</span>}
                    {loggedIn && (
                        <Navigate to={from ? ("/" + from) : "/"} replace={true} />
                    )}
                    <TextInputWithValidation
                        placeholder="Username"
                        value={username}
                        parentOnChange={handleUsernameChange}
                        required={true}
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
