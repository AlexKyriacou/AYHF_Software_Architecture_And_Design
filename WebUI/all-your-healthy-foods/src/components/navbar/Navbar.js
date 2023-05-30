/**
 * This file contains the Navbar component.
 * It exports a functional React component that renders a navigational bar.
 * The navigational bar contains a search input, a logo, and icons for user authentication and shopping cart.
 * @module components/navbar/Navbar
 */

import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import Search from "../../pages/search/Search";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPagelines } from "@fortawesome/free-brands-svg-icons";
import { faShoppingCart, faSignInAlt, faUser } from "@fortawesome/free-solid-svg-icons";
import { CartContext, UserContext } from "../../AppContext";
import axios from "axios";
import "./Navbar.css";

/**
 * A functional React component that renders a navigational bar.
 * The navigational bar contains a search input, a logo, and icons for user authentication and shopping cart.
 * @function
 * @returns {JSX.Element}
 */
function Navbar() {
    /**
     * Use the CartContext provider to get the count of items in the cart.
     * Use the UserContext provider to get the logged in user's information.
     * Use the useNavigate hook to navigate using the router.
     */
    const { cartCount } = useContext(CartContext);
    const { loggedIn, user } = useContext(UserContext);
    const navigate = useNavigate();

    /**
     * Handles searching for products by making a GET request to the server.
     * @function
     * @async
     * @param {string} searchQuery - The search query entered by the user in the search input.
     */
    const handleSearch = async (searchQuery) => {
        try {
            const response = await axios.get("https://localhost:7269/products", {
                params: {
                    search: searchQuery,
                },
            });
            navigate("/search-results", { state: response.data });
        } catch (error) {
            console.error("Error occurred during search:", error);
        }
    };

    /**
     * Renders the navigational bar.
     */
    return (
        <nav className="navbar">
            <span>
                <FontAwesomeIcon icon={faPagelines} className="App-header" aria-hidden="true" />
                <a href="/" className="App-header"> All Your Healthy Foods</a>
            </span>
            <Search onSearch={handleSearch} />
            <div className="icons">
                {loggedIn ? (
                    <Link className="links" to="account">
                        <FontAwesomeIcon icon={faUser} />&nbsp;{user.name}
                    </Link>
                ) : (
                    <Link className="links" to="/login">
                        <FontAwesomeIcon icon={faSignInAlt} />&nbsp;Login
                    </Link>
                )}
                <Link className="links" to="/cart">
                    <FontAwesomeIcon icon={faShoppingCart} />
                    <span className="cart-count">{cartCount}</span>
                </Link>
            </div>
        </nav>
    );
}

export default Navbar;
