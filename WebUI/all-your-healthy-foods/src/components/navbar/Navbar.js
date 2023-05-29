import React, { useContext } from "react";
import { Link } from "react-router-dom";
import Search from "../../pages/search/Search";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPagelines } from "@fortawesome/free-brands-svg-icons";
import { faShoppingCart, faSignInAlt, faUser } from "@fortawesome/free-solid-svg-icons";
import { CartContext, UserContext } from "../../AppContext";
import axios from "axios";
import "./Navbar.css";

function Navbar() {
    const { cartCount } = useContext(CartContext);
    const { loggedIn, user } = useContext(UserContext);

    const handleSearch = async (searchQuery) => {
        try {
            const response = await axios.get("https://localhost:7269/products", {
                params: {
                    search: searchQuery,
                },
            });
        } catch (error) {
            console.error("Error occurred during search:", error);
        }
    };

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
