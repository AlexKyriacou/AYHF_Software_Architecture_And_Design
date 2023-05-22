//TO DELETE ONCE WE ESTABLISH BACKEND CONNECTION
const productData = [
    {
        name: "Celebrate Health",
        description: "Tomato Sauce 430ml",
        longDescription: "Eating healthy shouldn't be hard. Real food that's better for you, even better again if it's low FODMAP, gluten free and contains no added sugar.",
        ingredients: "Water, Tomato Paste (22%), White Vinegar, Carrot Juice Concentrate, Corn Starch, Salt, Natural Sweetener (Steviol Glycosides), Spices [Cinnamon, Cloves, Nutmeg], Natural Colour [Paprika Extract].",
        image: require("../../images/tomato-sauce.png"),
        rating: 3,
        numRatings: 25,
        price: 4.99
    }, {
        name: "Nutra Organics",
        description: "Bone Broth Chicken Organic Homestyle Original 125g",
        longDescription: "The Nutra Organics Bone Broth Chicken Organic Homestyle flavour brings you the goodness of nutrient-dense bone broth with the added benefit of a traditional chicken flavour. This health elixir is rich in B Vitamins, Zinc, and gut-loving ingredients that heal the body and the soul while supporting the immune system. It's easy to use and a versatile kitchen staple. Replacing Nutra Organics Bone Broth Chicken Organic Homestyle Original 100g.",
        ingredients: "Free Range Chicken Bone Broth Powder  (Chicken Bones , Filtered Water, Apple Cider Vinegar , Himalayan Salt) (83%), Roasted Chicken Powder (Roasted Chicken, Tapioca Starch, Ginger, Rosemary), Deactivated Savoury Yeast Flakes, Himalayan Crystal Salt, Ascorbic Acid, Black Pepper. Certified Organic",
        image: require("../../images/chicken-broth.png"),
        rating: 5,
        numRatings: 1,
        price: 43.95
    }, {
        name: "Ceres Organics",
        description: "Basil Pesto 130g",
        longDescription: "This vegan basil pesto is made with extra virgin olive oil and bursting with basil! It's cheese-free, 100% certified organic, shelf-stable and made in Italy - the home of pesto.",
        ingredients: "Extra virgin olive oil , Cashew nuts , Genovese Basil P.D.O. in extra virgin olive oil 36% (Genovese Basil P.D.O. 48%, extra virgin olive oil  45%, salt), Apple fibre , Walnuts , Pine kernels , Garlic , Salt ( Certified Organic).",
        image: require("../../images/basil-pesto.png"),
        rating: 5,
        numRatings: 6,
        price: 8.49
    }, {
        name: "Bae Juice",
        description: "120ml",
        longDescription: "Bae Juice is sourced, squeezed and packaged in Naju, South Korea, and brought all the way down under into your hot little hands. Bae literally translates to 'pear' in Korean, and that’s all Bae Juice is—100% Korean pear juice. The Korean pear is a super-food, and lowers cholesterol, reduces blood pressure, is high in fibre and antioxidants and fill to the brim with micronutrients!",
        ingredients: "100% Korean Pear Juice",
        image: require("../../images/bae-juice.png"),
        rating: 4,
        numRatings: 20,
        price: 3.95
    }, {
        name: "Alter Eco Organic",
        description: "Chocolate Truffles Dark Salted Caramel 108g",
        longDescription: "These Alter Eco Organic Chocolate Dark Salted Caramel Truffles deliver and addictive twist on a classic truffle treat. With a super strict ingredients list featuring only the best in organic ingredients, these truffles are coated in a malty tasting, 58% cocoa Ecuadorian cacao and filled with nourishing coconut oil, and a sprinkling of fleur de sel de Guérande for a creamy, deep caramel bite.",
        ingredients: "Organic Raw Cane Sugar, Organic Cacao Beans, Organic Cocoa Butter, Organic Coconut Oil, Organic Whole Milk, Caramel Powder 0.5% (Organic Raw Cane Sugar, Organic Whole Milk), Natural Flavour, Sea Salt (Fleur De Sel) 0.4%, Organic Vanilla Beans.",
        image: require("../../images/dark-chocolate-sc.png"),
        rating: 2,
        numRatings: 4,
        price: 11.17
    }, {
        name: "Naked Life",
        description: "Nootropics Calm Passionfruit 4x250ml",
        longDescription: "Naked Life's range of sparkling nootropics uses a unique blend of natural and functional ingredients to deliver an impact you can feel and a taste you'll love. Designed to bring out your best with every sip, no matter what the day might throw at you.",
        ingredients: "Sparkling Water, Natural Sweeteners (Erythritol, Stevia), Citric Acid, Magnesium Citrate, Natural Extracts (Acerola, Passionflower, Lemon Balm, Ashwagandha Root), Natural Flavours, L-Theanine.",
        image: require("../../images/sparkling-water.png"),
        rating: 1,
        numRatings: 16,
        price: 10.00
    }, {
        name: "Lum Lum Organic",
        description: "Instant Noodle Chicken 75g",
        longDescription: "Wholegrain brown rice noodles with vegan chicken flavour for a convenient meal ready in minutes.",
        ingredients: "Organic brown rice noodle: brown rice, water organic chicken flavour: organic garlic, organic coriander, organic soybean paste, organic pepper, organic dried onion, organic cane sugar, organic coconut oil, salt, water, citric acid",
        image: require("../../images/instant-noodles.png"),
        rating: 5,
        numRatings: 2,
        price: 5.79
    }
];

export default productData;
