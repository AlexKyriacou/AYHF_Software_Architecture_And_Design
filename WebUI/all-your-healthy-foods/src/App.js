import React from 'react';
import { BrowserRouter as Router, Link, Route, Routes } from 'react-router-dom';
import LoginPage from './Pages/Login/LoginPage';
import SignupPage from './Pages/Login/SignupPage';
import './App.css';

function App() {
    return (
        <Router>
            <div className="App">
                <nav className="navbar">
                    <span class="fa fa-pagelines"><a href='/' className="App-header"> All Your Healthy Foods</a></span>
                    <Link className="login-button" to="/login">Login</Link>
                </nav>
                <Routes>
                    <Route path="/login" element={<LoginPage />}>
                    </Route>
                    <Route path="/signup" element={<SignupPage />}>
                    </Route>
                </Routes>
            </div>
        </Router>
    );
}

export default App;
