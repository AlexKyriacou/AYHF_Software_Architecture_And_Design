import React, { createContext, useState, useEffect } from 'react';

const CartContext = createContext();
const UserContext = createContext();

const CartProvider = ({ children }) => {
    const [cartCount, setCartCount] = useState(0);
    const [cartItems, setCartItems] = useState([]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        const storedCartItems = localStorage.getItem('cartItems');
        if (storedCartItems) {
            setCartItems(JSON.parse(storedCartItems));
            setCartCount(JSON.parse(storedCartItems).length);
        }
    }, []);

    const updateCartItems = (updatedItems) => {
        setCartItems(updatedItems);
        setCartCount(updatedItems.length);
        localStorage.setItem('cartItems', JSON.stringify(updatedItems));
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
        const foundItem = updatedCartItems.find((cartItem) => cartItem.name === itemName);
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
    };

    // Grouping the products by name and calculating the count
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
                clearCart,
            }}
        >
            {children}
        </CartContext.Provider>
    );
};

const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    const login = (userData) => {
        setUser(userData);
    };

    const logout = () => {
        setUser(null);
    };

    return (
        <UserContext.Provider
            value={{
                user,
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

export { CartContext, CartProvider, UserContext, UserProvider };
