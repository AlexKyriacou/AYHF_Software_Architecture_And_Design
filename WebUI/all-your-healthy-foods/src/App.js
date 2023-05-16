import React from 'react';
import { BrowserRouter as Router, Route, Link, Routes } from 'react-router-dom';
import LoginPage from './Pages/LoginPage';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <nav className="navbar">
          <a href='/' className="App-header">All Your Healthy Foods</a>
          <Link className="login-button" to="/login">Login</Link>
        </nav>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
