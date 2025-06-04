import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Header from "../Header";
import MaquinariaForm from "./FormMaquinaria";

export default function EditarMaquinaria() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [maquinaria, setMaquinaria] = useState(null);

  // Cargar datos de la maquinaria a editar desde el backend
  useEffect(() => {
    fetch(`http://localhost:5000/api/Maquinas/byId?id=${id}`)
      .then((res) => {
        if (!res.ok) throw new Error("Error al cargar la maquinaria");
        return res.json();
      })
      .then((data) => setMaquinaria(data))
      .catch((error) => {
        console.error(error);
        alert("No se pudo cargar la maquinaria");
        navigate("/"); // redirigir si falla
      });
  }, [id, navigate]);

  // Cancelar edición y volver a la página principal o listado
  const handleCancel = () => {
    navigate("/ListarMaquinaria");
  };

  // Enviar los datos modificados al backend
  const handleSubmit = (data) => {
    const body = {
      idMaquina: id,
      status: "Disponible",
      anioFabricacion: data.anioFabricacion,
      modelo: {
        modelo: data.modelo,
        marca: {
          marca: data.marca,
        },
      },
      tagsMaquina: data.tagsMaquina || [],
      tipoMaquina: {
        tipo: data.tipo,
      },
      permisosEspeciales:
        data.permisosEspeciales.map((p) => ({ permiso: p })) || [],
    };

    fetch(`http://localhost:5000/api/Maquinas/byId?id=${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al guardar la maquinaria");
        setTimeout(() => {
        navigate("/ListarMaquinaria");
      }, 2000);
      })
      .catch((error) => {
        console.error(error);
        alert("No se pudo guardar la maquinaria");
      });
  };

  // Mientras carga datos, muestra mensaje
  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <>
      <Header />
      <MaquinariaForm
        initialData={maquinaria}
        onSubmit={handleSubmit}
        onCancel={handleCancel}
        modo="Editar"
      />
    </>
  );
}