import React, {useContext} from "react";
import {CartContext} from "../../AppContext";
import "./Cart.css";

function calculateTotal(cartItems, promotionPercentage, shipping) {
    let subtotal = 0;
    for (const item of cartItems) {
        subtotal += parseFloat(item.price) * item.count;
    }

    const promotionAmount = subtotal * (promotionPercentage / 100);
    let total = subtotal - promotionAmount;

    if (shipping) {
        total += shipping;
    }

    return {
        subtotal: subtotal.toFixed(2),
        promotionAmount: promotionAmount.toFixed(2),
        total: total.toFixed(2)
    };
}

function OrderSummary({extra}) {
    const {cartItems, sortedGroupedProducts} = useContext(CartContext);
    const promotionPercentage = 10; // Assuming a 10% promotion for this example
    const shipping = 10; //TODO Calculate Shipping based on address (if necessary)
    const {subtotal, promotionAmount, total} = calculateTotal(Object.values(cartItems), promotionPercentage, shipping);

    return (
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
            <hr/>
            <div className="summary-row">
                <span>Subtotal</span>
                <span className="price">${subtotal}</span>
            </div>
            <div className="summary-row">
                <span>Promotion</span>
                <span className="price">- ${promotionAmount}</span>
            </div>
            <div className="summary-row">
                <span>Shipping</span>
                <span className="price">+ ${shipping}</span>
            </div>
            <hr/>
            <div className="summary-row">
                <span>Total</span>
                <span className="price">${total}</span>
            </div>
            <span className="extra">{extra}</span>
        </div>
    );
}

export default OrderSummary;
