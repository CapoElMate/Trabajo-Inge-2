import React, { useEffect, useState } from "react";
import { useAuth } from "../AuthContext";

const PermitFileUploader = () => {
  const { user } = useAuth();
  const [cliente, setCliente] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Form state
  const [permiso, setPermiso] = useState("");
  const [fecEmision, setFecEmision] = useState("");
  const [fecVencimiento, setFecVencimiento] = useState("");
  const [imagen, setImagen] = useState(null);
  const [imagenBase64, setImagenBase64] = useState("");

  const [mensaje, setMensaje] = useState("");

  // Obtener cliente
  useEffect(() => {
    const fetchCliente = async () => {
      try {
        const response = await fetch(
          `http://localhost:5000/api/Cliente/byEmail?email=${encodeURIComponent(user.userName)}`
        );
        if (!response.ok) {
          throw new Error("Error al obtener el cliente");
        }
        const data = await response.json();
        setCliente(data.usuarioRegistrado);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };
    if (user?.userName) {
      fetchCliente();
    }
  }, [user]);

  // Convertir imagen a base64
  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setImagen(file);
    const reader = new FileReader();
    reader.onloadend = () => {
      setImagenBase64(reader.result.split(",")[1]); // solo base64 sin encabezado data:
    };
    if (file) reader.readAsDataURL(file);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!permiso || !fecEmision || !fecVencimiento || !imagenBase64) {
      setMensaje("Por favor completá todos los campos.");
      return;
    }

    try {
      // Paso 1: Enviar permiso
      const permisoResponse = await fetch("http://localhost:5000/api/PermisosEspeciales/agregarPermisoUsuario", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          dniCliente: cliente.dni,
          fecEmision,
          fecVencimiento,
          status: "Pendiente de validacion",
          permiso
        })
      });

      if (!permisoResponse.ok) {
        throw new Error("Error al guardar el permiso.");
      }

      // Paso 2: Enviar imagen
      const archivoResponse = await fetch("http://localhost:5000/api/Archivo", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          entidadID: cliente.dni,
          tipoEntidad: 2,
          nombre: permiso,
          descripcion: "Permiso especial cargado por el cliente",
          archivo: imagenBase64
        })
      });

      if (!archivoResponse.ok) {
        throw new Error("Error al subir la imagen del permiso.");
      }

      setMensaje("Permiso y archivo subidos con éxito.");
      // Reset form
      setPermiso("");
      setFecEmision("");
      setFecVencimiento("");
      setImagen(null);
      setImagenBase64("");
    } catch (err) {
      setMensaje(err.message);
    }
  };

  if (loading) return <p>Cargando cliente...</p>;
  if (error) return <p>Error: {error}</p>;
  if (!cliente) return <p>No se encontró el cliente.</p>;

  return (
    <div className="p-4 max-w-md mx-auto border rounded-md shadow">
      <h2 className="text-xl font-bold mb-4">Subir Permiso Especial</h2>

      {mensaje && <p className="mb-2 text-blue-600">{mensaje}</p>}

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium">Tipo de permiso</label>
          <input
            type="text"
            value={permiso}
            onChange={(e) => setPermiso(e.target.value)}
            className="w-full border rounded px-2 py-1"
            required
          />
        </div>

        <div>
          <label className="block text-sm font-medium">Fecha de emisión</label>
          <input
            type="date"
            value={fecEmision}
            onChange={(e) => setFecEmision(e.target.value)}
            className="w-full border rounded px-2 py-1"
            required
          />
        </div>

        <div>
          <label className="block text-sm font-medium">Fecha de vencimiento</label>
          <input
            type="date"
            value={fecVencimiento}
            onChange={(e) => setFecVencimiento(e.target.value)}
            className="w-full border rounded px-2 py-1"
            required
          />
        </div>

        <div>
          <label className="block text-sm font-medium">Subir imagen del permiso</label>
          <input
            type="file"
            accept="image/*"
            onChange={handleImageChange}
            className="w-full"
            required
          />
        </div>

        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          Enviar permiso
        </button>
      </form>
    </div>
  );
};

export default PermitFileUploader;
