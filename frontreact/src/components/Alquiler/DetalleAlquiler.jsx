import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import StyledButton from "../CustomButton";
import "./DetalleAlquiler.css";

export default function DetalleAlquiler() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [alquiler, setAlquiler] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch(`http://localhost:5000/api/Alquiler/byId?id=${id}`)
      .then((res) => res.json())
      .then((data) => setAlquiler(data))
      .catch((err) => console.error("Error al cargar el alquiler:", err))
      .finally(() => setLoading(false));
  }, [id]);

  const handleCancelar = async () => {
    try {
      const res = await fetch(`http://localhost:5000/api/Alquiler/${id}`, {
        method: "DELETE",
      });
      if (!res.ok) throw new Error("Error al cancelar el alquiler");
      navigate("/HomePage");
    } catch (error) {
      console.error(error);
      alert("No se pudo cancelar el alquiler");
    }
  };

  if (loading) return <p className="detalle-loading">Cargando...</p>;
  if (!alquiler) return <p className="detalle-error">No se encontró el alquiler.</p>;

  return (
    <div className="detalle-alquiler-container">
      <h1>Detalle del Alquiler</h1>
      <div className="detalle-alquiler-info">
        <p><strong>Estado:</strong> {alquiler.status}</p>
        <p><strong>Fecha de Inicio:</strong> {alquiler.fecInicio}</p>
        <p><strong>Fecha de Fin:</strong> {alquiler.fecFin}</p>

        <h2>Maquinaria</h2>
        <p><strong>ID:</strong> {alquiler.maquina?.idMaquina}</p>
        <p><strong>Modelo:</strong> {alquiler.maquina?.modelo?.modelo}</p>
        <p><strong>Marca:</strong> {alquiler.maquina?.modelo?.marca?.marca}</p>
        <p><strong>Tipo:</strong> {alquiler.maquina?.tipoMaquina?.tipo}</p>
      </div>

      <StyledButton text="Cancelar Alquiler" onClick={handleCancelar} />
    </div>
  );
} 