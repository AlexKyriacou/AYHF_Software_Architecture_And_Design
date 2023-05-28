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
              <FontAwesomeIcon icon={faArrowLeft} /> Back
            </Link>
          </div>
          <img className="large-view" src={product.image} alt={product.name} />
        </div>
        <div className="product-overview-details">
          <div className="product-info">
            {inEditMode ? (
              <div className="product-name-desc">
                <input
                  type="text"
                  name="name"
                  value={editedProduct.name}
                  onChange={handleInputChange}
                />
                <input
                  type="text"
                  name="name"
                  value={editedProduct.description}
                  onChange={handleInputChange}
                />
              </div>
            ) : (
              <div className="product-name-desc">
                <p>{product.name}</p>
                <p>&nbsp;-&nbsp;</p>
                <p>{product.description}</p>
              </div>
            )}
            {!inEditMode && (
              <div className="product-rating">
                <Rating rate={product.rating} /> ({product.numRatings})
              </div>
            )}
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
            {!inEditMode && (
              <button className="add-to-cart-button" onClick={handleAddToCart}>
                Add to cart
              </button>
            )}
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
      </div>
    </div>
  );
}

export default ProductPage;
