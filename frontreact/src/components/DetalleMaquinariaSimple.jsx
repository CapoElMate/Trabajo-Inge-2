export default function DetalleMaquinariaSimple ({maquinaria={}}){

 
    return(
        <>
      {maquinaria ? 
      (<>
        <h5 className="text-xl mb-2"> {maquinaria.marca} {maquinaria.modelo}</h5>
        <p>Año: {maquinaria.anioFabricacion}</p>
        <p>Tipo: {maquinaria.tipo}</p>
        <p>Permisos: {maquinaria.permisosEspeciales?.map(p => p.permiso.permiso).join(", ") || "Aun no tiene permisos asignados"}</p>
        <p>Tags: {maquinaria.tagsMaquina?.length > 0
                    ? maquinaria.tagsMaquina.map(p => p.tag?.tag).join(", ")
                    : "Aún no tiene etiquetas asignadas"}</p>
      </>
      )
      :(
        <h2 className="text-xl mb-2"> {"No hay una maquinaria disponible"} </h2>
    )}    </> 
);

}
 {/*"id": "maq103",
      "marca": "Marca A",
      "modelo": "",
      "anioFabricacion": "",
      "tipo": "Compactador",
      "permisosEspeciales": [],
      "tagsMaquina": []
    }, */}