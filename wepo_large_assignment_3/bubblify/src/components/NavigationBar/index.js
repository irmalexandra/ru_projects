import React from 'react';
import NavLinks from "../NavLinks";
import {NavLink} from "react-router-dom";

const NavigationBar = () => (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <NavLink exact to="/" className="navbar-brand">Bubblify</NavLink>
        <NavLinks />
    </nav>
);

export default NavigationBar;


