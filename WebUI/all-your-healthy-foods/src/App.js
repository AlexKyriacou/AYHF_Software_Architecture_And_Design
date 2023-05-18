import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './components/pages/login/LoginPage';
import SignupPage from './components/pages/login/SignupPage';
import Navbar from './components/navbar/Navbar';
import './App.css';
import HomePage from './components/pages/HomePage';

function App() {
    return (
        <Router>
            <div className="App">
                <Navbar />
                <Routes>
                    <Route path="" element={<HomePage />}></Route>
                    <Route path="/login" element={<LoginPage />}>
                    </Route>
                    <Route path="/signup" element={<SignupPage />}>
                    </Route>
                </Routes>
                <footer className="App-footer">
                    <p>
                        © 2023 All Your Healthy Foods - All Rights Reserved.
                    </p>
                </footer>
            </div>
        </Router>
    );
}

export default App;
