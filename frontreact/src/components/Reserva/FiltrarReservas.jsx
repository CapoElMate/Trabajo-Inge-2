import React, { useState, useEffect } from "react";
import "../Filters.css";
import Header from "../Header";
import { useNavigate } from "react-router-dom";

// Dentro del componente:

const ReservaFilter = () => {
  const [reservas, setReservas] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [filters, setFilters] = useState({
    fechaDesde: "",
    fechaHasta: "",
    dniCliente: "",
  });
  const [mensaje, setMensaje] = useState("");
  const navigate = useNavigate();
  useEffect(() => {
    fetch("http://localhost:5000/api/Reserva/all")
      .then((res) => res.json())
      .then((data) => {
        setReservas(data);
        setFiltered(data);
        setMensaje(""); // limpia mensajes al cargar
      })
      .catch((err) => {
        console.error("Error fetching reservas:", err);
      });
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFilters((prev) => ({ ...prev, [name]: value }));
  };

  const handleFilter = async () => {
    setMensaje("");

    // Validar fechas
    const { fechaDesde, fechaHasta, dniCliente } = filters;
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

    let result = reservas;

    if (fechaDesde) {
      const desde = new Date(fechaDesde);
      result = result.filter((r) => new Date(r.fecInicio) >= desde);
    }

    if (fechaHasta) {
      const hasta = new Date(fechaHasta);
      result = result.filter((r) => new Date(r.fecFin) <= hasta);
    }

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

        result = result.filter((r) =>
          r.dniCliente.toLowerCase().includes(dniCliente.toLowerCase())
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
        setMensaje("No se encontraron reservas.");
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
          <h3>Filtrar Reservas</h3>
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
            {filtered.map((reserva) => (
              <div
                key={reserva.idReserva}
                onClick={() => navigate(`/DetalleReserva/${reserva.idReserva}`)}
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
                  <strong>ID:</strong> {reserva.idReserva}
                </p>
                <p>
                  <strong>Fecha Inicio:</strong>{" "}
                  {new Date(reserva.fecInicio).toLocaleDateString()}
                </p>
                <p>
                  <strong>Fecha Fin:</strong>{" "}
                  {new Date(reserva.fecFin).toLocaleDateString()}
                </p>
                <p>
                  <strong>DNI Cliente:</strong> {reserva.dniCliente}
                </p>
                <p>
                  <strong>Estado:</strong> {reserva.status}
                </p>
              </div>
            ))}
          </div>
        </section>
      </div>
    </>
  );
};

export default ReservaFilter;
