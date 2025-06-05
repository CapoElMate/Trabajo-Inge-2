//import React, { useState, useEffect } from "react";
//import "../Modal.css";
//import "./ModalResultado.css";
import SelectInput from "../SelectInput";
import TextInput from "../TextInput";

export default function ModalResultado({
    isOpen,
    estado,
    colorFondo,
}) {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className={`modal-content`}>
                <h1 style={{ backgroundColor: colorFondo, color:"white" }}>{estado}</h1>
                <div>   </div>
            </div>
        </div>
    );
}
