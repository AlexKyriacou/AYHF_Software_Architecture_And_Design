import React, { createContext, useEffect, useState } from "react";
import axios from "axios";

const CartContext = createContext();
const UserContext = createContext();
const ProductsContext = createContext();

const CartProvider = ({ children }) => {
    const [cartCount, setCartCount] = useState(0);
    const [cartItems, setCartItems] = useState([]);

    useEffect(() => {
        const storedCartItems = localStorage.getItem("cartItems");
        if (storedCartItems) {
            const parsedCartItems = JSON.parse(storedCartItems);
            setCartItems(parsedCartItems);
            setCartCount(parsedCartItems.length);
        }
    }, []);

    const updateCartItems = (updatedItems) => {
        setCartItems(updatedItems);
        setCartCount(updatedItems.length);
        localStorage.setItem("cartItems", JSON.stringify(updatedItems));
    };

    const addToCart = (product) => {
        const updatedCartItems = [...cartItems, { ...product, count: 1 }];
        updateCartItems(updatedCartItems);
    };

    const removeFromCart = (item) => {
        const updatedCartItems = cartItems.filter(
            (cartItem) => cartItem.name !== item.name
        );
        updateCartItems(updatedCartItems);
    };

    const increaseCount = (itemName) => {
        const updatedCartItems = [...cartItems];
        const foundItem = updatedCartItems.find(
            (cartItem) => cartItem.name === itemName
        );
        if (foundItem) {
            addToCart(foundItem);
        }
    };

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

    const clearCart = () => {
        setCartItems([]);
        setCartCount(0);
        localStorage.setItem("cartItems", JSON.stringify([]));
    };

    const groupedProducts = cartItems.reduce((grouped, item) => {
        if (!grouped[item.name]) {
            grouped[item.name] = { ...item, count: 1 };
        } else {
            grouped[item.name].count += 1;
        }
        return grouped;
    }, {});

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

const ProductsProvider = ({ children }) => {
    const [products, setProducts] = useState([]);

    useEffect(() => {
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

const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loggedIn, setLoggedIn] = useState(false);

    const login = (userData) => {
        setUser(userData);
        sessionStorage.setItem("user", JSON.stringify(userData));
        setLoggedIn(true);
        sessionStorage.setItem("loggedIn", "true");
    };

    const logout = () => {
        setUser(null);
        sessionStorage.removeItem("user");
        setLoggedIn(false);
        sessionStorage.setItem("loggedIn", "false");
    };

    useEffect(() => {
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
