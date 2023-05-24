import React, { useContext, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboardList, faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import Rating from "../rating/Rating";
import { CartContext } from "../../AppContext";
import { UserContext } from "../../AppContext";
import productData from "../../testData/productData";
import "./ProductCard.css"
import "./ProductPage.css";

function ProductPage() {
    const { productName } = useParams();
    const product = productData.find(product => product.name === productName);

    const { addToCart } = useContext(CartContext);

    const handleAddToCart = () => {
        addToCart(product);
    };

    const { loggedIn, user } = useContext(UserContext);
    const [inEditMode, setInEditMode] = useState(false);

    const handleEdit = () => {
        setInEditMode(true);
    };

    const handleDelete = () => {
        // Handle delete logic here
        console.log("Delete this product: ", productName)
    };

    const handleSave = () => {
        setInEditMode(false);
        console.log("Saving changes to: ", productName)
    }

    return (
        <div className="product-overview-container">
            <div className="overview-card">
                <div className="image-container">
                    <div className="back-link-container">
                        <Link to="/" className="back-link">
                            <FontAwesomeIcon icon={faArrowLeft} /> Back
                        </Link>
                    </div>
                    <img className="large-view" src={product.image} alt={product.name} />
                </div>
                <div className="product-overview-details">
                    <div className="product-info">
                        <p className="product-name-desc">{product.name} {product.description}</p>
                        <div className="product-rating">
                            <Rating rate={product.rating} /> ({product.numRatings})
                        </div>
                        <p className="product-price">$ {(product.price).toFixed(2)}</p>
                        <button className="add-to-cart-button" onClick={handleAddToCart}>Add to cart</button>
                        <button className="save-to-list-button">
                            <FontAwesomeIcon className="shopping-list" icon={faClipboardList} />
                            Save to list
                        </button>
                    </div>
                    <details open>
                        <summary>
                            Description
                        </summary>
                        <p>{product.longDescription}</p>
                    </details>
                    <details open>
                        <summary>
                            Ingredients
                        </summary>
                        <p>{product.ingredients}</p>
                    </details>
                </div>
                {loggedIn && user.role === "admin" && (
                    <div className="admin-buttons">
                        {!inEditMode && (
                            <>
                                <button className="primary-button" onClick={handleEdit}>
                                    Edit
                                </button>
                                <button className="primary-button" onClick={handleDelete}>
                                    Delete
                                </button>
                            </>
                        )}
                        {inEditMode && (
                            <button className="primary-button" onClick={handleSave}>
                                Save
                            </button>
                        )}
                    </div>
                )}
            </div>
        </div>
    );
}

export default ProductPage;
