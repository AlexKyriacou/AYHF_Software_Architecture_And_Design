import React, { createContext, useState } from 'react';

const CartContext = createContext();

const CartProvider = ({ children }) => {
    const [cartCount, setCartCount] = useState(0);
    const [cartItems, setCartItems] = useState([]);

    const addToCart = (product) => {
        const updatedCartItems = [...cartItems, { ...product, count: 1 }];
        setCartItems(updatedCartItems);
        setCartCount(cartCount + 1);
    };

    const removeFromCart = (item) => {
        const updatedCartItems = cartItems.filter((cartItem) => cartItem.name !== item.name);
        setCartItems(updatedCartItems);
        setCartCount(cartCount - 1);
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
        }
    };

    const clearCart = () => {
        setCartItems([]);
        setCartCount(0);
    };


    return (
        <CartContext.Provider
            value={{
                cartCount,
                cartItems,
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

export { CartContext, CartProvider };
