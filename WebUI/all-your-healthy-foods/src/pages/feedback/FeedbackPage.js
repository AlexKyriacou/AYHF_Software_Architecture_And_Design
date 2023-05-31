import React, { useState, useEffect, useContext } from "react";
import { Link, useParams } from "react-router-dom";
import { ProductsContext } from "../../AppContext";
import "./Feedback.css";

function Feedback() {
    const { productName } = useParams(); // Access the "productName" parameter from the URL
    const { products } = useContext(ProductsContext); // Access the "products" context from the "ProductsContext"
    const [product, setProduct] = useState(null); // Define state variable "product" and its setter function
    const [feedbacks, setFeedbacks] = useState([]); // Define state variable "feedbacks" and its setter function
    const { getProductFeedbacks } = useContext(ProductsContext); // Access the "getProductFeedbacks" function from the "ProductsContext"

    useEffect(() => {
        // This effect runs when the "products" or "productName" values change
        const selectedProduct = products.find((product) => product.name === productName); // Find the product object that matches the "productName"
        setProduct(selectedProduct); // Update the "product" state with the selected product
    }, [products, productName]);

    useEffect(() => {
        // This effect runs when the "product" value changes
        if (product) {
            const fetchFeedbacks = async () => {
                const feedbackData = await getProductFeedbacks(product); // Fetch feedbacks for the selected product
                setFeedbacks(feedbackData.feedbacks); // Update the "feedbacks" state with the fetched feedbacks
            };
            fetchFeedbacks(); // Call the fetchFeedbacks function
        }
    }, [getProductFeedbacks, product]);

    const formatDate = (dateString) => {
        // Function to format a date string
        const options = { year: "numeric", month: "long", day: "numeric" };
        const date = new Date(dateString);
        return date.toLocaleDateString(undefined, options); // Format the date according to the specified options
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
                    // Render each feedback in the "feedbacks" array as a list item
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
