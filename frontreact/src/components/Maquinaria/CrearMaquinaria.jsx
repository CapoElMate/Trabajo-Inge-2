import React, { Fragment, useState } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import Header from "../Header";
import { useNavigate } from "react-router-dom";
import MaquinariaForm from "./FormMaquinaria";
import "../FormStructure.css";

export default function CrearMaquinaria({ initialData = {}, modo = "Crear" }) {
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

  const getModelos = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Modelos/all");
      if (!response.ok) {
        throw new Error("Error al obtener los modelos");
      }
      const data = await response.json();
      return data.map(modelo => modelo.modelo);
    } catch (error) {
      console.error("Error fetching modelos:", error);
      return [];
    }
  }

  const getMarcas = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Marcas/all");
      if (!response.ok) {
        throw new Error("Error al obtener las marcas");
      }
      const data = await response.json();
      return data.map(marca => marca.marca);
    } catch (error) {
      console.error("Error fetching marcas:", error);
      return [];
    }
  }

  const createMarca = async (data) => {
    const body = {
      marca: data.marca,
    };

    
    console.log("Datos en cargar marca: ", body);
    let marcas = await getMarcas();
    if(!marcas.map(marca => marca.toLowerCase()).includes(data.marca.toLowerCase())) {
      // Enviar los datos al servidor
      return fetch("http://localhost:5000/api/Marcas", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      });
    }
    else
    {
      console.error("La marca ya existe");
      //setErrors(prev => ({ ...prev, modelo: "El modelo ya existe" }));
      return {ok: true};
    }
  }

  const createModelo = async (data) => {
    const body = {
      modelo: data.modelo,
      marca: {
        marca: data.marca,
      },
    };

    console.log("Datos en cargar modelo: ", body);

    let modelos = await getModelos();
    if(!modelos.map(modelo => modelo.toLowerCase()).includes(data.modelo.toLowerCase())) {
      // Enviar los datos al servidor
      return fetch("http://localhost:5000/api/Modelos", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      });
    }
    else
    {
      console.error("El modelo ya existe");
      //setErrors(prev => ({ ...prev, modelo: "El modelo ya existe" }));
      return {ok: true};
    }
  }

  const createMaquina = async (data) => {
    const body = {
      status: "Disponible",
      anioFabricacion: data.anioFabricacion,
      modelo: {
        modelo: data.modelo,
        marca: {
          marca: data.marca,
        },
      },
      tagsMaquina: data.tagsMaquina.map((t) => ({ tag: t })) || [],
      tipoMaquina: {
        tipo: data.tipo,
      },
      permisosEspeciales:
        data.permisosEspeciales.map((p) => ({ permiso: p })) || [],
    };

    console.log("Datos en cargar maquinarias: ", body);

    // Enviar los datos al servidor
    return fetch("http://localhost:5000/api/Maquinas", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body),
    }) 
  };

  const handleSubmit = async (data, setErrors) => {
    console.log(data);
    try {
    // 1 - Crear la marca
    const responseMarca = await createMarca(data);
    if (responseMarca != null && !responseMarca.ok) {
      console.error("Error al crear la marca");
      return;
    }

    // 2 - Crear el modelo
    const responseModelo = await createModelo(data);
    if (!responseModelo.ok) {
      console.error("Error al crear el modelo");
      return;
    }

    // 3 - Crear la maquinaria
    const responseMaquina = await createMaquina(data);
    if (responseMaquina.status !== 201) {
      console.error("Error al crear la maquinaria");
      setErrors(prev => ({ ...prev, maquinaria: "La maquinaria ya existe." }));
      return;
    }

    // 4 - Redirigir
    navigate("/maquinarias");
    } catch (error) {
      console.error("Error general en la creación:", error);
    }
    
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
    navigate("/maquinarias");
  };
  return (
    <>
      <Header />
      <MaquinariaForm onSubmit={handleSubmit} onCancel={handleCancel} />
    </>
  );
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
