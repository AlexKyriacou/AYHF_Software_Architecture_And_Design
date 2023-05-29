import React, {useContext} from "react";
import ProductCard from "../product/ProductCard";
import {ProductsContext, UserContext} from "../../AppContext";
import {Link} from "react-router-dom";
import "./Home.css";

const HomePage = () => {
    const {products} = useContext(ProductsContext);
    const {loggedIn, user} = useContext(UserContext);

    return (
        <div>
            <p className="welcome">Welcome to All Your Healthy Foods Online Store</p>
            {loggedIn && user.role === "admin" && (
                <div className="admin-buttons">
                    <Link to="/add-product" className="add-product link-button">
                        Add Product
                    </Link>
                </div>
            )}
            <div className="products-container">
                {products.map((product, index) => (
                    <ProductCard key={index} product={product}/>
                ))}
            </div>
        </div>
    );
};

export default HomePage;
