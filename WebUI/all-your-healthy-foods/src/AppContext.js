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
        const updatedCartItems = cartItems.filter(
            (cartItem) => cartItem.name !== itemName
        );
        updateCartItems(updatedCartItems);
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

    return (
        <ProductsContext.Provider value={{ products, setProducts }}>
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
