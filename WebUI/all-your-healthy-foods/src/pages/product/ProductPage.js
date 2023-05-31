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
    ); // Access the "products", "setProducts", and "getProductFeedbacks" functions from the "ProductsContext"
    const { addToCart } = useContext(CartContext); // Access the "addToCart" function from the "CartContext"
    const { loggedIn, user } = useContext(UserContext); // Access the "loggedIn" and "user" values from the "UserContext"

    const { productName } = useParams(); // Access the "productName" parameter from the URL

    const storedProduct = JSON.parse(sessionStorage.getItem("product"));
    const product = storedProduct && storedProduct.name === productName
        ? storedProduct
        : products.find((product) => product.name === productName); // Find the product object that matches the "productName"

    const productId = product.id; // Get the ID of the product
    const [editedProduct, setEditedProduct] = useState({ ...product }); // Define state variable "editedProduct" and its setter function
    const [inEditMode, setInEditMode] = useState(false); // Define state variable "inEditMode" and its setter function
    const [initialLoad, setInitialLoad] = useState(true); // Define state variable "initialLoad" and its setter function
    const [leaveFeedback, setLeaveFeedback] = useState(false); // Define state variable "leaveFeedback" and its setter function
    const [hasLeftFeedback, setHasLeftFeedback] = useState(false); // Define state variable "hasLeftFeedback" and its setter function
    const [rating, setRating] = useState(0); // Define state variable "rating" and its setter function
    const [feedbackMessage, setFeedbackMessage] = useState(""); // Define state variable "feedbackMessage" and its setter function
    const [productFeedbacks, setProductFeedbacks] = useState([]); // Define state variable "productFeedbacks" and its setter function
    const [averageRating, setAverageRating] = useState(0); // Define state variable "averageRating" and its setter function

    const navigate = useNavigate(); // Access the navigation function from the "react-router-dom" package

    useEffect(() => {
        // This effect runs when the component mounts and whenever "product" changes
        const fetchFeedbacks = async () => {
            const feedbackData = await getProductFeedbacks(product); // Fetch feedbacks for the selected product
            setProductFeedbacks(feedbackData.feedbacks); // Update the "productFeedbacks" state with the fetched feedbacks
            setAverageRating(feedbackData.averageRating); // Update the "averageRating" state with the fetched average rating
        };
        fetchFeedbacks(); // Call the fetchFeedbacks function
    }, [getProductFeedbacks, product]);

    useEffect(() => {
        // This effect runs when "initialLoad" changes
        if (initialLoad) {
            sessionStorage.setItem("product", JSON.stringify(product)); // Store the product object in the session storage
            setInEditMode(false); // Reset the "inEditMode" state to false
            setInitialLoad(false); // Reset the "initialLoad" state to false
        }
    }, [initialLoad, product]);

    useEffect(() => {
        // This effect runs when "productFeedbacks", "productId", or "user" changes
        if (user) {
            const userFeedback = productFeedbacks.find(
                (feedback) =>
                    feedback.productId === productId && feedback.userId === user.id
            ); // Find the feedback left by the current user for the selected product
            setHasLeftFeedback(!!userFeedback); // Update the "hasLeftFeedback" state based on whether the user left feedback or not
        }
    }, [productFeedbacks, productId, user]);

    const handleAddToCart = () => {
        addToCart(product); // Add the product to the cart
    };

    const handleInputChange = (event) => {
        setEditedProduct({
            ...editedProduct,
            [event.target.name]: event.target.value,
        }); // Update the "editedProduct" state with the changed input value
    };

    const handleDelete = async () => {
        try {
            const response = await axios.delete(`https://localhost:7269/products/${productId}`); // Send a delete request to remove the product

            if (response.status !== 204) {
                throw new Error("Request failed");
            } else {
                const updatedProducts = products.filter(
                    (product) => product.id !== productId
                ); // Remove the deleted product from the "products" array

                setProducts(updatedProducts); // Update the "products" state with the updated array
                navigate("/"); // Navigate to the home page
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleSave = async () => {
        try {
            const response = await axios.put(`https://localhost:7269/products/${productId}`, editedProduct); // Send a put request to update the product

            if (response.status !== 204) {
                throw new Error("Request failed");
            } else {
                const updatedProducts = products.map((product) =>
                    product.id === productId ? editedProduct : product
                ); // Update the product in the "products" array

                setProducts(updatedProducts); // Update the "products" state with the updated array
                navigate("/"); // Navigate to the home page
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleFeedbackCheckboxChange = () => {
        setLeaveFeedback(!leaveFeedback); // Toggle the "leaveFeedback" state
    };

    const handleRatingChange = (event) => {
        setRating(parseInt(event.target.value)); // Update the "rating" state with the selected value
    };

    const handleFeedbackMessageChange = (event) => {
        setFeedbackMessage(event.target.value); // Update the "feedbackMessage" state with the changed value
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
            }; // Create an object with the feedback data

            const response = await axios.post(
                "https://localhost:7269/feedback",
                feedbackData
            ); // Send a post request to submit the feedback

            if (response.status !== 201) {
                throw new Error("Request failed");
            } else {
                const updatedFeedbacks = [...productFeedbacks, feedbackData]; // Add the new feedback to the existing feedbacks array

                setProductFeedbacks(updatedFeedbacks); // Update the "productFeedbacks" state with the updated array
                setLeaveFeedback(false); // Reset the "leaveFeedback" state
                setRating(0); // Reset the "rating" state
                setFeedbackMessage(""); // Reset the "feedbackMessage" state
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
                                <details>
                                    <summary>Ingredients</summary>
                                    <p>{product.ingredients}</p>
                                </details>
                            </div>
                        )}
                    </div>
                </div>
            </div>
            {loggedIn && leaveFeedback && (
                <div className="feedback-form-container">
                    <h2>Leave Feedback</h2>
                    <div className="feedback-form">
                        <div className="rating-container">
                            <label>Rating:</label>
                            <Rating rate={rating} onChange={handleRatingChange} />
                        </div>
                        <div className="message-container">
                            <label>Message:</label>
                            <textarea
                                name="feedbackMessage"
                                value={feedbackMessage}
                                onChange={handleFeedbackMessageChange}
                            />
                        </div>
                        <button className="primary-button" onClick={handleLeaveReview}>
                            Submit Feedback
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default ProductPage;
