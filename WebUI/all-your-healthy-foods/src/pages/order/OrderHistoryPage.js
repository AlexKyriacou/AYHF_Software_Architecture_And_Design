import React, { useEffect, useState, useContext } from "react";
import axios from "axios";
import { UserContext } from "../../AppContext";

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
        <div>
            <h2>Order History</h2>
            {orders.length === 0 ? (
                <p>No orders found.</p>
            ) : (
                <ul>
                    {orders.map((order) => (
                        <li key={order.id}>
                            <p>Order ID: {order.id}</p>
                            <p>Order Date: {order.orderDate}</p>
                            <p>Total Amount: ${order.totalAmount.toFixed(2)}</p>
                            <ul>
                                {order.products.map((product) => (
                                    <li key={product.id}>{product.name}</li>
                                ))}
                            </ul>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}

export default OrderHistoryPage;
