import React, { useEffect, useState } from "react";
import Header from "./Header";

const PermisosDeUsuarios = () => {
  const [permisos, setPermisos] = useState([]);
  const [permisosFiltrados, setPermisosFiltrados] = useState([]);
  const [loading, setLoading] = useState(true);
  const [errores, setErrores] = useState(null);
  const [dniFiltro, setDniFiltro] = useState("");

  useEffect(() => {
    const fetchPermisos = async () => {
      try {
        const res = await fetch("http://localhost:5000/api/PermisosEspeciales/AllPermisosUsuarios");
        const data = await res.json();

        const pendientes = data.filter(p => p.status === "Pendiente de validacion");

        const permisosConImagenes = await Promise.all(
          pendientes.map(async (permiso) => {
            try {
              const resArchivo = await fetch(
                `http://localhost:5000/api/Archivo/User/PermisosEspeciales?DNI=${permiso.dniCliente}`
              );
              const archivos = await resArchivo.json();

              const archivo = archivos.find(a => a.nombre === permiso.permiso);

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
        setPermisosFiltrados(permisosConImagenes);
      } catch (error) {
        console.error("Error al cargar permisos:", error);
        setErrores("Error al obtener permisos.");
      } finally {
        setLoading(false);
      }
    };

    fetchPermisos();
  }, []);

  const aceptarPermiso = async (dni, permisoEspecial) => {
    const url = `http://localhost:5000/api/PermisosEspeciales/confirmPermisoEspecial?DNI=${dni}&permisoEspecial=${encodeURIComponent(permisoEspecial)}`;

    try {
      const res = await fetch(url, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!res.ok) throw new Error("No se pudo validar el permiso");

      setPermisos(prev => prev.filter(p => !(p.dniCliente === dni && p.permiso === permisoEspecial)));
      setPermisosFiltrados(prev => prev.filter(p => !(p.dniCliente === dni && p.permiso === permisoEspecial)));
    } catch (err) {
      alert(`Error al validar permiso de ${dni}: ${err.message}`);
    }
  };

  const filtrarPorDNI = () => {
    const filtro = dniFiltro.trim();
    if (filtro === "") {
      setPermisosFiltrados(permisos);
    } else {
      const filtrados = permisos.filter(p => p.dniCliente.includes(filtro));
      setPermisosFiltrados(filtrados);
    }
  };

  if (loading) return <p>Cargando permisos...</p>;
  if (errores) return <p style={{ color: "red" }}>{errores}</p>;

  return (
    <div>
      <Header />
      <h1>Permisos Pendientes de Validación</h1>
      <div style={{ display: "flex" }}>
        {/* Menú de filtro lateral */}
        <div style={{ width: "250px", marginRight: "20px" }}>
          <h4>Filtrar por DNI</h4>
          <input
            type="text"
            placeholder="Ingresar DNI"
            value={dniFiltro}
            onChange={e => setDniFiltro(e.target.value)}
            style={{ width: "100%", padding: "5px" }}
          />
          <button
            onClick={filtrarPorDNI}
            style={{
              marginTop: 10,
              backgroundColor: "#007bff",
              color: "white",
              padding: "5px 10px",
              border: "none",
              cursor: "pointer",
              width: "100%"
            }}
          >
            Filtrar
          </button>
        </div>

        {/* Lista de permisos */}
        <div style={{ flex: 1 }}>
          {permisosFiltrados.map((permiso, idx) => (
            <div key={idx} style={{ border: "1px solid #ccc", padding: 10, marginBottom: 15 }}>
              <p><strong>DNI:</strong> {permiso.dniCliente}</p>
              <p><strong>Permiso:</strong> {permiso.permiso}</p>
              <p><strong>Emitido:</strong> {new Date(permiso.fecEmision).toLocaleDateString()}</p>
              <p><strong>Vencimiento:</strong> {new Date(permiso.fecVencimiento).toLocaleDateString()}</p>
              {permiso.imagenBase64 ? (
                <img
                  src={`data:image/jpeg;base64,${permiso.imagenBase64}`}
                  alt={permiso.nombreArchivo}
                  style={{ maxWidth: "300px", marginTop: 10 }}
                />
              ) : (
                <p style={{ color: "orange" }}>Imagen no disponible</p>
              )}
              <button
                onClick={() => aceptarPermiso(permiso.dniCliente, permiso.permiso)}
                style={{
                  marginTop: 10,
                  backgroundColor: "green",
                  color: "white",
                  padding: "5px 10px",
                  border: "none",
                  cursor: "pointer"
                }}
              >
                Aceptar permiso
              </button>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default PermisosDeUsuarios;
