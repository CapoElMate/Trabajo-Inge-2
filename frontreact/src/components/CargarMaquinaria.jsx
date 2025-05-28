import React, { Fragment, useState } from "react";
import TextInput from "../components/TextInput";
import SelectInput from "../components/SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "../components/FormButtons";
import Header2 from "./Header2";
import { useNavigate } from "react-router-dom";
import MaquinariaForm from "./FormMaquinaria";

export default function CargarMaquinaria({initialData = {}, modo = 'Crear'}) {
  
  const [tags, setTags] = useState([]);
  const opcionesDeTags = ["Pesado", "Ligero"];
  const [anio, setAnio] = useState(initialData.anioFabricacion || "");
  const [marcaSeleccionada, setMarcaSeleccionada] = useState("");
  const [modelosDisponibles, setModelosDisponibles] = useState([]);
  const [modelo, setModelo] = useState(initialData.marca?.modelo || "");
  const [tipo, setTipo] = useState(initialData.tipo || "");
  const handleMarcaChange = (marca) => {
    setMarcaSeleccionada(marca);
    setModelosDisponibles(modelosPorMarca[marca] || []);
  };

  const navigate = useNavigate(); 
  const handleSubmit = async () => {
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
    /*
     
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
      <Header2 />       
         <MaquinariaForm onSubmit={handleSubmit} onCancel={handleCancel}/>
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