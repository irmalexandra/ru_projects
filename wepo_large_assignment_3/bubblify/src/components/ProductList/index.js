import React from "react";
import Product from "../Product";
import {PropTypes} from "prop-types";

const ProductList = ({products}) => (
        <div className="container-fluid padding">
            <div className="d-flex flex-row flex-wrap justify-content-center">
                {products.map(p => <Product key={p.name} {...p} />)}
            </div>
        </div>
)

ProductList.propTypes = {
    // An array of objects
    products: PropTypes.arrayOf(PropTypes.shape({
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
    })).isRequired
}

export default ProductList





