import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Rating from "../rating/Rating";
import { CartContext, ProductsContext } from "../../AppContext";
import "./Product.css";

function ProductCard({ product }) {
    const { getProductFeedbacks } = useContext(ProductsContext);
    const { addToCart } = useContext(CartContext);

    const [productFeedbacks, setProductFeedbacks] = useState([]);
    const [averageRating, setAverageRating] = useState(0);

    useEffect(() => {
        // Fetch product feedbacks and average rating when the component mounts or product prop changes
        const fetchFeedbacks = async () => {
            const feedbackData = await getProductFeedbacks(product);
            setProductFeedbacks(feedbackData.feedbacks);
            setAverageRating(feedbackData.averageRating);
        };
        fetchFeedbacks();
    }, [getProductFeedbacks, product]);

    const handleAddToCart = () => {
        // Add product to the cart
        addToCart(product);
    };

    return (
        <div className="product-container">
            <div className="product-card">
                <Link to={`/product/${product.name}`}>
                    <img src={product.image} alt={product.name} />
                </Link>
                <p className="product-name">{product.name}</p>
                <p className="product-desc">{product.description}</p>
                <div className="product-rating">
                    <Rating rate={averageRating} />
                    <Link to={`/product/${product.name}/feedbacks`}>
                        ({productFeedbacks.length})
                    </Link>
                </div>
                <p className="product-price">$ {(product.price).toFixed(2)}</p>
                <button className="add-to-cart-button" onClick={handleAddToCart}>
                    Add to cart
                </button>
            </div>
        </div>
    );
}

export default ProductCard;
