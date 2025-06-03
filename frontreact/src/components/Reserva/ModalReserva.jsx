import React, { useState, useEffect } from "react";
import "../Modal.css";
import "./ModalReserva.css";
import SelectInput from "../SelectInput";
import TextInput from "../TextInput";
import { initMercadoPago, Wallet } from "@mercadopago/sdk-react";

export default function ModalReserva({
  idPublicacion,
  precioPorDia,
  isOpen,
  onClose,
  onReservar,
}) {
  initMercadoPago("APP_USR-17034ece-e13c-4fa1-9151-a1e3335e6f39");
  const [fechaInicio, setFechaInicio] = useState("");
  const [fechaFin, setFechaFin] = useState("");
  const [entrega, setEntrega] = useState("");
  const [calle, setCalle] = useState("");
  const [altura, setAltura] = useState("");
  const [piso, setPiso] = useState("");
  const [dpto, setDpto] = useState("");
  const [mostrarBotonPago, setMostrarBotonPago] = useState(false);
  const [mostrarPagoExitoso, setMostrarPagoExitoso] = useState(false);
  const [mostrarPagoError, setMostrarPagoError] = useState(false);
  const [opcionesTipoEntrega, setOpcionesTipoEntrega] = useState();

  useEffect(() => {
    fetch(`http://localhost:5000/api/TipoEntrega/all`)
      .then((res) => res.json())
      .then((data) => setOpcionesTipoEntrega(data))
      .catch((error) =>
        console.error("Error al cargar tipos de entrega:", error)
      );
  }, []);

  if (!isOpen) return null;

  const buttonStyle = {
    backgroundColor: "#dc3545",
    color: "white",
    padding: "10px 16px",
    border: "none",
    borderRadius: "8px",
    cursor: "pointer",
    fontSize: "16px",
    fontWeight: "500",
    boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
    transition: "background-color 0.2s ease",
    margin: "0 8px",
  };

  const handleMouseOver = (e) => {
    e.target.style.backgroundColor = "#b32a36";
  };

  const handleMouseOut = (e) => {
    e.target.style.backgroundColor = "#dc3545";
  };

  const handleReservarClick = () => {
    if (!fechaInicio || !fechaFin) {
      alert("Por favor, selecciona ambas fechas.");
      return;
    }
    if (fechaFin < fechaInicio) {
      alert("La fecha fin debe ser mayor o igual a la fecha inicio.");
      return;
    }
    setFechaInicio("");
    setFechaFin("");
    setEntrega("");
    setCalle("");
    setAltura("");
    setPiso("");
    setDpto("");
    setMostrarBotonPago(true);
    handlePago();
  };

  const handlePago = () => {
    //procesa el pago con el back
    let fecInicioObj = new Date(fechaInicio);
    let fecFinObj = new Date(fechaFin);

    // Calcular la diferencia en milisegundos
    let diffMs = fecFinObj - fecInicioObj;

    // Convertir la diferencia a días
    let dias = Math.ceil(diffMs / (1000 * 60 * 60 * 24));

    // Calcular el monto final
    let monto = dias * precioPorDia;

    // if(todoOk)
    // {
    const reserva = {
      fecInicio: fecInicioObj.toISOString(),
      fecFin: fecFinObj.toISOString(),
      status: "Lista para efectivizar",
      calle,
      altura,
      dpto,
      piso,
      // pago: {
      //   nroPago: ,
      //   fecPago: new Date().toISOString(),
      // },
      tipoEntrega: {
        entrega,
      },
      idAlquiler: null,
      dniCliente: "2050022",
      idPublicacion,
      montoTotal: monto,
    };

    //Crear reserva
    fetch("http://localhost:5000/api/Reserva", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(reserva),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Error al crear la reserva");
        }
        console.log(response.json());
      })
      .then((data) => {
        console.log("Reserva creada con éxito:", data);
        setMostrarBotonPago(false);
        setMostrarPagoExitoso(true);
        // Aquí podrías actualizar UI, setear estado de éxito, etc.
      })
      .catch((error) => {
        console.error("Error en la petición:", error);
        // Aquí podrías setear estado de error para mostrar mensaje al usuario.
      });
    // }
    // else
    // {
    //     setMostrarPagoError(true);
    // }
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content" style={{ maxWidth: 400 }}>
        <h2>Reserva</h2>

        <label htmlFor="fechaInicio">Fecha de inicio:</label>
        <input
          type="date"
          id="fechaInicio"
          value={fechaInicio}
          onChange={(e) => setFechaInicio(e.target.value)}
          style={{
            width: "100%",
            padding: 8,
            marginBottom: 12,
            boxSizing: "border-box",
          }}
        />

        <label htmlFor="fechaFin">Fecha de fin:</label>
        <input
          type="date"
          id="fechaFin"
          value={fechaFin}
          onChange={(e) => setFechaFin(e.target.value)}
          style={{
            width: "100%",
            padding: 8,
            marginBottom: 20,
            boxSizing: "border-box",
          }}
        />

        <div className="entrega-container">
          <SelectInput
            optionCompleja={false}
            label="Tipo de entrega: "
            options={(opcionesTipoEntrega || []).map((t) => ({
              value: t.entrega,
              label: t.entrega,
            }))}
            onChange={(e) => setEntrega(e.target.value)}
            value={entrega || ""}
            required
          />

          {entrega === "A domicilio" && (
            <div className="direccion-container">
              <TextInput
                label="Calle:"
                type="text"
                value={calle}
                onChange={(e) => setCalle(e.target.value)}
                required
              />

              <TextInput
                label="Altura:"
                type="text"
                value={altura}
                onChange={(e) => setAltura(e.target.value)}
                required
              />

              <TextInput
                label="Piso:"
                type="text"
                value={piso}
                onChange={(e) => setPiso(e.target.value)}
                required
              />

              <TextInput
                label="Dpto.:"
                type="text"
                value={dpto}
                onChange={(e) => setDpto(e.target.value)}
                required
              />
            </div>
          )}
        </div>

        <div style={{ display: "flex", justifyContent: "center" }}>
          {mostrarBotonPago ? (
            <div style={{ display: "flex", justifyContent: "center" }}>
              <Wallet
                initialization={{
                  preferenceId:
                    "2462257991-513c4341-fc38-41fb-9e91-5e2fc02edb37",
                }}
              />
            </div>
          ) : mostrarPagoExitoso ? (
            <div style={{ textAlign: "center" }}>
              <p style={{ color: "green", fontWeight: "bold" }}>
                ¡Pago recibido y reserva concretada exitosamente!
              </p>
              <button
                style={buttonStyle}
                onMouseOver={handleMouseOver}
                onMouseOut={handleMouseOut}
                onClick={() => onReservar("success")}
              >
                Volver al inicio
              </button>
            </div>
          ) : mostrarPagoError ? (
            <div style={{ textAlign: "center" }}>
              <p style={{ color: "red", fontWeight: "bold" }}>
                No se pudo concretar el pago por un problema con MercadoPago,
                intente de nuevo más tarde.
              </p>
              <button
                style={buttonStyle}
                onMouseOver={handleMouseOver}
                onMouseOut={handleMouseOut}
                onClick={() => onReservar("error")}
              >
                Volver al inicio
              </button>
            </div>
          ) : (
            <div style={{ display: "flex", justifyContent: "center" }}>
              <button
                style={buttonStyle}
                onMouseOver={handleMouseOver}
                onMouseOut={handleMouseOut}
                onClick={handleReservarClick}
              >
                Reservar
              </button>
              <button
                style={buttonStyle}
                onMouseOver={handleMouseOver}
                onMouseOut={handleMouseOut}
                onClick={() => {
                  setFechaInicio("");
                  setFechaFin("");
                  setEntrega("");
                  setCalle("");
                  setAltura("");
                  setPiso("");
                  setDpto("");
                  onClose();
                }}
              >
                Cancelar
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
