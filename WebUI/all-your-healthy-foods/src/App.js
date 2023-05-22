import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './components/login/LoginPage';
import SignupPage from './components/login/SignupPage';
import Navbar from './components/navbar/Navbar';
import HomePage from './pages/home/HomePage';
import CartPage from './pages/cart/CartPage';
import { CartProvider } from './pages/cart/CartContext';
import ProductPage from './pages/product/ProductPage';
import CheckoutPage from './pages/checkout/CheckoutPage';
import ShippingPage from './pages/checkout/ShippingPage';
import PaymentPage from './pages/checkout/PaymentPage';
import './App.css';

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
                        <Route path="/cart" element={<CartPage />} />
                        <Route path="/product/:productName" element={<ProductPage />} />
                        <Route path="/checkout" element={<CheckoutPage />} />
                        <Route path="/shipping" element={<ShippingPage />} />
                        <Route path="/payment" element={<PaymentPage />} />
                    </Routes>
                    <footer className="App-footer">
                        <p>© 2023 All Your Healthy Foods - All Rights Reserved.</p>
                    </footer>
                </div>
            </CartProvider>
        </Router>
    );
}

export default App;
