import React, { useState, useEffect } from "react";
import "../Filters.css";
import Header from "../Header";
import { useNavigate } from "react-router-dom";

const AlquilerFilter = () => {
  const [alquileres, setAlquileres] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [filters, setFilters] = useState({
    fechaDesde: "",
    fechaHasta: "",
    dniCliente: "",
  });
  const [mensaje, setMensaje] = useState("");
  const navigate = useNavigate();
  useEffect(() => {
    console.log("useEffect ejecutado");
    fetch("http://localhost:5000/api/Alquiler/all")
      .then((res) => res.json())
      .then((data) => {
        setAlquileres(data);
        setFiltered(data);
        setMensaje("");
      })
      .catch((err) => {
        console.error("Error fetching alquileres:", err);
      });
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFilters((f) => ({ ...f, [name]: value }));
  };

  const handleFilter = async () => {
    setMensaje("");
    let result = alquileres;
    const { fechaDesde, fechaHasta, dniCliente } = filters;

    // Validación de fechas
    if (
      fechaDesde &&
      fechaHasta &&
      new Date(fechaHasta) < new Date(fechaDesde)
    ) {
      setFiltered([]);
      setMensaje(
        "La fecha de hasta es anterior a la fecha de desde. Por favor ingrese un rango de fechas correcto."
      );
      return;
    }

    if (fechaDesde) {
      const desde = new Date(fechaDesde);
      result = result.filter((a) => new Date(a.fecEfectivizacion) >= desde);
    }

    if (fechaHasta) {
      const hasta = new Date(fechaHasta);
      result = result.filter((a) => new Date(a.fecEfectivizacion) <= hasta);
    }

    // Validar existencia del cliente por DNI si se ingresó
    if (dniCliente.trim() !== "") {
      try {
        const response = await fetch(
          `http://localhost:5000/api/Cliente/byDNI?DNI=${dniCliente.trim()}`
        );
        if (!response.ok) {
          throw new Error("Cliente no encontrado");
        }

        const cliente = await response.json();
        if (!cliente) {
          setFiltered([]);
          setMensaje(
            "El DNI ingresado no se encuentra registrado en el sistema."
          );
          return;
        }

        result = result.filter((a) =>
          a.dniCliente.toLowerCase().includes(dniCliente.toLowerCase())
        );
      } catch (error) {
        console.error("Error al buscar cliente:", error);
        setFiltered([]);
        setMensaje(
          "El DNI ingresado no se encuentra registrado en el sistema."
        );
        return;
      }
    }

    if (result.length === 0) {
      if (fechaDesde || fechaHasta) {
        setMensaje(
          "No existen alquileres registrados en ese periodo de fechas."
        );
      } else {
        setMensaje("No se encontraron alquileres.");
      }
    } else {
      setMensaje("");
    }

    setFiltered(result);
  };

  return (
    <>
      <Header />
      <div style={{ display: "flex" }}>
        <aside className="filter-sidebar">
          <h3>Filtrar Alquileres</h3>
          <label>
            Fecha Desde:
            <input
              type="date"
              name="fechaDesde"
              value={filters.fechaDesde}
              onChange={handleChange}
            />
          </label>
          <label>
            Fecha Hasta:
            <input
              type="date"
              name="fechaHasta"
              value={filters.fechaHasta}
              onChange={handleChange}
            />
          </label>
          <label>
            DNI Cliente:
            <input
              type="text"
              name="dniCliente"
              placeholder="DNI Cliente"
              value={filters.dniCliente}
              onChange={handleChange}
            />
          </label>
          <button onClick={handleFilter}>Filtrar</button>
        </aside>

        <section style={{ flex: 1 }}>
          <div className="container">
            <h3>Resultados</h3>
            {mensaje && <p style={{ color: "red" }}>{mensaje}</p>}
          </div>
          <div
            style={{
              display: "grid",
              gap: "1rem",
              gridTemplateColumns: "repeat(auto-fill, minmax(250px, 1fr))",
            }}
          >
            {filtered.map((alquiler) => (
              <div
                key={alquiler.idAlquiler}
                onClick={() =>
                  navigate(`/DetalleAlquiler/${alquiler.idAlquiler}`)
                }
                style={{
                  border: "1px solid #ccc",
                  borderRadius: "8px",
                  padding: "1rem",
                  boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                  cursor: "pointer",
                  transition: "transform 0.2s, box-shadow 0.2s",
                }}
                onMouseEnter={(e) => {
                  e.currentTarget.style.transform = "scale(1.02)";
                  e.currentTarget.style.boxShadow =
                    "0 4px 8px rgba(0,0,0,0.15)";
                }}
                onMouseLeave={(e) => {
                  e.currentTarget.style.transform = "scale(1)";
                  e.currentTarget.style.boxShadow = "0 2px 4px rgba(0,0,0,0.1)";
                }}
              >
                <p>
                  <strong>ID:</strong> {alquiler.idAlquiler}
                </p>
                <p>
                  <strong>Fecha Efectivización:</strong>{" "}
                  {new Date(alquiler.fecEfectivizacion).toLocaleDateString()}
                </p>
                <p>
                  <strong>DNI Cliente:</strong> {alquiler.dniCliente}
                </p>
                <p>
                  <strong>Status:</strong> {alquiler.status}
                </p>
                <p>
                  <strong>Reserva:</strong>{" "}
                  {alquiler.reserva ? (
                    <>
                      Desde:{" "}
                      {new Date(
                        alquiler.reserva.fecInicio
                      ).toLocaleDateString()}{" "}
                      - Hasta:{" "}
                      {new Date(alquiler.reserva.fecFin).toLocaleDateString()}
                    </>
                  ) : (
                    "N/A"
                  )}
                </p>
              </div>
            ))}
          </div>
        </section>
      </div>
    </>
  );
};

export default AlquilerFilter;
