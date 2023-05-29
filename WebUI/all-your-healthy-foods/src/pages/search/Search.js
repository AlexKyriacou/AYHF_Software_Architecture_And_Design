import React, { useState } from "react";
import "./Search.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@fortawesome/free-solid-svg-icons";

const Search = ({ onSearch }) => {
    const [searchQuery, setSearchQuery] = useState("");

    const handleSearchChange = (e) => {
        const query = e.target.value;
        setSearchQuery(query);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await onSearch(searchQuery);
    };

    return (
        <form onSubmit={handleSubmit} className="search-form">
            <input
                type="text"
                placeholder="Search 100+ Products and brands..."
                value={searchQuery}
                onChange={handleSearchChange}
            />
            <FontAwesomeIcon
                icon={faSearch}
                className="search-button"
                onClick={handleSubmit}
            />
        </form>
    );
};

export default Search;
