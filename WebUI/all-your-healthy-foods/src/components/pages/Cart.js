import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { CartContext } from './CartContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faMinus, faPlus } from '@fortawesome/free-solid-svg-icons';
import './Cart.css';

function calculateTotal(cartItems) {
    let total = 0;
    for (const item of cartItems) {
        total += parseFloat(item.price) * item.count;
    }
    return total.toFixed(2);
}

function Cart() {
    const { cartItems, removeFromCart, increaseCount, decreaseCount } = useContext(CartContext);
    const total = calculateTotal(Object.values(cartItems));

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

    return (
        <div>
            <h2>Your Cart</h2>
            {Object.keys(groupedProducts).length === 0 ? (
                <p>Your cart is empty.</p>
            ) : (
                <ul>
                    {Object.entries(groupedProducts).map(([name, item], index) => (
                        <li key={index}>
                            <div>
                                <img src={item.image} alt={item.name} />
                                <div>
                                    <p>{name} - {item.description}</p>
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
                                    <button className='remove-button' onClick={() => handleRemoveItem(item)}>
                                        <FontAwesomeIcon className='shopping-list' icon={faTrash} />
                                        Remove
                                    </button>
                                </div>
                                <div>$ {item.price}</div>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
            <div className='summary-card'>
                <p>Subtotal</p>
                <p>Promotion</p>
                <hr></hr>
                <p>Total {total}</p>
                <button className='checkout-button'>Checkout</button>
                <Link className='back-to-home' to="/">Continue Shopping</Link>
            </div>
        </div>
    );
}

export default Cart;
