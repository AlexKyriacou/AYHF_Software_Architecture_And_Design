import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import LoginPage from "./pages/login/LoginPage";
import SignupPage from "./pages/login/SignupPage";
import Navbar from "./components/navbar/Navbar";
import HomePage from "./pages/home/HomePage";
import CartPage from "./pages/cart/CartPage";
import { UserProvider } from "./AppContext";
import ProductPage from "./pages/product/ProductPage";
import CheckoutPage from "./pages/checkout/CheckoutPage";
import ShippingPage from "./pages/checkout/ShippingPage";
import PaymentPage from "./pages/checkout/PaymentPage";
import AccountPage from "./pages/account/AccountPage";
import OrderConfirmationPage from "./pages/checkout/OrderConfirmationPage"
import "./App.css";
import OrderHistory from "./pages/checkout/OrderHistory";

function App() {
    return (
        <Router>
            <UserProvider>
                <div className="App">
                    <Navbar />
                    <Routes>
                        <Route path="" element={<HomePage />}></Route>
                        <Route path="/login" element={<LoginPage />}></Route>
                        <Route path="/signup" element={<SignupPage />}></Route>
                        <Route path="/account/:username" element={<AccountPage />} />
                        <Route path="/cart" element={<CartPage />} />
                        <Route path="/product/:productName" element={<ProductPage />} />
                        <Route path="/checkout" element={<CheckoutPage />} />
                        <Route path="/shipping" element={<ShippingPage />} />
                        <Route path="/payment" element={<PaymentPage />} />
                        <Route path="/order-confirmation" element={<OrderConfirmationPage />} />
                        <Route path="/order-history" element={<OrderHistory />} />
                    </Routes>
                    <footer className="App-footer">
                        <p>© 2023 All Your Healthy Foods - All Rights Reserved.</p>
                    </footer>
                </div>
            </UserProvider>
        </Router>
    );
}

export default App;
