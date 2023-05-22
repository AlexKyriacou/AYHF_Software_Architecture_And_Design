import React, { useState } from 'react';
import './Login.css';
import { Link } from 'react-router-dom';
import PasswordInput from "./PasswordInput";

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleUsernameChange = (event) => {
        setUsername(event.target.value);
    };

    const handlePasswordChange = (event) => {
        setPassword(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        const user = {
            username,
            password
        };

        // TODO: Send the user object to the backend and get the backend response
        // (including whether the user exists, and if so what's their role)
        console.log("User: ", user);
    };

    return (
        <div className="login-container">
            <div className="login-card">
                <h2 className="login-title">Login</h2>
                <form className="login-form" onSubmit={handleSubmit}>
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
                        clear={() => setPassword('')}
                    />
                    <button type="submit" className="login-button">Login</button>
                </form>
                <Link className='switch' to='/signup'>Not Already a member? Sign Up Now!</Link>
            </div>
        </div>
    );
};

export default LoginPage;
