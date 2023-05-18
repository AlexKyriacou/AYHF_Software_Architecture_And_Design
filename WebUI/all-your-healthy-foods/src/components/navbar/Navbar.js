import React from 'react';
import { Link } from 'react-router-dom';
import Search from '../search/Search'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPagelines } from "@fortawesome/free-brands-svg-icons";
import { faShoppingCart, faClipboardList, faSignInAlt } from '@fortawesome/free-solid-svg-icons';
import './Navbar.css';

function Navbar() {
    return (
        <nav className="navbar">
            <span>
                <FontAwesomeIcon icon={faPagelines} className="App-header" aria-hidden="true" />
                <a href='/' className="App-header"> All Your Healthy Foods</a>
            </span>
            <Search />
            <div className='icons'>
                <Link className='login-link' to="/login">
                    <FontAwesomeIcon className="login-icon" icon={faSignInAlt} />
                    <span className="input-space"></span>Login
                </Link>
                <span className="input-space"></span>
                <Link className='shopping-list-link' to="/list">
                    <FontAwesomeIcon className='shopping-list' icon={faClipboardList} />
                </Link>
                <span className="input-space"></span>
                <Link className='shopping-cart-link' to="/cart">
                    <FontAwesomeIcon className='shopping-cart' icon={faShoppingCart} />
                </Link>
            </div>
        </nav>
    );
}

export default Navbar;
