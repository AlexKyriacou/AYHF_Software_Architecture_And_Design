import React, {useContext, useState} from "react";
import {Link, useNavigate} from "react-router-dom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faArrowLeft} from "@fortawesome/free-solid-svg-icons";
import {ProductsContext, UserContext} from "../../AppContext";
import "./ProductPage.css";
import axios from "axios";

function AddProductPage() {
    const {loggedIn, user} = useContext(UserContext);
    const {products, setProducts} = useContext(ProductsContext);

    const [newProduct, setNewProduct] = useState({
        name: "",
        description: "",
        price: 0,
        longDescription: "",
        ingredients: "",
        image: "",
        rating: 0,
        numRatings: 0
    });

    const navigate = useNavigate();

    const handleInputChange = (event) => {
        setNewProduct({
            ...newProduct,
            [event.target.name]: event.target.value,
        });
    };

    const handleSave = async () => {
        try {
            const response = await axios.post("https://localhost:7269/products", newProduct);

            if (response.status === 200) {
                const newProductData = response.data;
                setProducts([...products, newProductData]); // Update the products in ProductsContext
                sessionStorage.setItem("products", JSON.stringify([...products, newProductData])); // Update the products in sessionStorage
                navigate("/");
            } else {
                throw new Error("Request failed");
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="add-product-container">
            <div className="add-product-card">
                <Link to="/" className="back-link">
                    <FontAwesomeIcon icon={faArrowLeft}/> Back
                </Link>
                <div className="add-product">
                    {loggedIn && user.role === "admin" && (
                        <div className="admin-buttons">
                            <button className="primary-button" onClick={handleSave}>
                                Save
                            </button>
                            <button className="primary-button" onClick={() => navigate("/")}>
                                Cancel
                            </button>
                        </div>
                    )}
                    <div className="product-info">
                        <div className="edit-fields">
                            <div className="field-container">
                                <label>Image URL/Path:</label>
                                <input
                                    type="text"
                                    name="image"
                                    value={newProduct.image}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="field-container">
                                <label>Name:</label>
                                <input
                                    type="text"
                                    name="name"
                                    value={newProduct.name}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="field-container">
                                <label>Description:</label>
                                <input
                                    type="text"
                                    name="description"
                                    value={newProduct.description}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="field-container">
                                <label>Price:</label>
                                <input
                                    type="number"
                                    name="price"
                                    value={newProduct.price}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="field-container">
                                <label>Long Description:</label>
                                <textarea
                                    name="longDescription"
                                    value={newProduct.longDescription}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="field-container">
                                <label>Ingredients:</label>
                                <textarea
                                    name="ingredients"
                                    value={newProduct.ingredients}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default AddProductPage;
