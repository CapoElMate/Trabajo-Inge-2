import React, { Fragment, useEffect, useState} from "react";

function ListaPersonas({ personas, setPersonas}) {

    const deletePersona = async (id) => {
        try {
            const deletePersona = await fetch(`http://localhost:5069/api/Personas/${id}`, {
                method: "DELETE"
            });

            setPersonas(personas.filter(persona => persona.id !== id));
        } catch (err) {
            console.error(err.message);
        }
    };

    async function getPersonas() {
        try {
            const response = await fetch("http://localhost:5069/api/Personas");
            const jsonData = await response.json();

            setPersonas(Array.isArray(jsonData) ? jsonData : []); //garantiza que setPersonas siempre reciba un array, asi despues lo puedo mapear con .map()
            console.log(jsonData);

        } catch (err) {
            console.error(err.message);
        }
    };

    useEffect(() => {
        getPersonas();
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
                        {/*
            <tr>
                <th scope="row">1</th>
                <td>Jorge</td>
                <td>23</td>
            </tr>
            */}
                        {personas.map(persona => (
                            <tr>
                                <td scope="row">{persona.id}</td>
                                <td>{persona.name}</td>
                                <td>{persona.age}</td>
                                <td>
                                    <button className="btn btn-danger" onClick={() => deletePersona(persona.id)}> delete </button>
                                </td>
                                <td>editar</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </Fragment>
    );
}


export default ListaPersonas;