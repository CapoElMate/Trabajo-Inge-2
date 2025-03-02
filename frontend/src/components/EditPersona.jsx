import React, { Fragment, useState } from "react";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

import { getPersonas } from "./ListPersona.jsx"
function EditPersona({ persona, setPersonas }) {



    const [nombre, setNombre] = useState(persona.name); //nombre
    const [edad, setEdad] = useState(persona.age); //edad
    const [show, setShow] = useState(false); //mostrar o no el "Modal"

    //llamo a la funcion cuando presiono el boton de guardar cambios
    async function editPersona(e) {

        e.preventDefault(); //no se q hace pero es importante (aparentemente)

        try {
            //guardo las constantes en variables con nombres en ingles
            const name = nombre;
            const age = edad;
            const id = persona.id;
            const body = { id, name, age }; //las pongo las 3 juntas en un arreglo

            console.log(JSON.stringify(body)); //las hago JSON

            //hago una consulta http asincronica con el metodo PUT (editar)
            const editPersona = await fetch(`http://localhost:5069/api/Personas/${id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(body) //le paso los datos en JSON
            });

            await getPersonas(setPersonas); //actualizo la lista de personas
            handleClose(); //esto cierra el pop-up modal

        }
        catch (err) {
            console.error(err.message);
        }
    };

    const handleClose = () => setShow(false); //Cerrar el popup
    function handleShow() { //al mostrar el popup, lo actualiza con los datos reales de la persona.
        setNombre(persona.name)//actualiza nombre, para que en el cuadro de texto aparezca el valor que corresponde
        setEdad(persona.age)//quizas esto deberia estar en otra parte, pero es lo q se hacer


        return setShow(true);//muestra el popup
    }


    //usé un modal de react-boostrap (https://react-bootstrap.netlify.app/docs/components/modal/) (fue el que mejor andaba)
    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Editar
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Editar</Modal.Title>
                </Modal.Header>
                <Modal.Body>

                    <form>
                    
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

                    </form>

                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Cancelar
                    </Button>
                    <Button variant="primary" onClick={editPersona}   >
                        Guardar cambios
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default EditPersona;