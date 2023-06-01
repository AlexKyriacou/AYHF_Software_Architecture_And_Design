import React, { useContext, useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import Rating from "../rating/Rating";
import { CartContext, ProductsContext, UserContext } from "../../AppContext";
import "./Product.css";
import axios from "axios";

function ProductPage() {
    const { products, setProducts, getProductFeedbacks } = useContext(
        ProductsContext
    );
    const { addToCart } = useContext(CartContext);
    const { loggedIn, user } = useContext(UserContext);

    const { productName } = useParams();

    const storedProduct = JSON.parse(sessionStorage.getItem("product"));
    const product = storedProduct && storedProduct.name === productName
        ? storedProduct
        : products.find((product) => product.name === productName);

    const productId = product.id;
    const [editedProduct, setEditedProduct] = useState({ ...product });
    const [inEditMode, setInEditMode] = useState(false);
    const [initialLoad, setInitialLoad] = useState(true);
    const [leaveFeedback, setLeaveFeedback] = useState(false);
    const [hasLeftFeedback, setHasLeftFeedback] = useState(false);
    const [rating, setRating] = useState(0);
    const [feedbackMessage, setFeedbackMessage] = useState("");
    const [productFeedbacks, setProductFeedbacks] = useState([]);
    const [averageRating, setAverageRating] = useState(0);

    const navigate = useNavigate();

    useEffect(() => {
        const fetchFeedbacks = async () => {
            const feedbackData = await getProductFeedbacks(product);
            setProductFeedbacks(feedbackData.feedbacks);
            setAverageRating(feedbackData.averageRating);
        };
        fetchFeedbacks();
    }, [getProductFeedbacks, product]);

    useEffect(() => {
        if (initialLoad) {
            sessionStorage.setItem("product", JSON.stringify(product));
            setInEditMode(false);
            setInitialLoad(false);
        }
    }, [initialLoad, product]);

    useEffect(() => {
        if (user) {
            const userFeedback = productFeedbacks.find(
                (feedback) =>
                    feedback.productId === productId && feedback.userId === user.id
            );
            setHasLeftFeedback(!!userFeedback);
        }
    }, [productFeedbacks, productId, user]);

    const handleAddToCart = () => {
        addToCart(product);
    };

    const handleInputChange = (event) => {
        setEditedProduct({
            ...editedProduct,
            [event.target.name]: event.target.value,
        });
    };

    const handleDelete = async () => {
        try {
            const response = await axios.delete(`https://localhost:7269/products/${productId}`);

            if (response.status !== 204) {
                throw new Error("Request failed");
            } else {
                const updatedProducts = products.filter(
                    (product) => product.id !== productId
                );

                setProducts(updatedProducts);
                navigate("/");
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleSave = async () => {
        try {
            const response = await axios.put(`https://localhost:7269/products/${productId}`, editedProduct);

            if (response.status !== 204) {
                throw new Error("Request failed");
            } else {
                const updatedProducts = products.map((product) =>
                    product.id === productId ? editedProduct : product
                );
                setProducts(updatedProducts);
                navigate("/");
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleFeedbackCheckboxChange = () => {
        setLeaveFeedback(!leaveFeedback);
    };

    const handleRatingChange = (event) => {
        setRating(parseInt(event.target.value));
    };

    const handleFeedbackMessageChange = (event) => {
        setFeedbackMessage(event.target.value);
    };

    const handleLeaveReview = async () => {
        try {
            const feedbackData = {
                id: 0,
                userId: user?.id,
                rating: rating,
                productId: productId,
                message: feedbackMessage,
                feedbackDate: new Date().toISOString(),
            };

            const response = await axios.post(
                "https://localhost:7269/feedback",
                feedbackData
            );

            if (response.status !== 201) {
                throw new Error("Request failed");
            } else {
                const updatedFeedbacks = [...productFeedbacks, feedbackData];
                setProductFeedbacks(updatedFeedbacks);
                setLeaveFeedback(false);
                setRating(0);
                setFeedbackMessage("");
            }
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
                            <FontAwesomeIcon icon={faArrowLeft} /> Back
                        </Link>
                    </div>
                    <img className="large-view" src={product.image} alt={product.name} />
                </div>
                <div className="product-overview-details">
                    {loggedIn && user.role === 0 && (
                        <div className="admin-buttons">
                            {!inEditMode && (
                                <>
                                    <button className="primary-button" onClick={() => setInEditMode(true)}>
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
                    <div className="product-info">
                        {inEditMode ? (
                            <div className="edit-fields">
                                <div className="field-container">
                                    <label>Name:</label>
                                    <input
                                        type="text"
                                        name="name"
                                        value={editedProduct.name}
                                        onChange={handleInputChange}
                                    />
                                </div>
                                <div className="field-container">
                                    <label>Description:</label>
                                    <input
                                        type="text"
                                        name="description"
                                        value={editedProduct.description}
                                        onChange={handleInputChange}
                                    />
                                </div>
                                <div className="field-container">
                                    <label>Price:</label>
                                    <input
                                        type="number"
                                        name="price"
                                        value={editedProduct.price}
                                        onChange={handleInputChange}
                                    />
                                </div>
                                <div className="field-container">
                                    <label>Long Description:</label>
                                    <textarea
                                        name="longDescription"
                                        value={editedProduct.longDescription}
                                        onChange={handleInputChange}
                                    />
                                </div>
                                <div className="field-container">
                                    <label>Ingredients:</label>
                                    <textarea
                                        name="ingredients"
                                        value={editedProduct.ingredients}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                        ) : (
                            <div>
                                <div className="product-name-desc">
                                    <p>{product.name}</p>
                                    <p>{product.description}</p>
                                </div>
                                <div className="product-rating">
                                    <Rating rate={averageRating} />
                                    <Link to={`/product/${product.name}/feedbacks`}>
                                        ({productFeedbacks.length})
                                    </Link>
                                </div>
                                {loggedIn && !hasLeftFeedback && (
                                    <div className="leave-feedback">
                                        <label>
                                            <input
                                                type="checkbox"
                                                checked={leaveFeedback}
                                                onChange={handleFeedbackCheckboxChange}
                                            />
                                            Leave Feedback?
                                        </label>
                                    </div>
                                )}
                                <p className="product-price">
                                    $ {(product.price).toFixed(2)}
                                </p>
                                <button
                                    className="add-to-cart-button"
                                    onClick={handleAddToCart}
                                >
                                    Add to cart
                                </button>
                                <details open>
                                    <summary>Description</summary>
                                    <p>{product.longDescription}</p>
                                </details>
                                <details open>
                                    <summary>Ingredients</summary>
                                    <p>{product.ingredients}</p>
                                </details>
                                {leaveFeedback && (
                                    <div className="feedback-fields">
                                        <div className="field-container">
                                            <label>Rating:</label>
                                            <input
                                                type="number"
                                                name="rating"
                                                value={rating}
                                                onChange={handleRatingChange}
                                                min="1"
                                                max="5"
                                                required
                                            />
                                        </div>
                                        <div className="field-container">
                                            <label>Feedback Message:</label>
                                            <textarea
                                                name="feedbackMessage"
                                                value={feedbackMessage}
                                                onChange={handleFeedbackMessageChange}
                                            />
                                        </div>
                                        <button
                                            className="primary-button"
                                            onClick={handleLeaveReview}
                                        >
                                            Save
                                        </button>
                                    </div>
                                )}
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ProductPage;