import React from 'react';
import { getProductsByNumber } from "../../services/productService";
import ProductList from "../ProductList";

class PreviousOrders extends React.Component {
    state = {
        products: [],
        gotNumber: false
    };

    async changeTelephoneState() {
        const telNumber = document.getElementById("telephoneInput").value
        const productsList = await getProductsByNumber(telNumber)
        this.setState({products: productsList, gotNumber: true}) //
    }

   render() {
        return (
            <div>
                {this.state.gotNumber ? <div>
                    <h1>Previous Orders</h1>
                    <ProductList products={ this.state.products } />
                    </div> :
                    <div>
                        <input className="m-2" id="telephoneInput" type="text" placeholder="Enter Telephone number"/><br/>
                        <button className="btn btn-secondary m-2" onClick={() => this.changeTelephoneState()}>
                            Get Orders
                    </button>
                    </div>
                }
            </div>
        )
    }
}

export default PreviousOrders