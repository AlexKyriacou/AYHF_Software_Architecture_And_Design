import React from 'react';
import ProductCard from '../product/ProductCard';
import './Home.css';
import productData from '../product/productData';

const HomePage = () => {
    const products = productData;

    return (
        <div>
            <p className='welcome'>Welcome to All Your Healthy Foods Online Store</p>
            <div className='products-container'>
                {products.map((product, index) => (
                    <ProductCard key={index} product={product} />
                ))}
            </div>
        </div>
    );
};

export default HomePage;


//Ideas for the home page
// 1. Hero Banner: Use an attractive and visually appealing image or slideshow showcasing your healthy food products. Incorporate compelling text that highlights the benefits of your products and encourages users to explore further.
// 2. Featured Products: Display a section highlighting a selection of your top-selling or new arrivals of healthy food items. Include high-quality product images, brief descriptions, and prominent call-to-action buttons to encourage users to view more details or make a purchase.
// 3. Testimonials: Incorporate customer testimonials or reviews to build trust and credibility. Showcase positive feedback from satisfied customers who have enjoyed your healthy food products.
// 4. Special Offers: Promote any ongoing discounts, deals, or special offers prominently on the home page. This can attract attention and encourage visitors to explore your products further.
// 5. Product Categories: Provide clear navigation and visually appealing links to different product categories. For example, categorize your healthy food items into sections like "Snacks," "Beverages," "Organic Produce," etc. Make it easy for users to find what they're looking for.
// 6. Recipe Ideas: Include a section featuring healthy recipes using your products. Provide cooking tips, ingredient lists, and step-by-step instructions to inspire customers to experiment with your food items and create delicious and nutritious meals
// 7. Health Benefits: Educate your visitors about the health benefits of your products. Highlight specific nutrients, superfoods, or dietary benefits associated with your healthy food items. This can help customers make informed choices and understand why your products are beneficial for their well-being.
// 8. Subscription or Membership Options: If you offer subscription boxes or membership programs, feature them prominently on the home page. Explain the benefits of subscribing, such as regular deliveries of curated healthy food items, exclusive discounts, or access to member-only content.
// 9. Blog or Content Section: Incorporate a blog or content section where you can share informative articles, tips, and guides related to healthy eating, wellness, or food trends. This can establish your store as a valuable resource and keep visitors engaged.
// 10. Social Media Integration: Include social media buttons or feeds to showcase your active presence on platforms like Instagram, Facebook, or Pinterest. This allows visitors to explore your social content, engage with your brand, and share your products with their networks.
