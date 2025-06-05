import React, { useState, useEffect } from "react";
import "../Filters.css"; // Asegúrate de tener este archivo CSS para los estilos
import Header from "../Header";

const AlquilerFilter = () => {
  const [alquileres, setAlquileres] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [filters, setFilters] = useState({
    fechaDesde: "",
    fechaHasta: "",
    dniCliente: "",
  });

  useEffect(() => {
    console.log("useEffect ejecutado");
    fetch("http://localhost:5000/api/Alquiler/all")
      .then((res) => res.json())
      .then((data) => {
        setAlquileres(data);
        setFiltered(data);
      })
      .catch((err) => {
        console.error("Error fetching alquileres:", err);
      });
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFilters((f) => ({ ...f, [name]: value }));
  };

  const handleFilter = () => {
    let result = alquileres;

    if (filters.fechaDesde) {
      const desde = new Date(filters.fechaDesde);
      result = result.filter((a) => new Date(a.fecEfectivizacion) >= desde);
    }

    if (filters.fechaHasta) {
      const hasta = new Date(filters.fechaHasta);
      result = result.filter((a) => new Date(a.fecEfectivizacion) <= hasta);
    }

    if (filters.dniCliente.trim() !== "") {
      result = result.filter((a) =>
        a.dniCliente.toLowerCase().includes(filters.dniCliente.toLowerCase())
      );
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
            {filtered.length === 0 && <p>No se encontraron alquileres.</p>}
          </div>
          <ul>
            {filtered.map((alquiler) => (
              <li key={alquiler.idAlquiler} style={{ marginBottom: "1rem" }}>
                <strong>ID:</strong> {alquiler.idAlquiler} <br />
                <strong>Fecha Efectivización:</strong>{" "}
                {new Date(alquiler.fecEfectivizacion).toLocaleDateString()}{" "}
                <br />
                <strong>DNI Cliente:</strong> {alquiler.dniCliente} <br />
                <strong>Status:</strong> {alquiler.status} <br />
                <strong>Reserva:</strong>{" "}
                {alquiler.reserva ? (
                  <>
                    Desde:{" "}
                    {new Date(alquiler.reserva.fecInicio).toLocaleDateString()}{" "}
                    - Hasta:{" "}
                    {new Date(alquiler.reserva.fecFin).toLocaleDateString()}
                  </>
                ) : (
                  "N/A"
                )}
              </li>
            ))}
          </ul>
        </section>
      </div>
    </>
  );
};

export default AlquilerFilter;
