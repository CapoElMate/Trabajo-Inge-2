import { useState } from "react";
import TextInput from "../components/TextInput";
import SelectInput from "../components/SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "../components/FormButtons";
import ImageUploader from "./ImageUploader";
import ImagePreviewList from "./ImagePreviwList"; 
import Header from "./Header";
import { useNavigate } from "react-router-dom";
import PublicacionForm from "./FormPublicacion"

export default function CargarPublicacion() {  
  const navigate = useNavigate(); 
  const handleSubmit =(data) => {
    console.log(data) 
     //como ya lo recibo en el formato que quiero no lo modifico
    fetch('http://localhost:3001/publicaciones', {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    }).then(() =>{  
      navigate("/HomePage");});//ir al listado de publicaciones
/**/
   /*
       if (!marca || !modelo || !año) {
      alert("Por favor completá todos los campos obligatorios.");
      return;
    } 
    const data = {
      "marcaName": marca,
      "marca": {
        "marcaName": marca,
        "modelos": [
          {
            "modeloName": modelo,
            "marcaName":marca,
          }
      ]},
      "anioFabricacion": año,
      "tipo": tipo,
      "permisosEspeciales": permisos.map((p)=>({"permiso":p })),//mapear al formato
      "tagsMaquina":tags.map((t)=>({"tag":t })),
    };
    console.log("Datos en cargar maquinariasubmit: ", data);
    
   
    try {
      const res = await fetch("api/maquinarias", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      if (!res.ok) throw new Error("Error en el servidor");
       
      alert("maquinaria cargada exitosamente")
    } catch (err) {
      console.error("Error al enviar:", err)
      alert("Ha ocurrido un error")
    }*/
  };
   
  const handleCancel = () => {
    console.log("Cancelado");
    navigate('/HomePage');
  };
  return (
    <>
      <Header/>       
         <PublicacionForm  onSubmit={handleSubmit} onCancel={handleCancel}/>
    </>
  )
}