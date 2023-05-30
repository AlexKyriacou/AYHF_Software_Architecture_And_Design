import React, { useState, useEffect, useContext } from "react";
import { useParams } from "react-router-dom";
import { ProductsContext } from "../../AppContext";
import axios from "axios";
import "./Feedback.css";

function Feedback() {
    const { productName } = useParams();
    const { products } = useContext(ProductsContext);
    const [product, setProduct] = useState(null);
    const [feedbacks, setFeedbacks] = useState([]);

    useEffect(() => {
        const selectedProduct = products.find((product) => product.name === productName);
        setProduct(selectedProduct);
    }, [products, productName]);

    useEffect(() => {
        if (product) {
            const fetchFeedbacks = async () => {
                try {
                    const response = await axios.get(
                        `https://localhost:7269/Products/${product.id}/feedback`
                    );

                    if (response.status === 200) {
                        setFeedbacks(response.data);
                    } else {
                        throw new Error("Failed to fetch feedbacks");
                    }
                } catch (error) {
                    console.error(error);
                }
            };

            fetchFeedbacks();
        }
    }, [product]);

    const formatDate = (dateString) => {
        const options = { year: "numeric", month: "long", day: "numeric" };
        const date = new Date(dateString);
        return date.toLocaleDateString(undefined, options);
    };

    return (
        <div className="feedback-container">
            <div className="feedback-box">
                <h1 className="feedback-title">Feedback</h1>
                {product && (
                    <div className="product-details">
                        <h2 className="product-name">{product.name}</h2>
                        <div className="product-image">
                            <img src={product.image} alt={product.name} />
                        </div>
                        <p className="product-description">{product.description}</p>
                    </div>
                )}
            </div>
            <ul className="feedback-list">
                {feedbacks.map((feedback) => (
                    <li key={feedback.id} className="feedback-item">
                        <p className="customer-id">Customer ID: {feedback.userId}</p>
                        <p className="rating">Rating: {feedback.rating}</p>
                        <p>Message: {feedback.message}</p>
                        <p className="feedback-date">Feedback Date: {formatDate(feedback.feedbackDate)}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Feedback;
