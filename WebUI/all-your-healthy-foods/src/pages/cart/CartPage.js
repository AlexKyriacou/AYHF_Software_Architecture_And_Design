/**
 * This module contains the CartPage component, which displays the user's
 * shopping cart and provides functionality to manage its contents.
 * @module CartPage
 */

import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { CartContext } from "../../AppContext";
import OrderSummary from "../order/OrderSummary";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMinus, faPlus, faTrash } from "@fortawesome/free-solid-svg-icons";
import "./Cart.css";

/**
 * A stateless functional component that renders a page displaying the
 * current contents of the user's shopping cart. Cart items can be viewed
 * and modified, and the total price of the current cart contents is
 * displayed alongside links to continue shopping or checkout.
 *
 * @returns {JSX.Element} - The rendered CartPage component
 */
function CartPage() {
    const { sortedGroupedProducts, removeFromCart, increaseCount, decreaseCount } = useContext(CartContext);

    /**
     * Removes the specified item from the user's shopping cart
     *
     * @param {Object} item - The item to remove from the cart
     */
    const handleRemoveItem = (item) => {
        removeFromCart(item);
    };

    /**
     * Increases the count of the specified cart item by 1
     *
     * @param {string} itemName - The name of the item to increase the count of
     */
    const handleIncreaseCount = (itemName) => {
        increaseCount(itemName);
    };

    /**
     * Decreases the count of the specified cart item by 1
     *
     * @param {string} itemName - The name of the item to decrease the count of
     */
    const handleDecreaseCount = (itemName) => {
        decreaseCount(itemName);
    };

    return (
        <div className="cart">
            <h2>Your Cart</h2>
            {Object.keys(sortedGroupedProducts).length === 0 ? (
                <div>
                    <p>Your cart is empty.</p>
                    <Link className="link-button" to="/">Continue Shopping</Link>
                </div>
            ) : (
                <ul>
                    {Object.entries(sortedGroupedProducts).map(([name, item], index) => (
                        <li key={index}>
                            <div className="item-container">
                                <Link to={`/product/${item.name}`}>
                                    <img src={item.image} alt={item.name} />
                                </Link>
                                <p><strong>{name}</strong> - {item.description}</p>
                                <div className="count-buttons">
                                    <button
                                        className="count-button"
                                        onClick={() => handleDecreaseCount(item.name)}
                                        disabled={item.count === 1}
                                    >
                                        <FontAwesomeIcon icon={faMinus} />
                                    </button>
                                    <span className="count">{item.count}</span>
                                    <button className="count-button" onClick={() => handleIncreaseCount(item.name)}>
                                        <FontAwesomeIcon icon={faPlus} />
                                    </button>
                                </div>
                                <button className="remove-button" onClick={() => handleRemoveItem(item)}>
                                    <FontAwesomeIcon icon={faTrash} />
                                    Remove
                                </button>
                                <div>$ {(item.price * item.count).toFixed(2)}</div>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
            {Object.keys(sortedGroupedProducts).length === 0 ? null : (
                <OrderSummary extra={<div>
                    <Link className="link-button" to="/checkout">Checkout</Link>
                    <Link className="link-button" to="/">Continue Shopping</Link>
                </div>} />
            )}
        </div>
    );
}

export default CartPage;
