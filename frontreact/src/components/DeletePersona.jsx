// importar para usar boton de delete persona.
//importar con "import DeletePersona from "./DeletePersona.jsx";"
//y llamar con: "<DeletePersona persona={persona} personas={personas} setPersonas={setPersonas} />"

//pido como referencia esto para actualizarlo.
function DeletePersona({ persona, personas, setPersonas }){
    

    const deletePersona = async (id, setPersonas, personas) => {

        console.log(id);
        console.log(setPersonas);
        console.log(personas);

        try {
            const deletePersona = await fetch(`http://capoelmate.com.ar:5069/api/Personas/${id}`, {
                method: "DELETE"
            });

            setPersonas(personas.filter(persona => persona.id !== id));
        } catch (err) {
            console.error(err.message);
        }
    };

    return (
        <button className="btn btn-danger" onClick={() => deletePersona(persona.id, setPersonas, personas)}> Borrar </button>
    );

}

export default DeletePersona;