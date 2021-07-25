import React, {useState, useEffect} from 'react'
import ProductList from "../ProductList";
import {getProducts} from "../../services/productService"

const Products = () =>{
    const [ products, setProducts] = useState([])

    useEffect(  () => {
        getProducts().then(r => setProducts(r));
        },[])

    return (
        <div>
            <h1>Products</h1>
            <ProductList products={ products } />
        </div>
    )

}

export default Products;