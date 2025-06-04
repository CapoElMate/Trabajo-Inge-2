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
  const navigate = useNavigate();

  // const createMarca = async (data) => {
  //   const body = {
  //     marca: data.marca,
  //   };

  //   console.log("Datos en cargar marca: ", body);
  //   let marcas = await getMarcas();
  //   if(!marcas.map(marca => marca.toLowerCase()).includes(data.marca.toLowerCase())) {
  //     // Enviar los datos al servidor
  //     return fetch("http://localhost:5000/api/Marcas", {
  //       method: "POST",
  //       headers: { "Content-Type": "application/json" },
  //       body: JSON.stringify(body),
  //     });
  //   }
  //   else
  //   {
  //     console.error("La marca ya existe");
  //     //setErrors(prev => ({ ...prev, modelo: "El modelo ya existe" }));
  //     return {ok: true};
  //   }
  // }

  // const createModelo = async (data) => {
  //   const body = {
  //     modelo: data.modelo,
  //     marca: {
  //       marca: data.marca,
  //     },
  //   };

  //   console.log("Datos en cargar modelo: ", body);

  //   let modelos = await getModelos();
  //   if(!modelos.map(modelo => modelo.toLowerCase()).includes(data.modelo.toLowerCase())) {
  //     // Enviar los datos al servidor
  //     return fetch("http://localhost:5000/api/Modelos", {
  //       method: "POST",
  //       headers: { "Content-Type": "application/json" },
  //       body: JSON.stringify(body),
  //     });
  //   }
  //   else
  //   {
  //     console.error("El modelo ya existe");
  //     //setErrors(prev => ({ ...prev, modelo: "El modelo ya existe" }));
  //     return {ok: true};
  //   }
  // }

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
      // 3 - Crear la maquinaria
      const responseMaquina = await createMaquina(data);
      if (responseMaquina.status !== 201) {
        console.error("Error al crear la maquinaria");
        setErrors((prev) => ({
          ...prev,
          maquinaria: "La maquinaria ya existe.",
        }));
        return;
      }

      setTimeout(() => {
        navigate("/ListarMaquinaria");
      }, 2000);
    } catch (error) {
      console.error("Error general en la creación:", error);
    }
  };

  const handleCancel = () => {
    console.log("Cancelado");
    navigate("/ListarMaquinaria");
  };
  return (
    <>
      <Header />
      <MaquinariaForm onSubmit={handleSubmit} onCancel={handleCancel} />
    </>
  );
}
