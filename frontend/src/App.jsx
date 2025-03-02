import React, { Fragment } from "react";
import { useState } from 'react';



import './App.css';


//componentes 
import InputPersona from "./components/InputPersona.jsx"
import ListaPersonas from "./components/ListPersona.jsx"

//aplicacion principal
function App() {

    //defino el hook para actualizar la lista de personas, lo paso como
    //referencia a listaPersonas e InputPersona para que los modulos individuales puedan actualizar a los demas.
    //personas es como una variable que cuando modifico con la funcion setPersonas(), se actualiza graficamente el cuadro.
    const [personas, setPersonas] = useState([]);


    return (
        <Fragment>
            <div classname="container">
                <InputPersona setPersonas={setPersonas}/>
                <ListaPersonas personas={personas} setPersonas={setPersonas} />
            </div>
        
        </Fragment>
    );
}

export default App
