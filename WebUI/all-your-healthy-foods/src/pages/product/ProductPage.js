import React, {useContext, useState} from "react";
import {Link, useParams, useNavigate} from "react-router-dom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faArrowLeft} from "@fortawesome/free-solid-svg-icons";
import Rating from "../rating/Rating";
import {CartContext, UserContext} from "../../AppContext";
import productData from "../../testData/productData";
import "./ProductCard.css"
import "./ProductPage.css";
import axios from "axios";

function ProductPage() {
    const navigate = useNavigate();
    const {productName} = useParams();
    const product = productData.find(product => product.name === productName);
    const productId = product.id;
    const {addToCart} = useContext(CartContext);
    const [editedProduct, setEditedProduct] = useState({...product});


    const handleAddToCart = () => {
        addToCart(product);
    };

    const {loggedIn, user} = useContext(UserContext);
    const [inEditMode, setInEditMode] = useState(false);

    const handleEdit = () => {
        setInEditMode(true);
    };

    const handleInputChange = (event) => {
        setEditedProduct({
            ...editedProduct,
            [event.target.name]: event.target.value,
        });
    };

    const handleDelete = async () => {
        try {
            const response = axios.delete(`https://localhost:7269/products/${productId}`);

            if (!response.ok) {
                throw new Error("Request failed");
            }

            navigate("/products");
        } catch (error) {
            console.error(error);
        }
    };

    const handleSave = async () => {
        try {
            const response = axios.put(`https://localhost:7269/products/${productId}`, editedProduct);

            if (!response.ok) {
                throw new Error("Request failed");
            }

            navigate("/products");
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="product-overview-container">
            <div className="overview-card">
                <div className="image-container">
                    <div className="back-link-container">
                        <Link to="/" className="back-link">
                            <FontAwesomeIcon icon={faArrowLeft}/> Back
                        </Link>
                    </div>
                    <img className="large-view" src={product.image} alt={product.name}/>
                </div>
                <div className="product-overview-details">
                    <div className="product-info">
                        {inEditMode ? (
                            <input
                                type="text"
                                name="name"
                                value={editedProduct.name}
                                onChange={handleInputChange}
                            />
                        ) : (
                            <p className="product-name-desc">
                                {product.name} {product.description}
                            </p>
                        )}
                        <div className="product-rating">
                            <Rating rate={product.rating}/> ({product.numRatings})
                        </div>
                        {inEditMode ? (
                            <input
                                type="number"
                                name="price"
                                value={editedProduct.price}
                                onChange={handleInputChange}
                            />
                        ) : (
                            <p className="product-price">$ {(product.price).toFixed(2)}</p>
                        )}
                        <button className="add-to-cart-button" onClick={handleAddToCart}>
                            Add to cart
                        </button>
                    </div>
                    <details open>
                        <summary>Description</summary>
                        {inEditMode ? (
                            <textarea
                                name="longDescription"
                                value={editedProduct.longDescription}
                                onChange={handleInputChange}
                            />
                        ) : (
                            <p>{product.longDescription}</p>
                        )}
                    </details>
                    <details open>
                        <summary>Ingredients</summary>
                        {inEditMode ? (
                            <textarea
                                name="ingredients"
                                value={editedProduct.ingredients}
                                onChange={handleInputChange}
                            />
                        ) : (
                            <p>{product.ingredients}</p>
                        )}
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
                            <>
                                <button className="primary-button" onClick={handleSave}>
                                    Save
                                </button>
                                <button className="primary-button" onClick={() => setInEditMode(false)}>
                                    Cancel
                                </button>
                            </>
                        )}
                    </div>
                )}
            </div>
        </div>
    );
}

export default ProductPage;