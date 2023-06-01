import React, { useContext } from "react";
import { Link } from "react-router-dom";
import Rating from "../rating/Rating";
import { CartContext } from "../../AppContext";
import "./Product.css";

function ProductCard({ product }) {
    const { addToCart } = useContext(CartContext);

    const handleAddToCart = () => {
        // Add product to the cart
        addToCart(product);
    };

    const calculateAverageRating = () => {
        if (product && product.feedback) {
            const totalRating = product.feedback.reduce(
                (total, feedback) => total + feedback.rating,
                0
            );
            return totalRating / product.feedback.length;
        } else {
            return 0;
        }
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
                    <Rating rate={calculateAverageRating()} />
                    <Link to={`/product/${product.name}/feedbacks`}>
                        ({product?.feedback?.length ?? 0})
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
