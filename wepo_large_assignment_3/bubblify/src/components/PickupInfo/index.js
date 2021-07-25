import React from 'react';
import OrderReview from "../OrderReview";



class PickupInfo extends React.Component {
    constructor(props) {
        super(props);
        this.userForm = {};
    }

    state = {
        isValid: false,
        fields: {
            fullName: '',
            telephone: '',
        },
        errors: {
            fullNameError: '',
            telephoneError: '',
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
        const {fullName, telephone} = this.state.fields;
        const errors = {};

        if (fullName === '') {
            errors.fullNameError = 'Full name is required.';
        }
        if (telephone === '') {
            errors.telephoneError = 'Telephone is required.';
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
                        <h1>Pickup Info</h1>
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
                            <button type="submit" className="btn btn-primary">Submit</button>
                        </form>
                    </div>
                }
            </div>
        )
    }
}

export default PickupInfo
