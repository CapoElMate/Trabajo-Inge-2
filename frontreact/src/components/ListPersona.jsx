import React, { Fragment, useEffect, useState} from "react";

import EditPersona from "./EditPersona.jsx";

import DeletePersona from "./DeletePersona.jsx";

//list personas es de las mas importantes, se encarga de mostrar y actualizar las personas, ademas de eliminarlas.

//uso export para poder usar getPersonas en otros archivos.
export const getPersonas = async (setPersonas) => {
    try {
        const response = await fetch("http://localhost:5069/api/Personas"); //pido GET de todos, me lo devuelve en json
        const jsonData = await response.json();

        setPersonas(Array.isArray(jsonData) ? jsonData : []); //garantiza que setPersonas siempre reciba un array, asi despues lo puedo mapear con .map()
        console.log(jsonData);

    } catch (err) {
        console.error(err.message);
    }
};

function ListaPersonas({ personas, setPersonas }) {

    //no se que hace useEffect, pero si lo saco se rompe todo:
    useEffect(() => {
        getPersonas(setPersonas);
    }, []);


    return (
        <Fragment>

            {" "}

            <div class="table-responsive">
                <table class="table mt-5 text-center">
                    <thead>
                        <tr>
                            <th scope="col">ID</th>
                            <th scope="col">Nombre</th>
                            <th scope="col">Edad</th>
                            <th scope="col">Eliminar</th>
                            <th scope="col">Editar</th>
                        </tr>
                    </thead>
                    <tbody>
                        
                        {personas.map(persona => (
                            <tr>
                                <td scope="row">{persona.id}</td>
                                <td>{persona.name}</td>
                                <td>{persona.age}</td>

                                <td>
                                    <div>
                                        <DeletePersona persona={persona} personas={personas} setPersonas={setPersonas} />
                                    </div>                                    
                                </td>
                                
                                <td> <EditPersona persona={persona} setPersonas={setPersonas} /> </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </Fragment>
    );
}


export default ListaPersonas;