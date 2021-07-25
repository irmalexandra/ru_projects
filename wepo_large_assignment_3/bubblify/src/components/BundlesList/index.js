import React from "react";
import Bundle from "../Bundle";

const BundlesList = ({bundles}) => (
    <div className="container-fluid padding">
            {bundles.map(b => <Bundle key={b.id} {...b} />)}
    </div>
)
export default BundlesList

