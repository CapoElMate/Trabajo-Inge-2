import React, { useEffect, useState } from "react";
import { useAuth } from "../AuthContext"; 
import Header from "./Header";

const PermitFileUploader = () => {
  const { user } = useAuth(); 

  const [cliente, setCliente] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [permiso, setPermiso] = useState("");
  const [fecEmision, setFecEmision] = useState("");
  const [fecVencimiento, setFecVencimiento] = useState("");
  const [imagen, setImagen] = useState(null); 

  const [mensaje, setMensaje] = useState("");

  const [permisosDisponibles, setPermisosDisponibles] = useState([]);

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

  useEffect(() => {
    const getPermisosEspeciales = async () => {
      try {
        const response = await fetch("http://localhost:5000/api/PermisosEspeciales/all");
        if (!response.ok) throw new Error("Error al obtener los permisos especiales");
        const data = await response.json();
        setPermisosDisponibles(data);
      } catch (error) {
        console.error("Error fetching permisos especiales:", error);
        setPermisosDisponibles([]);
      }
    };
    getPermisosEspeciales();
  }, []);

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setImagen(file);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!permiso || !fecEmision || !fecVencimiento || !imagen) {
      setMensaje("Por favor completá todos los campos y subí la imagen.");
      return; 
    }

    try {
      const permisoResponse = await fetch("http://localhost:5000/api/PermisosEspeciales/agregarPermisoUsuario", {
        method: "POST",
        headers: { "Content-Type": "application/json" }, 
        body: JSON.stringify({
          dniCliente: cliente.dni, 
          fecEmision,
          fecVencimiento,
          status: "Pendiente de validacion", 
          permiso 
        })
      });

      if (!permisoResponse.ok) {
        const errorData = await permisoResponse.json(); 
        throw new Error(`Error al guardar el permiso: ${errorData.message || permisoResponse.statusText}`);
      }

      const formData = new FormData();
      formData.append("entidadID", cliente.dni); 
      formData.append("tipoEntidad", "2"); 
      formData.append("nombre", permiso); 
      formData.append("descripcion", "Permiso especial cargado por el cliente"); 
      formData.append("archivo", imagen); 

      const archivoResponse = await fetch("http://localhost:5000/api/Archivo", {
        method: "POST",
        body: formData 
      });

      if (!archivoResponse.ok) {
        const errorText = await archivoResponse.text(); 
        throw new Error(`Error al subir la imagen del permiso: ${errorText}`);
      }

      setMensaje("Permiso y archivo subidos con éxito.");
      setPermiso("");
      setFecEmision("");
      setFecVencimiento("");
      setImagen(null); 
      e.target.reset(); 
    } catch (err) {
      setMensaje(err.message);
    }
  };

  if (loading) return <p className="text-center p-4">Cargando cliente...</p>;
  if (error) return <p className="text-center p-4 text-red-500">Error: {error}</p>;
  if (!cliente) return <p className="text-center p-4 text-orange-500">No se encontró el cliente.</p>;

  return (<>
     <Header/>
    <div className="p-4 max-w-md mx-auto border border-gray-200 rounded-lg shadow-lg bg-white font-inter">
      <h2 className="text-2xl font-semibold mb-6 text-center text-gray-800">Subir Permiso Especial</h2>
      {mensaje && (
        <p className={`mb-4 text-center p-2 rounded-md ${mensaje.includes("éxito") ? "bg-green-100 text-green-700" : "bg-red-100 text-red-700"}`}>
          {mensaje}
        </p>
      )}

      <form onSubmit={handleSubmit} className="space-y-5">
        <div>
          <label htmlFor="permiso-select" className="block text-sm font-medium text-gray-700 mb-1">Tipo de permiso</label>
          <select
            id="permiso-select"
            value={permiso}
            onChange={(e) => setPermiso(e.target.value)}
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-800 focus:ring-blue-500 focus:border-blue-500 transition duration-150 ease-in-out shadow-sm"
            required
          >
            <option value="">Seleccioná un permiso</option>
            {permisosDisponibles.map((p, index) => (
              <option key={index} value={p.permiso}>
                {p.permiso}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label htmlFor="fecEmision" className="block text-sm font-medium text-gray-700 mb-1">Fecha de emisión</label>
          <input
            type="date"
            id="fecEmision"
            value={fecEmision}
            onChange={(e) => setFecEmision(e.target.value)}
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-800 focus:ring-blue-500 focus:border-blue-500 transition duration-150 ease-in-out shadow-sm"
            required
          />
        </div>

        <div>
          <label htmlFor="fecVencimiento" className="block text-sm font-medium text-gray-700 mb-1">Fecha de vencimiento</label>
          <input
            type="date"
            id="fecVencimiento"
            value={fecVencimiento}
            onChange={(e) => setFecVencimiento(e.target.value)}
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-800 focus:ring-blue-500 focus:border-blue-500 transition duration-150 ease-in-out shadow-sm"
            required
          />
        </div>

        <div>
          <label htmlFor="image-upload" className="block text-sm font-medium text-gray-700 mb-1">Subir imagen del permiso</label>
          <input
            type="file"
            id="image-upload"
            accept="image/*"
            onChange={handleImageChange}
            className="w-full p-2 border border-gray-300 rounded-md bg-gray-50 hover:bg-gray-100 transition duration-150 ease-in-out cursor-pointer"
            required
          />
        </div>

        <button
          type="submit"
          className="w-full bg-blue-600 text-white px-5 py-2.5 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 transition duration-150 ease-in-out text-lg font-medium shadow-md"
        >
          Enviar permiso
        </button>
      </form>
    </div>
  </>);
};

export default PermitFileUploader;
