import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './components/pages/login/LoginPage';
import SignupPage from './components/pages/login/SignupPage';
import Navbar from './components/navbar/Navbar';
import './App.css';
import HomePage from './components/pages/HomePage';
import Cart from './components/pages/Cart';
import { CartProvider } from './components/pages/CartContext';

function App() {
    return (
        <Router>
            <CartProvider>
                <div className="App">
                    <Navbar />
                    <Routes>
                        <Route path="" element={<HomePage />}></Route>
                        <Route path="/login" element={<LoginPage />}>
                        </Route>
                        <Route path="/signup" element={<SignupPage />}>
                        </Route>
                        <Route path="/cart" element={<Cart />} />
                    </Routes>
                    <footer className="App-footer">
                        <p>
                            © 2023 All Your Healthy Foods - All Rights Reserved.
                        </p>
                    </footer>
                </div>
            </CartProvider>
        </Router>
    );
}

export default App;
