import React, { useContext } from "react";
import ProductCard from "../product/ProductCard";
import { ProductsContext, UserContext } from "../../AppContext";
import { Link } from "react-router-dom";
import "./Home.css";

const HomePage = () => {
    const { products, feedback } = useContext(ProductsContext);
    const { loggedIn, user } = useContext(UserContext);

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

    return (
        <div>
            <p className="welcome">Welcome to All Your Healthy Foods Online Store</p>
            {loggedIn && user.role === 0 && (
                <div className="admin-buttons">
                    <Link to="/add-product" className="add-product link-button">
                        Add Product
                    </Link>
                </div>
            )}
            <div className="products-container">
                {addFeedbackToProducts().map((product, index) => (
                    <ProductCard key={index} product={product} />
                ))}
            </div>
        </div>
    );
};

export default HomePage;
