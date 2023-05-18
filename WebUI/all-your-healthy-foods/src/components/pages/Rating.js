import React from "react";
import { FaStar } from "react-icons/fa";

const Rating = ({ rate }) => {
    return (
        <div>
            {[...Array(5)].map((item, index) => {
                const givenRating = index + 1;
                return (
                    <label>
                        <input
                            type="radio"
                            hidden
                            value={givenRating}
                        />
                        <FaStar
                            color={
                                givenRating < rate || givenRating === rate
                                    ? "000"
                                    : "rgb(192,192,192)"
                            }
                        />

                    </label>
                );
            })}
        </div>
    );
};

export default Rating;