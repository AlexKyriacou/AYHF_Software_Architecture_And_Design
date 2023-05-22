import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboardList } from '@fortawesome/free-solid-svg-icons';
import Rating from '../rating/Rating'
import { CartContext } from '../cart/CartContext';
import './ProductCard.css';

function ProductCard({ product }) {
    const { addToCart } = useContext(CartContext);

    const handleAddToCart = () => {
        addToCart(product);
    };

    return (
        <div className='product-container'>
            <div className='product-card'>
                <Link to={`/product/${product.name}`}>
                    <img src={product.image} alt={product.name} />
                </Link>
                <p className='product-name'>{product.name}</p>
                <p className='product-desc'>{product.description}</p>
                <div className='product-rating'><Rating rate={product.rating} /> ({product.numRatings})</div>
                <p className='product-price'>$ {(product.price).toFixed(2)}</p>
                <button className='add-to-cart-button' onClick={handleAddToCart}>Add to cart</button>
                <button className='save-to-list-button'><FontAwesomeIcon className='shopping-list' icon={faClipboardList} />Save to list</button>
            </div>
        </div>
    );
}

export default ProductCard;