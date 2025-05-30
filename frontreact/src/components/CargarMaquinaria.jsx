import React, { Fragment, useState } from "react";
import TextInput from "../components/TextInput";
import SelectInput from "../components/SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "../components/FormButtons";
import Header from "./Header";
import { useNavigate } from "react-router-dom";
import MaquinariaForm from "./FormMaquinaria";

export default function CargarMaquinaria({initialData = {}, modo = 'Crear'}) {
  /*
  const [tags, setTags] = useState([]);
  const opcionesDeTags = ["Pesado", "Ligero"];

  const [modelo, setModelo] = useState(initialData.marca?.modelo || "");
  const [tipo, setTipo] = useState(initialData.tipo || "");
  const handleMarcaChange = (marca) => {
    setMarcaSeleccionada(marca);
    setModelosDisponibles(modelosPorMarca[marca] || []);
  };
  const [anio, setAnio] = useState(initialData.anioFabricacion || "");
  const [marcaSeleccionada, setMarcaSeleccionada] = useState("");
  const [modelosDisponibles, setModelosDisponibles] = useState([]);*/
  const navigate = useNavigate(); 
  const handleSubmit =(data) => {
    console.log(data)
    const body = {//mapear al formato
      "id":data.id,
      "marca": data.marca, 
      "modelo": data.modelo,
      "anioFabricacion": data.anio,
      "tipo": data.tipo,
      "permisosEspeciales": data.permisos.map((p)=>({"permiso":p })),
      "tagsMaquina":data.tags.map((t)=>({"tag":t })),
    };
     
    fetch('http://localhost:3001/maquinas', {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body),
    }).then(() =>{ 
      //"¡Maquinaria 'Caterpillar D6N LGP' creada correctamente!"
      navigate("/maquinarias");});
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
    navigate('/maquinarias');
  };
  return (
    <>
      <Header/>       
         <MaquinariaForm  onSubmit={handleSubmit} onCancel={handleCancel}/>
    </>
  )
}
 /*
    "anioFabricacion": int,
    "marcaName": "string",
  "marca": {
    "marcaName": "string",
    "modelos": [
      {
        "modeloName": "string",
        "marcaName": "string",
        "marca": "string"
      }
    ]
  },
  "tagsMaquina": [
    {
      "tag": "string",
    },
    {
      "tag": "string",
    }
  ],
  "permisosEspeciales": [
    {
      "permiso": "string",
    },
    {
      "permiso": "string",
    }
  ],
  "tipo": "string",
    */ 