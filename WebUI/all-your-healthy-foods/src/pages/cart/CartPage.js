import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { CartContext } from './CartContext';
import CartSummary from './CartSummary';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faMinus, faPlus } from '@fortawesome/free-solid-svg-icons';
import './Cart.css';

function CartPage() {
    const { sortedGroupedProducts, removeFromCart, increaseCount, decreaseCount } = useContext(CartContext);

    const handleRemoveItem = (item) => {
        removeFromCart(item);
    };

    const handleIncreaseCount = (itemName) => {
        increaseCount(itemName);
    };

    const handleDecreaseCount = (itemName) => {
        decreaseCount(itemName);
    };

    return (
        <div className="cart">
            <h2>Your Cart</h2>
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
                <CartSummary extra={<div>
                    <Link className="next" to="/checkout">Checkout</Link>
                    <Link className="back-to-home" to="/">Continue Shopping</Link>
                </div>} />
            )}
        </div>
    );
}

export default CartPage;
