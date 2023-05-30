/**
 * @fileoverview Defines the context providers for the application.
 */


import React, { createContext, useEffect, useState } from "react";
import axios from "axios";

/**
 * Context to provide information about the cart.
 */
const CartContext = createContext();

/**
 * Context to provide information about the user.
 */
const UserContext = createContext();

/**
 * Context to provide information about the products.
 */
const ProductsContext = createContext();

/**
 * Component to provide value to the CartContext.
 * @param {Object} param0 The props for the component.
 * @returns The component.
 */
const CartProvider = ({ children }) => {
    const [cartCount, setCartCount] = useState(0);
    const [cartItems, setCartItems] = useState([]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        const storedCartItems = localStorage.getItem("cartItems");
        if (storedCartItems) {
            setCartItems(JSON.parse(storedCartItems));
            setCartCount(JSON.parse(storedCartItems).length);
        }
    }, []);

    /**
     * Updates the cart items in state and in local storage.
     * @param {Array} updatedItems The updated cart items to set.
     */
    const updateCartItems = (updatedItems) => {
        setCartItems(updatedItems);
        setCartCount(updatedItems.length);
        localStorage.setItem("cartItems", JSON.stringify(updatedItems));
    };

    /**
     * Adds a product to the cart.
     * @param {Object} product The product to add.
     */
    const addToCart = (product) => {
        const updatedCartItems = [...cartItems, { ...product, count: 1 }];
        updateCartItems(updatedCartItems);
    };

    /**
     * Removes an item from the cart.
     * @param {Object} item The item to remove.
     */
    const removeFromCart = (item) => {
        const updatedCartItems = cartItems.filter(
            (cartItem) => cartItem.name !== item.name
        );
        updateCartItems(updatedCartItems);
    };

    /**
     * Increases the count of an item in the cart.
     * @param {String} itemName The name of the item to increase count for.
     */
    const increaseCount = (itemName) => {
        const updatedCartItems = [...cartItems];
        const foundItem = updatedCartItems.find((cartItem) => cartItem.name === itemName);
        if (foundItem) {
            addToCart(foundItem);
        }
    };

    /**
     * Decreases the count of an item in the cart.
     * @param {String} itemName The name of the item to decrease count for.
     */
    const decreaseCount = (itemName) => {
        const updatedCartItems = [...cartItems];
        const foundItemIndex = updatedCartItems.findIndex((cartItem) => cartItem.name === itemName);
        if (foundItemIndex !== -1) {
            updatedCartItems.splice(foundItemIndex, 1);
            setCartItems(updatedCartItems);
            setCartCount(cartCount - 1);
            updateCartItems(updatedCartItems);
        }
    };

    /**
     * Clears the cart.
     */
    const clearCart = () => {
        setCartItems([]);
        setCartCount(0);
        localStorage.setItem("cartItems", JSON.stringify([]));
    };

    // Group cart items by product name and count how many of each product there are
    const groupedProducts = cartItems.reduce((grouped, item) => {
        if (!grouped[item.name]) {
            grouped[item.name] = {
                ...item,
                count: 1,
            };
        } else {
            grouped[item.name].count += 1;
        }
        return grouped;
    }, {});

    // Sort grouped products by name
    const sortedGroupedProducts = Object.fromEntries(
        Object.entries(groupedProducts).sort(([nameA], [nameB]) => nameA.localeCompare(nameB))
    );

    return (
        <CartContext.Provider
            value={{
                cartCount,
                cartItems,
                sortedGroupedProducts,
                addToCart,
                removeFromCart,
                decreaseCount,
                increaseCount,
                clearCart
            }}
        >
            {children}
        </CartContext.Provider>
    );
};

/**
 * Component to provide value to the ProductsContext.
 * @param {Object} props The props for the component.
 * @returns The component.
 */
const ProductsProvider = ({ children }) => {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await axios.get("https://localhost:7269/products");
                if (response.status === 200) {
                    const fetchedProducts = response.data;
                    setProducts(fetchedProducts);
                    sessionStorage.setItem("products", JSON.stringify(fetchedProducts)); // Save products to sessionStorage
                }
            } catch (error) {
                console.error("Error fetching products:", error);
            }
        };

        const storedProducts = sessionStorage.getItem("products");
        if (storedProducts) {
            setProducts(JSON.parse(storedProducts)); // Load products from sessionStorage if available
        } else {
            fetchProducts(); // Fetch products if not available in sessionStorage
        }
    }, []);

    return (
        <ProductsContext.Provider value={{ products, setProducts }}>
            {children}
        </ProductsContext.Provider>
    );
};


/**
 * Component to provide value to the UserContext.
 * @param {Object} props The props for the component.
 * @returns The component.
 */
const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loggedIn, setLoggedIn] = useState(false);

    /**
     * Logs a user in.
     * @param {Object} userData The user to log in.
     */
    const login = (userData) => {
        setUser(userData);
        sessionStorage.setItem("user", JSON.stringify(userData));
        setLoggedIn(true);
        sessionStorage.setItem("loggedIn", "true");
    };

    /**
     * Logs a user out.
     */
    const logout = () => {
        setUser(null);
        sessionStorage.setItem("user", null);
        setLoggedIn(false);
        sessionStorage.setItem("loggedIn", "false");
    };

    useEffect(() => {
        // Load user details from Session storage
        const loggedInValue = sessionStorage.getItem("loggedIn");
        const loggedInUser = sessionStorage.getItem("user");
        if (loggedInValue === "true") {
            setUser(JSON.parse(loggedInUser));
            setLoggedIn(true);
        }
    }, []);

    return (
        <UserContext.Provider
            value={{
                user,
                loggedIn,
                login,
                logout,
            }}
        >
            <CartProvider>
                {children}
            </CartProvider>
        </UserContext.Provider>
    );
};

export { CartContext, CartProvider, UserContext, UserProvider, ProductsContext, ProductsProvider };
