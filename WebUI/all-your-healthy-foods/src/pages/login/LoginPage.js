import React, { useContext, useState } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import PasswordInput from "../../components/PasswordInput";
import { UserContext } from "../../AppContext";
import TextInputWithValidation from '../../components/TextInputWithValidation'
import "./Login.css";
import axios from "axios";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loginError, setLoginError] = useState("");
    const { login } = useContext(UserContext);

    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const from = queryParams.get("from");

    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const response = await axios.post('https://localhost:7269/users/login', { email, password });

            if (response.status !== 200) {
                throw new Error('Network response was not ok');
            }

            const { token, user } = response.data;

            localStorage.setItem("token", token);

            login(user);
            setLoginError("");
            navigate(from ? `/${from}` : "/");

        } catch (error) {
            setLoginError("Invalid email or password");
        }
    };

    return (
        <div className="page-container">
            <div className="page-card">
                <h2 className="page-title">Login</h2>
                <form className="page-form" onSubmit={handleSubmit}>
                    {loginError && <span className="error-message">{loginError}</span>}
                    <TextInputWithValidation
                        placeholder="Email"
                        value={email}
                        parentOnChange={setEmail}
                        required={true}
                        type="email"
                    />
                    <PasswordInput
                        placeholder="Password"
                        value={password}
                        onChange={setPassword}
                        checkPattern={false}
                    />
                    <button type="submit" className="primary-button">Login</button>
                </form>
                <Link className="switch" to="/signup">Not Already a member? Sign Up Now!</Link>
            </div>
        </div>
    );
};

export default LoginPage;
