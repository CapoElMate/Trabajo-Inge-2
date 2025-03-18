import React, { Fragment, useState } from "react";
import { getPersonas } from "./ListPersona.jsx"

const puerto = import.meta.env.VITE_APP_PUERTO_API;
const protocolo = import.meta.env.VITE_APP_PROTOCOLO_API;
const direccion = import.meta.env.VITE_APP_DIRECCION_API;


function InputPersona({ setPersonas}) {
    
    const [nombre, setNombre] = useState("");
    const [edad, setEdad] = useState("");


    //cuando termino de escribir:
    const onSubmitForm = async (e) => {
        e.preventDefault();

        try {
            const id = 0;
            const age = edad;
            const name = nombre;
            const response = await fetch(`${protocolo}://${direccion}:${puerto}/api/Personas/`, {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ id, name, age })  //id name y age deben tener esos nombres de variables para que se formateen correctamente.
            });

            //tambien borro los campos de entrada
            setEdad("");
            setNombre("");

            //hago un getPersonas para actualizar los datos en la pagina.
            getPersonas(setPersonas)
        }
        catch (err) {
            console.error(err.message);
        }
    };



    return (
        <Fragment>
            <h2>Input persona: </h2>
            <div>

                <span>
                    <form onSubmit={onSubmitForm}>

                        <p class="mt-3 mb-0">   edad: </p>
                        <input
                            type="number"
                            className="form-control"
                            value={edad}
                            onChange={e => setEdad(e.target.value)} />

                        <p class="mt-3 mb-0">   nombre: </p>
                        <input
                            type="text"
                            className="form-control"
                            value={nombre}
                            onChange={e => setNombre(e.target.value)} />

                        <button class="mt-3">Ingresar</button>

                    </form>
                </span>
            </div>

        </Fragment>

    );
}


export default InputPersona