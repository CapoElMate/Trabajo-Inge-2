import React, { useState, useEffect } from "react";
import "../Filters.css";
import Header from "../Header";

const ReservaFilter = () => {
  const [reservas, setReservas] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [filters, setFilters] = useState({
    fechaDesde: "",
    fechaHasta: "",
    dniCliente: "",
  });

  useEffect(() => {
    fetch("http://localhost:5000/api/Reserva/all")
      .then((res) => res.json())
      .then((data) => {
        setReservas(data);
        setFiltered(data);
      })
      .catch((err) => {
        console.error("Error fetching reservas:", err);
      });
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFilters((prev) => ({ ...prev, [name]: value }));
  };

  const handleFilter = () => {
    let result = reservas;

    if (filters.fechaDesde) {
      const desde = new Date(filters.fechaDesde);
      result = result.filter((r) => new Date(r.fecInicio) >= desde);
    }

    if (filters.fechaHasta) {
      const hasta = new Date(filters.fechaHasta);
      result = result.filter((r) => new Date(r.fecFin) <= hasta);
    }

    if (filters.dniCliente.trim() !== "") {
      result = result.filter((r) =>
        r.dniCliente.toLowerCase().includes(filters.dniCliente.toLowerCase())
      );
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
            {filtered.length === 0 && <p>No se encontraron reservas.</p>}
        </div>
        <ul>
          {filtered.map((reserva) => (
            <li key={reserva.idReserva} style={{ marginBottom: "1rem" }}>
              <strong>ID:</strong> {reserva.idReserva} <br />
              <strong>Fecha Inicio:</strong>{" "}
              {new Date(reserva.fecInicio).toLocaleDateString()} <br />
              <strong>Fecha Fin:</strong>{" "}
              {new Date(reserva.fecFin).toLocaleDateString()} <br />
              <strong>DNI Cliente:</strong> {reserva.dniCliente} <br />
              <strong>Estado:</strong> {reserva.estado} <br />
            </li>
          ))}
        </ul>
      </section>
    </div>
    </>
  );
};

export default ReservaFilter;