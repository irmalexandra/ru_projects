import React from 'react'
import ProductList from "../ProductList";
import {getCartItems, clearCart} from "../../services/productService";
import {NavLink} from "react-router-dom";


class Cart extends React.Component {

    state = {
        products: [],
        delivery: true
    };

    async componentDidMount() {
        this.setState({
            products: await getCartItems()
        })
    }

    clearButtonHandler = () => {
        clearCart()
        this.setState({products: []});
    }


    render() {
        return (<div id="cartBody">
            <h1>Cart</h1>
            <ProductList products={this.state.products}/>

            {this.state.products.length > 0 ?
                <div>
                    <button type="submit" className="btn btn-outline-secondary m-1"
                            onClick={() => this.clearButtonHandler()}>Clear cart</button>
                    <br/>
                    <NavLink className="btn btn-secondary m-1" to="/checkout">Proceed to checkout</NavLink>
                </div>

                : <h3>The cart is empty.</h3>
            }

        </div>);
    }

}

export default Cart;