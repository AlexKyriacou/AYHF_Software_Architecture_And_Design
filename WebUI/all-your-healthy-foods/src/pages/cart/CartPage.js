import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { CartContext } from './CartContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faMinus, faPlus } from '@fortawesome/free-solid-svg-icons';
import './Cart.css';

function calculateTotal(cartItems, promotionPercentage) {
    let subtotal = 0;
    for (const item of cartItems) {
        subtotal += parseFloat(item.price) * item.count;
    }

    const promotionAmount = subtotal * (promotionPercentage / 100);
    const total = subtotal - promotionAmount;

    return {
        subtotal: subtotal.toFixed(2),
        promotionAmount: promotionAmount.toFixed(2),
        total: total.toFixed(2)
    };
}

function CartPage() {
    const { cartItems, removeFromCart, increaseCount, decreaseCount } = useContext(CartContext);
    const promotionPercentage = 10; // Assuming a 10% promotion for this example
    const { subtotal, promotionAmount, total } = calculateTotal(Object.values(cartItems), promotionPercentage);

    const handleRemoveItem = (item) => {
        removeFromCart(item);
    };

    const handleIncreaseCount = (itemName) => {
        increaseCount(itemName);
    };

    const handleDecreaseCount = (itemName) => {
        decreaseCount(itemName);
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
        <div className="cart">
            <h2>Your CartPage</h2>
            {Object.keys(sortedGroupedProducts).length === 0 ? (
                <div>
                    <p>Your cart is empty.</p>
                    <Link className="back-to-home" to="/">Continue Shopping</Link>
                </div>
            ) : (
                <ul>
                    {Object.entries(sortedGroupedProducts).map(([name, item], index) => (
                        <li key={index}>
                            <div className="item-container">
                                <img src={item.image} alt={item.name} />
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
                                    <FontAwesomeIcon className="shopping-list" icon={faTrash} />
                                    Remove
                                </button>
                                <div>$ {(item.price * item.count).toFixed(2)}</div>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
            {Object.keys(sortedGroupedProducts).length === 0 ? null : (
                <div className="summary-card">
                    <div className="summary-row">
                        <span>Item Breakdown</span>
                    </div>
                    {Object.entries(sortedGroupedProducts).map(([name, item]) => (
                        <div key={name} className="summary-row">
                            <span className="item-name">{name}</span>
                            <span className="item-count">x {item.count}</span>
                            <span className="item-price">${(item.price * item.count).toFixed(2)}</span>
                        </div>
                    ))}
                    <hr />
                    <div className="summary-row">
                        <span>Subtotal</span>
                        <span className="price">${subtotal}</span>
                    </div>
                    <div className="summary-row">
                        <span>Promotion</span>
                        <span className="price">- ${promotionAmount}</span>
                    </div>
                    <hr />
                    <div className="summary-row">
                        <span>Total</span>
                        <span className="price">${total}</span>
                    </div>

                    <button className="checkout-button">Checkout</button>
                    <Link className="back-to-home" to="/">Continue Shopping</Link>
                </div>
            )}
        </div>
    );
}

export default CartPage;
