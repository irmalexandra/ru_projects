import React from 'react';
import OrderReview from "../OrderReview";


class DeliveryInfo extends React.Component {
    constructor(props) {
        super(props);
        this.userForm = {};
    }

    state = {
        isValid: false,
        fields: {
            fullName: '',
            telephone: '',
            postalCode: '',
            city: '',
            address: '',
        },
        errors: {
            fullNameError: '',
            telephoneError: '',
            countryError: '',
            postalCodeError: '',
            addressError: '',
        },
    }

    onInput(e) {
        this.setState({
            fields: {
                ...this.state.fields,
                [e.target.name]: e.target.value,
            }
        });
    }

    validateForm(e) {
        e.preventDefault();
        const {fullName, telephone, address, city, postalCode} = this.state.fields;
        const errors = {};

        if (fullName === '') {
            errors.fullNameError = 'Full name is required.';
        }
        if (telephone === '') {
            errors.telephoneError = 'Telephone is required.';
        }
        if (postalCode === '') {
            errors.PostalCodeError = 'Postal code is required.';
        }
        if (city === '') {
            errors.cityError = 'City is required.';
        }
        if (address === '') {
            errors.addressError = 'Address is required.';
        }

        if (Object.keys(errors).length > 0) {
            this.setState({...this.state.errors, errors});
            this.setState({isValid: false})
        } else {
            this.setState({isValid: true})
        }
    }

    render() {
        return (

            <div className="inputFormContainer">
                {this.state.isValid ?
                    <OrderReview info={this.state.fields}/> :
                    <div>
                        <h1>Delivery Info</h1>
                        <form onSubmit={(e) => this.validateForm(e)}>
                            <div className="form-group">
                                <label htmlFor="inputName">Full Name</label>
                                <input type="text" ref={input => this.userForm.fullName = input}
                                       className="form-control"
                                       id="inputName" name="fullName"
                                       aria-describedby="nameHelp" placeholder="Enter name"
                                       onInput={e => this.onInput(e)}/>
                                <small id="nameHelp" className="form-text text-muted mb-4">We hope you will share your
                                    name with
                                    us</small>
                            </div>
                            <div className="form-group">
                                <label htmlFor="inputAddress">Address</label>
                                <input type="text" ref={input => this.userForm.address = input} className="form-control"
                                       id="inputAddress" name="address"
                                       aria-describedby="addressHelp" placeholder="Enter Address"
                                       onInput={e => this.onInput(e)}/>
                                <small id="addressHelp" className="form-text text-muted mb-4">Your address is very
                                    important so
                                    the bubbles find a new home</small>
                            </div>
                            <div className="form-group">
                                <label htmlFor="inputCity">City</label>
                                <input type="text" ref={input => this.userForm.city = input} className="form-control"
                                       id="inputCity" name="city"
                                       aria-describedby="cityHelp" placeholder="Enter City name"
                                       onInput={e => this.onInput(e)}/>
                                <small id="cityHelp" className="form-text text-muted mb-4">What is a city without
                                    bubbles in
                                    it?</small>
                            </div>
                            <div className="form-group">
                                <label htmlFor="inputTelephone">Telephone</label>
                                <input type="text" ref={input => this.userForm.telephone = input}
                                       className="form-control"
                                       id="inputTelephone" name="telephone"
                                       aria-describedby="telephoneHelp" placeholder="Enter telephone"
                                       onInput={e => this.onInput(e)}/>
                                <small id="telephoneHelp" className="form-text text-muted mb-4">So we can call you for
                                    all your
                                    bubble needs</small>
                            </div>
                            <div className="form-group">
                                <label htmlFor="inputPostalCode">Postal Code</label>
                                <input type="text" ref={input => this.userForm.postalCode = input}
                                       className="form-control"
                                       id="inputPostalCode" name="postalCode"
                                       aria-describedby="postalCodeHelp" placeholder="Enter postal code"
                                       onInput={e => this.onInput(e)}/>
                                <small id="postalCodeHelp" className="form-text text-muted mb-4">We couldn't come up
                                    with
                                    anything clever here...</small>
                            </div>


                            <button type="submit" className="btn btn-primary">Submit</button>
                        </form>
                    </div>
                }
            </div>
        )
    }
}

export default DeliveryInfo