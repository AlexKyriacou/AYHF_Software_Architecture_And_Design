import React, { useContext, useState } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import Rating from "../rating/Rating";
import { CartContext, UserContext, ProductsContext } from "../../AppContext";
import "./ProductCard.css";
import "./ProductPage.css";
import axios from "axios";

function ProductPage() {
  const { products } = useContext(ProductsContext);

  const { productName } = useParams();
  const product = products.find(product => product.name === productName);
  const productId = product.id;
  const { addToCart } = useContext(CartContext);
  const [editedProduct, setEditedProduct] = useState({ ...product });

  const navigate = useNavigate();

  const handleAddToCart = () => {
    addToCart(product);
  };

  const { loggedIn, user } = useContext(UserContext);
  const [inEditMode, setInEditMode] = useState(false);

  const handleInputChange = (event) => {
    setEditedProduct({
      ...editedProduct,
      [event.target.name]: event.target.value,
    });
  };

  const handleDelete = async () => {
    try {
      const response = await axios.delete(`https://localhost:7269/products/${productId}`);

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
      const response = await axios.put(`https://localhost:7269/products/${productId}`, editedProduct);

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
              <FontAwesomeIcon icon={faArrowLeft} /> Back
            </Link>
          </div>
          <img className="large-view" src={product.image} alt={product.name} />
        </div>
        <div className="product-overview-details">
          {loggedIn && user.role === "admin" && (
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
                  <Rating rate={product.rating} /> ({product.numRatings})
                </div>
                <p className="product-price">$ {(product.price).toFixed(2)}</p>
                <button className="add-to-cart-button" onClick={handleAddToCart}>
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
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductPage;
