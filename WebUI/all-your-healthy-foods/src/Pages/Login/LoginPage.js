import React from 'react';
import './Login.css';
import { Link } from 'react-router-dom';
import PasswordInput from "./PasswordInput";

const LoginInterface = () => {
    return (
        <div className="login-container">
            <div className="login-card">
                <h2 className="login-title">Login</h2>
                <form className="login-form">
                    <input type="text" placeholder="Username" className="login-input" />
                    <PasswordInput placeholder="Password" showInfo={true} />
                    <button type="submit" className="login-button">Login</button>
                </form>
                <Link to='/signup'>Not Already a member? Sign Up Now!</Link>
            </div>
        </div>
    );
};

export default LoginInterface;
