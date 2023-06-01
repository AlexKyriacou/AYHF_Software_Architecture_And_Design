import React, { useContext } from "react";
import { Link, useParams } from "react-router-dom";
import { ProductsContext } from "../../AppContext";
import "./Feedback.css";

function Feedback() {
    const { productName } = useParams();
    const { products, feedback } = useContext(ProductsContext);

    const addFeedbackToProducts = () => {
        if (products.length > 0 && feedback.length > 0) {
            const productsWithFeedback = products.map((product) => ({
                ...product,
                feedback: feedback.filter((item) => item.productId === product.id),
            }));
            return productsWithFeedback;
        } else {
            return [];
        }
    };

    const product = addFeedbackToProducts().find((product) => product.name === productName);

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
                {product && product.feedback && product.feedback.map((feedback) => (
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
