import React, { Fragment } from "react";
import { useState } from 'react';



import './App.css';


//componentes 
import InputPersona from "./components/InputPersona.jsx"
import ListaPersonas from "./components/ListPersona.jsx"

function App() {

    const [personas, setPersonas] = useState([]);

    return (
        <Fragment>
            <div classname="container">
                <InputPersona personas={personas} setPersonas={setPersonas}/>
                <ListaPersonas personas={personas} setPersonas={setPersonas} />
            </div>
        
        </Fragment>
    );
}

export default App
