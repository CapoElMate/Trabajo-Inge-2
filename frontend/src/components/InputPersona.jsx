import React, { Fragment, useState, useEffect } from "react";


function InputPersona({personas, setPersonas}) {

    
    const [nombre, setNombre] = useState("");
    const [edad, setEdad] = useState("");

    const onSubmitForm = async (e) => {
        e.preventDefault();

        try {
            const id = 0;
            const age = edad;
            const name = nombre;
            const response = await fetch("http://localhost:5069/api/Personas", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ id, name, age })
            });

            setEdad("");
            setNombre("");
            
                const responseTwo = await fetch("http://localhost:5069/api/Personas");
                const jsonData = await responseTwo.json();

                setPersonas(Array.isArray(jsonData) ? jsonData : []); //garantiza que setPersonas siempre reciba un array, asi despues lo puedo mapear con .map()
                console.log(jsonData);


            console.log(JSON.stringify({ id, name, age }));
            console.log(responseTwo);
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


const styles = {
    grid: {
        display: "grid",
        gridTemplateColumns: "repeat(3,auto)", // Dos columnas de tamaño automático
        alignItems: "center", // Centra verticalmente
        gap: "20px",
    },
    padding: { padding: "20px" },
};


export default InputPersona