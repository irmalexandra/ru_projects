import React from 'react';
import PropTypes from 'prop-types';
import Product from "../Product";
import {addBundleToCart} from "../../services/productService";

const Bundle = ({id, name, items}) => (
    <div className="bundle-container mb-5">
        <h5 className="card-title">{name}</h5>
        <div className="product-container">
        {items.products.map(p => <Product key={p.name} {...p} />)}
        </div>
        <button className="btn btn-outline-secondary add_to_cart_button" onClick={() => addBundleToCart(id)}
                id={id}>Add to Cart
        </button>
    </div>


);

Bundle.propTypes = {
    // The ID of the bundle
    id: PropTypes.number.isRequired,
    // The name of the bundle
    name: PropTypes.string.isRequired,
    // A list of product ids
    items: PropTypes.object.isRequired
};

export default Bundle;