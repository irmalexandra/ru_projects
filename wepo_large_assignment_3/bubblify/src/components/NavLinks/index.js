import React from 'react';
import { NavLink} from "react-router-dom";

const NavLinks = () => (
    <div className="container-fluid">
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                <li className="nav-item">
                    <NavLink exact to="/" className="nav-link">Products</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink exact to="/bundles" className="nav-link">Bundles</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink exact to="/about" className="nav-link">About Us</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink exact to="/cart" className="nav-link">Cart</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink exact to="/previousorder" className="nav-link">Previous Orders</NavLink>
                </li>
            </ul>
        </div>
    </div>
)

export default NavLinks;