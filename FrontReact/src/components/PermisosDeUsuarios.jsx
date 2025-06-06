import React, { useEffect, useState } from "react";
import Header from "./Header";
import { useAuth } from "../AuthContext";

const PermisosDeUsuarios = () => {
  const [permisos, setPermisos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [errores, setErrores] = useState(null);
  const [empleadoDNI, setEmpleadoDNI] = useState(null);
  const [mensajeError, setMensajeError] = useState("");
  const { user, loadAuth } = useAuth();

  useEffect(() => {
    const fetchEmpleadoDNI = async () => {
      try {
        if (!user?.userName) return;
        const res = await fetch(
          `http://localhost:5000/api/Usuario/byEmail?email=${user.userName}`
        );
        const data = await res.json();
        setEmpleadoDNI(data.dni);
      } catch (error) {
        console.error("Error al obtener DNI del empleado:", error);
        setErrores("Error al obtener el DNI del usuario.");
      }
    };

    const fetchPermisos = async () => {
      try {
        const res = await fetch(
          "http://localhost:5000/api/PermisosEspeciales/AllPermisosUsuarios"
        );
        const data = await res.json();

        const pendientes = data.filter(
          (p) => p.status === "Pendiente de validacion"
        );

        const permisosConImagenes = await Promise.all(
          pendientes.map(async (permiso) => {
            try {
              const resArchivo = await fetch(
                `http://localhost:5000/api/Archivo/User/PermisosEspeciales?DNI=${permiso.dniCliente}`
              );
              const archivos = await resArchivo.json();

              const archivo = archivos.find(
                (a) => a.nombre === permiso.permiso
              );

              return {
                ...permiso,
                imagenBase64: archivo ? archivo.archivoBase64 : null,
                nombreArchivo: archivo ? archivo.nombre : null,
              };
            } catch {
              return {
                ...permiso,
                imagenBase64: null,
                nombreArchivo: null,
              };
            }
          })
        );

        setPermisos(permisosConImagenes);
      } catch (error) {
        console.error("Error al cargar permisos:", error);
        setErrores("Error al obtener permisos.");
      } finally {
        setLoading(false);
      }
    };

    fetchEmpleadoDNI();
    fetchPermisos();
  }, [user]);

  const aceptarPermiso = async (dni, permisoEspecial) => {
    if (dni === empleadoDNI) {
      setMensajeError("No puede aprobar sus propios permisos");
      return;
    }

    const url = `http://localhost:5000/api/PermisosEspeciales/confirmPermisoEspecial?DNI=${dni}&permisoEspecial=${encodeURIComponent(
      permisoEspecial
    )}`;

    try {
      const res = await fetch(url, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!res.ok) throw new Error("No se pudo validar el permiso");

      setPermisos((prev) =>
        prev.filter(
          (p) => !(p.dniCliente === dni && p.permiso === permisoEspecial)
        )
      );
    } catch (err) {
      alert(`Error al validar permiso de ${dni}: ${err.message}`);
    }
  };

  const rechazarPermiso = async (dni, permisoEspecial) => {
    if (dni === empleadoDNI) {
      setMensajeError("No puede rechazar sus propios permisos");
      return;
    }

    const url = `http://localhost:5000/api/PermisosEspeciales/rechazarPermisoEspecial?DNI=${dni}&permisoEspecial=${encodeURIComponent(
      permisoEspecial
    )}`;

    try {
      const res = await fetch(url, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!res.ok) throw new Error("No se pudo rechazar el permiso");

      setPermisos((prev) =>
        prev.filter(
          (p) => !(p.dniCliente === dni && p.permiso === permisoEspecial)
        )
      );
    } catch (err) {
      alert(`Error al rechazar permiso de ${dni}: ${err.message}`);
    }
  };

  if (loading || empleadoDNI === null) return <p>Cargando permisos...</p>;
  if (errores) return <p style={{ color: "red" }}>{errores}</p>;

  return (
    <div>
      <Header />
      <h2>Permisos Pendientes de Validación</h2>
      {mensajeError && (
        <p style={{ color: "red", marginBottom: 10 }}>{mensajeError}</p>
      )}
      {permisos.map((permiso, idx) => (
        <div
          key={idx}
          style={{ border: "1px solid #ccc", padding: 10, marginBottom: 15 }}
        >
          <p>
            <strong>DNI:</strong> {permiso.dniCliente}
          </p>
          <p>
            <strong>Permiso:</strong> {permiso.permiso}
          </p>
          <p>
            <strong>Emitido:</strong>{" "}
            {new Date(permiso.fecEmision).toLocaleDateString()}
          </p>
          <p>
            <strong>Vencimiento:</strong>{" "}
            {new Date(permiso.fecVencimiento).toLocaleDateString()}
          </p>
          {permiso.imagenBase64 ? (
            <img
              src={`data:image/jpeg;base64,${permiso.imagenBase64}`}
              alt={permiso.nombreArchivo}
              style={{ maxWidth: "300px", marginTop: 10 }}
            />
          ) : (
            <p style={{ color: "orange" }}>Imagen no disponible</p>
          )}
          <div style={{ marginTop: 10 }}>
            <button
              onClick={() =>
                aceptarPermiso(permiso.dniCliente, permiso.permiso)
              }
              style={{
                backgroundColor: "green",
                color: "white",
                padding: "5px 10px",
                border: "none",
                cursor: "pointer",
                marginRight: 10,
              }}
            >
              Aceptar permiso
            </button>
            <button
              onClick={() =>
                rechazarPermiso(permiso.dniCliente, permiso.permiso)
              }
              style={{
                backgroundColor: "red",
                color: "white",
                padding: "5px 10px",
                border: "none",
                cursor: "pointer",
              }}
            >
              Rechazar permiso
            </button>
          </div>
        </div>
      ))}
    </div>
  );
};

export default PermisosDeUsuarios;
