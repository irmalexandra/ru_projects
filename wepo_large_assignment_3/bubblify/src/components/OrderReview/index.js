import React, {useEffect, useState} from 'react'
import {postOrder, getCartItems} from "../../services/productService"
import {NavLink} from "react-router-dom";


const OrderReview = ({info}) => {
    const [cart, setCart] = useState([])

    useEffect(() => {
        getCartItems().then(cart => setCart(cart))
    },[])

    return (
        <div>
            <h1>Order Review</h1>
            <h2>Products</h2>
            <table className="table table-hover">
                <thead>
                <tr>
                    <th scope="col">Product Name</th>
                    <th scope="col">Product Price</th>
                </tr>
                </thead>
                <tbody>
                {cart.map(item =>
                    <tr key={item.id}>
                        <td>{item.name}</td>
                        <td>{item.price}</td>
                    </tr>
                    )}
                </tbody>
            </table>
            <h2>Info</h2>
            <table className="table">
                <thead>
                <tr>
                    {info.fullName &&
                    <th scope="col">Full Name</th>}
                    {info.city &&
                    <th scope="col">City</th>}
                    {info.address &&
                    <th scope="col">Address</th>}
                    {info.telephone &&
                    <th scope="col">Telephone</th>}
                    {info.postalCode &&
                    <th scope="col">Postal Code</th>}
                </tr>
                </thead>
                <tbody>
                    <tr>
                        {info.fullName &&
                        <td>{info.fullName}</td>}
                        {info.city &&
                        <td>{info.city}</td>}
                        {info.address &&
                        <td>{info.address}</td>}
                        {info.telephone &&
                        <td>{info.telephone}</td>}
                        {info.postalCode &&
                        <td>{info.postalCode}</td>}
                    </tr>
                </tbody>
            </table>
            <div id="confirmBody">
                <NavLink to="/" className="btn btn-primary" onClick={() => postOrder({cart: cart, info: info})}>Confirm</NavLink>
            </div>
        </div>
    )

}

export default OrderReview;


























