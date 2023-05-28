import React, { useContext } from "react";
import ProductCard from "../product/ProductCard";
import { ProductsContext } from "../../AppContext";
import "./Home.css";

const HomePage = () => {
    const { products } = useContext(ProductsContext);

    return (
        <div>
            <p className="welcome">Welcome to All Your Healthy Foods Online Store</p>
            <div className="products-container">
                {products.map((product, index) => (
                    <ProductCard key={index} product={product} />
                ))}
            </div>
        </div>
    );
};

export default HomePage;
