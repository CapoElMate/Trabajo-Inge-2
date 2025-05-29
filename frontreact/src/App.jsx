import React, { Fragment } from "react";
import { useState } from 'react';
import axios from 'axios';

import './App.css';

// Importa el SDK de Mercado Pago
import { initMercadoPago, Wallet } from '@mercadopago/sdk-react';

// Inicializa Mercado Pago con tu public key
initMercadoPago('APP_USR-17034ece-e13c-4fa1-9151-a1e3335e6f39');


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
            {/*<div classname="container">*/}
            {/*    */}{/*<InputPersona setPersonas={setPersonas}/>*/}
            {/*    <h2>Lista Usuarios Registrados</h2>*/}
            {/*    <ListaPersonas personas={personas} setPersonas={setPersonas} />*/}
            {/*</div>*/}

            mercadolibrestuff

            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '50px' }}>
                <h1>Botón de Pago</h1>
                <p>Haz clic en el botón para realizar el pago.</p>
                {/* Renderiza el botón de pago */}
                <div style={{ width: '300px' }}>
                    <Wallet initialization={{ preferenceId: '2462257991-f2e4c7a7-7e31-47e8-a9d5-98f035aac74c' }} />
                </div>
            </div>
                    

        </Fragment>
    );

}

export default App
