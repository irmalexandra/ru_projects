import React, {useEffect, useState} from 'react';
import {getProductById} from "../../services/productService"
import ProductDetails from "../ProductDetails";
import {useParams} from "react-router";

const ProductDetailsWrapper = () => {
    const [product, setProduct] = useState(
        {
            product: {
                name: "",
                image: "",
                price: 0,
                description: "",
                id: 0
            }
        })

    const productId = useParams().productId

    useEffect(() => {
        getProductById(productId).then(r => setProduct(r));
    }, [productId])

    return (
        <div>
            {
                product.name !== undefined ?
                    <ProductDetails product={product.name} {...product}/>
                    :
                    <div/>
            }
        </div>


)
}

export default ProductDetailsWrapper