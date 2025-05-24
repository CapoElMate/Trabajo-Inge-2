import React, { Fragment, useState } from "react";
import TextInput from "../components/TextInput";
import SelectInput from "../components/SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "../components/FormButtons";
import Header2 from "./Header2";
import { useNavigate } from "react-router-dom";


export default function CargarMaquinaria() {
  const modelosPorMarca = {
  "Marca A": ["Modelo A1", "Modelo A2"],
  "Marca B": ["Modelo B1", "Modelo B2"]
  };
  const [tags, setTags] = useState([]);
  const opcionesDeTags = ["Pesado", "Ligero"];

  const [marcaSeleccionada, setMarcaSeleccionada] = useState("");
  const [modelosDisponibles, setModelosDisponibles] = useState([]);

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

    try {
      const res = await fetch("api/maquinarias", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      if (!res.ok) throw new Error("Error en el servidor");

      const data = await res.json();
      console.log("Respuesta exitosa:", data);
    } catch (err) {
      console.error("Error al enviar:", err);
    }
  };

  const handleCancel = () => {
    console.log("Cancelado");
    navigate('/HomePage');
  };
  return (
    <>
      <Header2 />
       <div className="detalle-contenedor">
      <form className="max-w-md mx-auto p-4 border rounded space-y-4">
        <h2 className="text-xl font-semibold text-center">Cargar Maquinaria</h2>

        <SelectInput
          label="Marca "
          options={Object.keys(modelosPorMarca)}
          value={marcaSeleccionada}
          onChange={(e) => handleMarcaChange(e.target.value)}
        />

        <SelectInput
          label="Modelo "
          options={modelosDisponibles}
        />

        <TextInput label="Año de Fabricacion" type="number" />
        <SelectInput label="Permisos " options={["Permiso A", "Permiso B"]} />
        <SelectInput label="Tipo Maquinaria " options={["Excavadora", "Grúa"]} />
        <SelectInput label="Permisos adicionales " options={["Extra 1", "Extra 2"]} />

        <TagSelector tags={tags} setTags={setTags} opciones={opcionesDeTags} />
        <FormButtons  onSubmit={handleSubmit} onCancel={handleCancel} />
      </form>
      </div>
    </>
  )
}