import React, { createContext, useEffect, useState } from "react";
import axios from "axios";

// Create the CartContext, UserContext, and ProductsContext
const CartContext = createContext();
const UserContext = createContext();
const ProductsContext = createContext();

// Define the CartProvider component
const CartProvider = ({ children }) => {
    const [cartCount, setCartCount] = useState(0);
    const [cartItems, setCartItems] = useState([]);

    useEffect(() => {
        // Retrieve cart items from local storage when the component mounts
        const storedCartItems = localStorage.getItem("cartItems");
        if (storedCartItems) {
            const parsedCartItems = JSON.parse(storedCartItems);
            setCartItems(parsedCartItems);
            setCartCount(parsedCartItems.length);
        }
    }, []);

    // Function to update cart items and count
    const updateCartItems = (updatedItems) => {
        setCartItems(updatedItems);
        setCartCount(updatedItems.length);
        localStorage.setItem("cartItems", JSON.stringify(updatedItems));
    };

    // Function to add a product to the cart
    const addToCart = (product) => {
        const updatedCartItems = [...cartItems, { ...product, count: 1 }];
        updateCartItems(updatedCartItems);
    };

    // Function to remove an item from the cart
    const removeFromCart = (item) => {
        const updatedCartItems = cartItems.filter(
            (cartItem) => cartItem.name !== item.name
        );
        updateCartItems(updatedCartItems);
    };

    // Function to increase the count of a cart item
    const increaseCount = (itemName) => {
        const updatedCartItems = [...cartItems];
        const foundItem = updatedCartItems.find(
            (cartItem) => cartItem.name === itemName
        );
        if (foundItem) {
            addToCart(foundItem);
        }
    };

    // Function to decrease the count of a cart item
    const decreaseCount = (itemName) => {
        const updatedCartItems = [...cartItems];
        const foundItemIndex = updatedCartItems.findIndex(
            (cartItem) => cartItem.name === itemName
        );
        if (foundItemIndex !== -1) {
            updatedCartItems.splice(foundItemIndex, 1);
            setCartItems(updatedCartItems);
            setCartCount(cartCount - 1);
            updateCartItems(updatedCartItems);
        }
    };

    // Function to clear the cart
    const clearCart = () => {
        setCartItems([]);
        setCartCount(0);
        localStorage.setItem("cartItems", JSON.stringify([]));
    };

    // Group cart items by name and count the occurrences
    const groupedProducts = cartItems.reduce((grouped, item) => {
        if (!grouped[item.name]) {
            grouped[item.name] = { ...item, count: 1 };
        } else {
            grouped[item.name].count += 1;
        }
        return grouped;
    }, {});

    // Sort the grouped products by name
    const sortedGroupedProducts = Object.fromEntries(
        Object.entries(groupedProducts).sort(([nameA], [nameB]) =>
            nameA.localeCompare(nameB)
        )
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
                clearCart,
            }}
        >
            {children}
        </CartContext.Provider>
    );
};

// Define the ProductsProvider component
const ProductsProvider = ({ children }) => {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        // Fetch products from API or retrieve from session storage
        const fetchProducts = async () => {
            try {
                const response = await axios.get("https://localhost:7269/products");
                if (response.status === 200) {
                    const fetchedProducts = response.data;
                    setProducts(fetchedProducts);
                    sessionStorage.setItem("products", JSON.stringify(fetchedProducts));
                }
            } catch (error) {
                console.error("Error fetching products:", error);
            }
        };

        const storedProducts = sessionStorage.getItem("products");
        if (storedProducts) {
            setProducts(JSON.parse(storedProducts));
        } else {
            fetchProducts();
        }
    }, []);

    // Fetch product feedbacks from the API
    const fetchProductFeedbacks = async (product) => {
        try {
            const response = await axios.get(
                `https://localhost:7269/Products/${product.id}/feedback`
            );
            if (response.status === 200) {
                return response.data;
            } else {
                throw new Error("Failed to fetch feedbacks");
            }
        } catch (error) {
            console.error(error);
            throw error; // Rethrow the error to propagate it to the caller
        }
    };

    // Get product feedbacks and calculate average rating
    const getProductFeedbacks = async (product) => {
        try {
            const feedbacks = await fetchProductFeedbacks(product);
            const totalRating = feedbacks.reduce(
                (total, feedback) => total + feedback.rating,
                0
            );
            const averageRating = totalRating / feedbacks.length;
            return { feedbacks, averageRating };
        } catch (error) {
            console.error(error);
            return { feedbacks: [], averageRating: 0 }; // Return an empty array as a fallback in case of an error
        }
    };

    return (
        <ProductsContext.Provider value={{ products, setProducts, getProductFeedbacks }}>
            {children}
        </ProductsContext.Provider>
    );
};

// Define the UserProvider component
const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loggedIn, setLoggedIn] = useState(false);

    // Login user and store in session storage
    const login = (userData) => {
        setUser(userData);
        sessionStorage.setItem("user", JSON.stringify(userData));
        setLoggedIn(true);
        sessionStorage.setItem("loggedIn", "true");
    };

    // Logout user and clear session storage
    const logout = () => {
        setUser(null);
        sessionStorage.removeItem("user");
        setLoggedIn(false);
        sessionStorage.setItem("loggedIn", "false");
    };

    useEffect(() => {
        // Check session storage for logged in user
        const loggedInValue = sessionStorage.getItem("loggedIn");
        const loggedInUser = sessionStorage.getItem("user");
        if (loggedInValue === "true" && loggedInUser) {
            setUser(JSON.parse(loggedInUser));
            setLoggedIn(true);
        }
    }, []);

    return (
        <UserContext.Provider value={{ user, loggedIn, login, logout }}>
            <CartProvider>{children}</CartProvider>
        </UserContext.Provider>
    );
};

export {
    CartContext,
    CartProvider,
    UserContext,
    UserProvider,
    ProductsContext,
    ProductsProvider,
};
