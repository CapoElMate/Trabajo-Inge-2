// importar para usar boton de delete persona.
//importar con "import DeletePersona from "./DeletePersona.jsx";"
//y llamar con: "<DeletePersona persona={persona} personas={personas} setPersonas={setPersonas} />"

const puerto = import.meta.env.VITE_APP_PUERTO_API;
const protocolo = import.meta.env.VITE_APP_PROTOCOLO_API;
const direccion = import.meta.env.VITE_APP_DIRECCION_API;
const usoHTTPS = import.meta.env.VITE_APP_USAR_HTTPS;

//pido como referencia esto para actualizarlo.
function DeletePersona({ persona, personas, setPersonas }){
    

    const deletePersona = async (id, setPersonas, personas) => {

        console.log(id);
        console.log(setPersonas);
        console.log(personas);

        try {
            const deletePersona = await fetch(`${protocolo}://${direccion}:${puerto}/api/Personas/${id}`, {
                method: "DELETE",
                credentials: usoHTTPS === 'true' ? 'include' : false,
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