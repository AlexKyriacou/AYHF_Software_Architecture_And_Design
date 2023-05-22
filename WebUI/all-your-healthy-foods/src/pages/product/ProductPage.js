import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboardList, faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import Rating from '../rating/Rating';
import { CartContext } from '../cart/CartContext';
import productData from './productData';
import './ProductCard.css'
import './ProductPage.css';

function ProductPage() {
    const { productName } = useParams();
    const product = productData.find(product => product.name === productName);

    const { addToCart } = useContext(CartContext);

    const handleAddToCart = () => {
        addToCart(product);
    };

    return (
        <div className="product-overview-container">
            <div className='overview-card'>
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
            </div>
        </div>
    );
}

export default ProductPage;
