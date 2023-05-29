import React, {useContext} from "react";
import {Link} from "react-router-dom";
import Rating from "../rating/Rating"
import {CartContext} from "../../AppContext";
import "./ProductCard.css";

function ProductCard({product}) {
    const {addToCart} = useContext(CartContext);

    const handleAddToCart = () => {
        addToCart(product);
    };

    return (
        <div className="product-container">
            <div className="product-card">
                <Link to={`/product/${product.name}`}>
                    <img src={product.image} alt={product.name}/>
                </Link>
                <p className="product-name">{product.name}</p>
                <p className="product-desc">{product.description}</p>
                <div className="product-rating"><Rating rate={product.rating}/> 
                <Link to={`/feedback`}>
                    ({product.numRatings})
                </Link>
                </div>
                <p className="product-price">$ {(product.price).toFixed(2)}</p>
                <button className="add-to-cart-button" onClick={handleAddToCart}>Add to cart</button>
            </div>
        </div>
    );
}

export default ProductCard;
