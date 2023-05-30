import React, { useState, useEffect, useContext } from "react";
import { Link, useParams } from "react-router-dom";
import { ProductsContext } from "../../AppContext";
import "./Feedback.css";

function Feedback() {
    const { productName } = useParams();
    const { products } = useContext(ProductsContext);
    const [product, setProduct] = useState(null);
    const [feedbacks, setFeedbacks] = useState([]);
    const { getProductFeedbacks } = useContext(ProductsContext);

    useEffect(() => {
        const selectedProduct = products.find((product) => product.name === productName);
        setProduct(selectedProduct);
    }, [products, productName]);

    useEffect(() => {
        if (product) {
            const fetchFeedbacks = async () => {
                const feedbackData = await getProductFeedbacks(product);
                setFeedbacks(feedbackData.feedbacks);
            };
            fetchFeedbacks();
        }
    }, [getProductFeedbacks, product]);

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
                        <Link to={`/product/${product.name}`}>
                            <img src={product.image} alt={product.name} />
                        </Link>
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
