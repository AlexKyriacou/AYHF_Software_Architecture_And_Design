import React from "react";
import ProductCard from "../product/ProductCard";
import {useLocation} from "react-router-dom";

const SearchResultsPage = () => {
    const location = useLocation();
    const results = location.state;

    return (
        <div>
            {results.length === 0 ? (
                <p className="message">No Products Found</p>
            ) : (
                <div>
                    <p className="message">{results.length} Product/s Found</p>
                    <div className="products-container">
                        {results.map((result) => (
                            <ProductCard key={result.id} product={result}/>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

export default SearchResultsPage;
