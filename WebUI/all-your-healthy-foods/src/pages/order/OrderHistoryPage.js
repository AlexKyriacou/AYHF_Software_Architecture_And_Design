import React, { useEffect, useState, useContext } from "react";
import axios from "axios";
import { UserContext } from "../../AppContext";
import { Link } from "react-router-dom";
import "./Order.css";

function OrderHistoryPage() {
    const { user } = useContext(UserContext);
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        const fetchOrders = async () => {
            try {
                const response = await axios.get(
                    `https://localhost:7269/orders?userId=${user.id}`
                );

                if (response.data && response.data.length > 0) {
                    setOrders(response.data);
                }
            } catch (error) {
                console.error(error);
            }
        };

        fetchOrders();
    }, [user.id]);

    return (
        <div className="order-history-container">
            <h2>Order History</h2>
            {orders.length === 0 ? (
                <p className="no-orders">No orders found.</p>
            ) : (
                <ul className="order-list">
                    {orders.map((order) => (
                        <li key={order.id} className="order-item">
                            <p className="order-id">Order ID: {order.id}</p>
                            <p className="order-date">Order Date: {order.orderDate}</p>
                            <p className="order-amount">
                                Total Amount: ${order.totalAmount.toFixed(2)}
                            </p>
                            <ul className="product-list">
                                {order.products.map((product) => (
                                    <li key={product.id}>
                                        <div className="product">
                                            <Link to={`/product/${product.name}`}>
                                                <img src={product.image} alt={product.name} />
                                            </Link>
                                            <p>{product.name}</p>
                                        </div>
                                    </li>
                                ))}
                            </ul>
                            <hr />
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}

export default OrderHistoryPage;
