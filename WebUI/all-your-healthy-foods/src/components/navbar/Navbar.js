import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import Search from '../../pages/search/Search';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPagelines } from "@fortawesome/free-brands-svg-icons";
import { faShoppingCart, faClipboardList, faSignInAlt } from '@fortawesome/free-solid-svg-icons';
import { CartContext } from '../../AppContext';
import './Navbar.css';

function Navbar() {
    const { cartCount } = useContext(CartContext);

    return (
        <nav className="navbar">
            <span>
                <FontAwesomeIcon icon={faPagelines} className="App-header" aria-hidden="true" />
                <a href='/' className="App-header"> All Your Healthy Foods</a>
            </span>
            <Search />
            <div className='icons'>
                <Link className='login-link' to="/login">
                    <FontAwesomeIcon className="login-icon" icon={faSignInAlt} />&nbsp;Login
                </Link>
                <Link className='shopping-list-link' to="/list">
                    <FontAwesomeIcon className='shopping-list' icon={faClipboardList} />
                </Link>
                <Link className='shopping-cart-link' to="/cart">
                    <FontAwesomeIcon className='shopping-cart' icon={faShoppingCart} />
                    <span className='cart-count'>{cartCount}</span>
                </Link>
            </div>
        </nav>
    );
}

export default Navbar;
