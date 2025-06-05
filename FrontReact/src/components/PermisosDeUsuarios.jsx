import React, { useEffect, useState } from "react";
import Header from "./Header";
const PermisosDeUsuarios = () => {
  const [permisos, setPermisos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [errores, setErrores] = useState(null);

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

      // Eliminar el permiso validado de la lista
      setPermisos(prev => prev.filter(p => !(p.dniCliente === dni && p.permiso === permisoEspecial)));
    } catch (err) {
      alert(`Error al validar permiso de ${dni}: ${err.message}`);
    }
  };

  if (loading) return <p>Cargando permisos...</p>;
  if (errores) return <p style={{ color: "red" }}>{errores}</p>;

  return (
    <div>
         <Header/>
      <h2>Permisos Pendientes de Validación</h2>
      {permisos.map((permiso, idx) => (
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
            style={{ marginTop: 10, backgroundColor: "green", color: "white", padding: "5px 10px", border: "none", cursor: "pointer" }}
          >
            Aceptar permiso
          </button>
        </div>
      ))}
    </div>
  );
};

export default PermisosDeUsuarios;
