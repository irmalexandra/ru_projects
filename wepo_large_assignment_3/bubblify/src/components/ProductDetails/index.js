import React from 'react';
import { PropTypes } from 'prop-types'
import { addToCart } from "../../services/productService";

const ProductDetails = ({name, image, price, description, id}) =>{
        return (
            <div className="product-container">
                <div className="image-container">
                    <img src={image} className="img-fluid"
                         alt={"A picture of bubble product with id " + id}/>
                </div>
                <div className="product-details">
                    <h1>{name}</h1>
                    <hr className="light" />
                    <h2>Product Price</h2>
                    <p>{price}</p>
                    <h2>Description</h2>
                    <p>{description}</p>
                    <button className="btn btn-outline-secondary add_to_cart_button" onClick={() => addToCart(id)}
                            id={id}>Add to Cart
                    </button>
                </div>
            </div>
        );
}

ProductDetails.propTypes = {
    // The name of the product
    name: PropTypes.string.isRequired,
    // An image of the product
    image: PropTypes.string.isRequired,
    // The price of the product
    price: PropTypes.number.isRequired,
    // The description of the product
    description: PropTypes.string,
    // The unique id of the product
    id: PropTypes.number.isRequired
}
export default ProductDetails;
