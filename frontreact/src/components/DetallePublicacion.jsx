import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import Modal from "react-modal";
import "./DetallePublicacion.css";

Modal.setAppElement("#root"); // necesario para accesibilidad

function DetallePublicacion() {
  const { id } = useParams();
  const [maquinaria, setMaquinaria] = useState(null);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [fechaInicio, setFechaInicio] = useState("");
  const [fechaFin, setFechaFin] = useState("");
  const [precioTotal, setPrecioTotal] = useState(0);

  useEffect(() => {
    fetch(`http://localhost:3001/maquinarias/${id}`)
      .then((res) => res.json())
      .then((data) => setMaquinaria(data))
      .catch((err) => console.error("Error al cargar la maquinaria", err));
  }, [id]);

  const calcularPrecio = () => {
    if (fechaInicio && fechaFin) {
      const inicio = new Date(fechaInicio);
      const fin = new Date(fechaFin);
      const dias = (fin - inicio) / (1000 * 60 * 60 * 24);
      if (dias >= 0) {
        setPrecioTotal(dias * maquinaria.precio);
      } else {
        setPrecioTotal(0);
      }
    }
  };

  const handleReserva = (e) => {
    e.preventDefault();
    alert(`Reserva realizada del ${fechaInicio} al ${fechaFin}.\nTotal: $${precioTotal}`);
    setModalIsOpen(false);
  };

  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <div className="detalle-contenedor">
      <h1>{maquinaria.nombre}</h1>
      <img src={maquinaria.imagen} alt={maquinaria.nombre} className="detalle-imagen" />
      <p>{maquinaria.descripcion}</p>
      <p><strong>Ubicación:</strong> {maquinaria.ubicacion}</p>
      <p><strong>Precio por día:</strong> ${maquinaria.precio}</p>

      <button className="detalle-boton" onClick={() => setModalIsOpen(true)}>
        Reservar Maquinaria
      </button>

      <Modal
        isOpen={modalIsOpen}
        onRequestClose={() => setModalIsOpen(false)}
        className="modal-detalle"
        overlayClassName="modal-overlay"
      >
        <h2>Reservar</h2>
        <form onSubmit={handleReserva} className="reserva-formulario">
          <label>Fecha de inicio:</label>
          <input
            type="date"
            value={fechaInicio}
            onChange={(e) => {
              setFechaInicio(e.target.value);
              calcularPrecio();
            }}
          />
          <label>Fecha de fin:</label>
          <input
            type="date"
            value={fechaFin}
            onChange={(e) => {
              setFechaFin(e.target.value);
              calcularPrecio();
            }}
          />
          <p><strong>Total estimado:</strong> ${precioTotal}</p>
          <button type="submit" className="detalle-boton">Confirmar Reserva</button>
        </form>
      </Modal>
    </div>
  );
}

export default DetallePublicacion;
