import React, { useState, useEffect } from "react";
import "../Modal.css";
import "./ModalReserva.css";
import SelectInput from "../SelectInput";
import TextInput from "../TextInput";
import { initMercadoPago, Wallet } from "@mercadopago/sdk-react";
import { generarPreferenciaPago } from "./PagoMeLi/generarPreferenciaPago";
import { useAuth } from "../../AuthContext";

export default function ModalReserva({
  publicacion,
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
  // const [mostrarPagoExitoso, setMostrarPagoExitoso] = useState(false);
  // const [mostrarPagoError, setMostrarPagoError] = useState(false);
  const [opcionesTipoEntrega, setOpcionesTipoEntrega] = useState();
  const { user, loadAuth } = useAuth();
  const [cliente, setCliente] = useState();
  const [MyPreferenciaPago, setMyPreferenciaPago] = useState();
  const [errors, setErrors] = useState();

  useEffect(() => {
    if (!user?.userName) return; // Espera a que user esté definido

    const fetchCliente = async () => {
      try {
        const getDNI = await fetch(
          `http://localhost:5000/api/Usuario/byEmail?email=${user.userName}`
        )
          .then((res) => res.json())
          .then((data) => data.dni);

        const response = await fetch(
          `http://localhost:5000/api/Cliente/byDNI?DNI=${getDNI}`
        );
        if (!response.ok) throw new Error("Error al obtener el cliente");
        const data = await response.json();
        setCliente(data);
      } catch (error) {
        console.error("Error al cargar el cliente:", error);
      }
    };

    fetchCliente();
  }, [user?.userName]);

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

  const handleReservarClick = async () => {
    // Limpiar errores anteriores
    setErrors({});

    const nuevosErrores = {};

    // Validación de fechas
    if (!fechaInicio || !fechaFin) {
      nuevosErrores.faltaFech = "Por favor, selecciona ambas fechas.";
    } else if (fechaFin < fechaInicio) {
      nuevosErrores.fechaFinInvalida =
        "Por favor ingrese una fecha de fin posterior a la fecha de inicio.";
    }

    // Validación de tipo de entrega
    if (!entrega) {
      nuevosErrores.faltaTipoEntrega =
        "Por favor, selecciona un tipo de entrega.";
    }

    // Validación de dirección si corresponde
    if (entrega === "A domicilio") {
      if (!calle || calle.trim() === "") {
        nuevosErrores.faltaCalle = "Por favor, ingresá la calle.";
      }
      if (!altura || altura.trim() === "") {
        nuevosErrores.faltaAltura = "Por favor, ingresá la altura.";
      }
    }

    if (Object.keys(nuevosErrores).length > 0) {
      setErrors(nuevosErrores);
      return;
    }

    // Validar permisos especiales
    try {
      const response = await fetch(
        "http://localhost:5000/api/PermisosEspeciales/AllPermisosUsuarios"
      );
      if (!response.ok)
        throw new Error("Error al obtener permisos del cliente");
      const data = await response.json();

      const permisosCliente = data
        .filter(
          (p) =>
            p.dniCliente === cliente.usuarioRegistrado.dni &&
            p.status === "Vigente"
        )
        .map((p) => p.permiso);

      const permisosRequeridos = publicacion.maquina.permisosEspeciales || [];

      const tieneTodosLosPermisos = permisosRequeridos.every((pr) =>
        permisosCliente.includes(pr)
      );

      if (!tieneTodosLosPermisos) {
        setErrors({
          permisosInsuficientes:
            "Permisos insuficientes para realizar la reserva. Intenta nuevamente cuando tengas el/los permisos cargados y aprobados",
        });
        return;
      }
    } catch (error) {
      console.error("Error al validar permisos:", error);
      console.log(
        "No se pudo validar permisos especiales. Intente nuevamente más tarde."
      );
      return;
    }

    // Si todo es válido, generar preferencia de pago
    const reserva = handlePago();
    const idPrefPago = await generarPreferenciaPago(
      publicacion.titulo,
      reserva
    );
    console.log(idPrefPago);
    setMyPreferenciaPago(idPrefPago);
    setMostrarBotonPago(true);
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
      fecInicio: fecInicioObj.toISOString().split("T")[0],
      fecFin: fecFinObj.toISOString().split("T")[0],
      status: "Lista para efectivizar",
      calle,
      altura,
      dpto,
      piso,
      tipoEntrega: {
        entrega,
      },
      idAlquiler: null,
      dniCliente: cliente.usuarioRegistrado.dni,
      idPublicacion: publicacion.idPublicacion,
      montoTotal: monto,
    };

    return reserva;
  };

  const handleFechaFinChange = (e) => {
    const nuevaFechaFin = e.target.value;
    setFechaFin(nuevaFechaFin);

    // Limpiar el error si existía
    setErrors((prev) => ({
      ...prev,
      fechaFinInvalida: null,
    }));
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
        {errors?.faltaFech && !fechaInicio && (
          <span style={{ color: "red", fontSize: "0.9rem" }}>
            {errors.faltaFech}
          </span>
        )}

        <label htmlFor="fechaFin">Fecha de fin:</label>
        <input
          type="date"
          id="fechaFin"
          value={fechaFin}
          onChange={handleFechaFinChange}
          style={{
            width: "100%",
            padding: 8,
            marginBottom: 20,
            boxSizing: "border-box",
          }}
        />
        {errors?.faltaFech && !fechaFin && (
          <span style={{ color: "red", fontSize: "0.9rem" }}>
            {errors.faltaFech}
          </span>
        )}

        <div className="entrega-container">
          <SelectInput
            optionCompleja={false}
            label="Tipo de entrega: "
            options={(opcionesTipoEntrega || []).map((t) => ({
              value: t.entrega,
              label: t.entrega,
            }))}
            onChange={(e) => {
              setEntrega(e.target.value);
              setErrors((prev) => ({
                ...prev,
                faltaTipoEntrega: null,
              }));
            }}
            value={entrega || ""}
            required
          />
          {errors?.faltaTipoEntrega && (
            <span style={{ color: "red", fontSize: "0.9rem" }}>
              {errors.faltaTipoEntrega}
            </span>
          )}

          {entrega === "A domicilio" && (
            <div className="direccion-container">
              <TextInput
                label="Calle:"
                type="text"
                value={calle}
                onChange={(e) => {
                  setCalle(e.target.value);
                  setErrors((prev) => ({
                    ...prev,
                    faltaCalle: null,
                  }));
                }}
                required
              />
              {errors?.faltaCalle && (
                <span style={{ color: "red", fontSize: "0.9rem" }}>
                  {errors.faltaCalle}
                </span>
              )}

              <TextInput
                label="Altura:"
                type="text"
                value={altura}
                onChange={(e) => {
                  setAltura(e.target.value);
                  setErrors((prev) => ({
                    ...prev,
                    faltaAltura: null,
                  }));
                }}
                required
              />
              {errors?.faltaAltura && (
                <span style={{ color: "red", fontSize: "0.9rem" }}>
                  {errors.faltaAltura}
                </span>
              )}

              <TextInput
                label="Piso:"
                type="text"
                value={piso}
                onChange={(e) => setPiso(e.target.value)}
              />

              <TextInput
                label="Dpto.:"
                type="text"
                value={dpto}
                onChange={(e) => setDpto(e.target.value)}
              />
            </div>
          )}
        </div>

        <div style={{ display: "flex", justifyContent: "center" }}>
          {mostrarBotonPago ? (
            <div style={{ display: "flex", justifyContent: "center" }}>
              <Wallet initialization={{ preferenceId: MyPreferenciaPago }} />
            </div>
          ) : (
            <div
              style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
              }}
            >
              {errors?.permisosInsuficientes && (
                <span
                  style={{
                    color: "red",
                    fontSize: "0.9rem",
                    marginBottom: "0.3rem",
                  }}
                >
                  {errors.permisosInsuficientes}
                </span>
              )}
              {errors?.fechaFinInvalida && (
                <span
                  style={{
                    color: "red",
                    fontSize: "0.9rem",
                    marginBottom: "0.5rem",
                  }}
                >
                  {errors.fechaFinInvalida}
                </span>
              )}
              <div
                style={{
                  display: "flex",
                  justifyContent: "center",
                  gap: "1rem",
                }}
              >
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
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
