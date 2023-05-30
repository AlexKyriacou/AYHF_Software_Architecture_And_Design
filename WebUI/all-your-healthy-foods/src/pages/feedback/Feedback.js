import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import {useContext} from "react";
import { ProductsContext } from "../../AppContext";
import axios from "axios";

function Feedback() {
    const { productName } = useParams();
    const { products } = useContext(ProductsContext);
    const [product, setProduct] = useState(null);
    const [feedbacks, setFeedbacks] = useState([]);

    useEffect(() => {
        setProduct(products.find((product) => product.name === productName));
        fetchFeedbacks();      
    }, [products, productName]);
    
      useEffect(() => {
        
      }, [product]);

  const fetchFeedbacks = async () => {
    if (!product) return;

    try {
      const response = await axios.get(
        `https://localhost:7269/Products/${product.id}/Feedbacks`
      );

      if (response.status === 200) {
        setFeedbacks(response.data);
      } else {
        throw new Error("Failed to fetch feedbacks");
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <h1>Feedbacks</h1>
      {product && (
        <div>
          <h2>Product: {product.name}</h2>
          <p>Description: {product.description}</p>
        </div>
      )}
      <ul>
        {feedbacks.map((feedback) => (
          <li key={feedback.id}>
            <p>Customer ID: {feedback.customerId}</p>
            <p>Rating: {feedback.rating}</p>
            <p>Product ID: {feedback.productId}</p>
            <p>Message: {feedback.message}</p>
            <p>Feedback Date: {feedback.feedbackDate}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Feedback;
