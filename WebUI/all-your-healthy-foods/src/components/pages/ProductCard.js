import React from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboardList } from '@fortawesome/free-solid-svg-icons';
import Rating from './Rating'
import './Product.css';

function ProductCard({ product }) {
    return (
        <div className='product-container'>
            <div className='product-card'>
                <img src={product.image} alt={product.name} />
                <p className='product-name'>{product.name}</p>
                <p className='product-desc'>{product.description}</p>
                <div className='product-rating'><Rating rate={product.rating} /> ({product.numRatings})</div>
                <p className='product-price'>$ {product.price}</p>
                <button className='add-to-cart-button'>Add to cart</button>
                <button class='save-to-list-button'><FontAwesomeIcon className='shopping-list' icon={faClipboardList} />Save to list</button>
            </div>
        </div>
    );
}

export default ProductCard;