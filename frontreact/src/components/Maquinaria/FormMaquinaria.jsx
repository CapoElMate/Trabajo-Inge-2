import { useState, useEffect } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";

export default function MaquinariaForm({ initialData = {}, onSubmit, onCancel, modo = "Crear" }) {
  // Estados para el formulario
  const [marca, setMarca] = useState("");
  const [modelo, setModelo] = useState("");
  const [anio, setAnio] = useState("");
  const [tipo, setTipo] = useState("");
  const [permisos, setPermisos] = useState([]);
  const [tags, setTags] = useState([]);
  const [errors, setErrors] = useState({});
  const [tagsDisponibles, setTagsDisponibles] = useState([]);
  const [permisosDisponibles, setPermisosDisponibles] = useState([]);
  const [tiposMaquinaDisponibles, setTiposMaquinaDisponibles] = useState([]);

  // Cuando cambie initialData, cargar valores en los estados (editar)
useEffect(() => {
  if (initialData && Object.keys(initialData).length > 0 && modo === "Editar") {
    setMarca(initialData.modelo?.marca?.marca || "");
    setModelo(initialData.modelo?.modelo || "");
    setAnio(initialData.anioFabricacion ? initialData.anioFabricacion.toString() : "");
    setTipo(initialData.tipoMaquina?.tipo || "");
    setPermisos(initialData.permisosEspeciales?.map(p => p.permiso) || []);
    setTags(initialData.tagsMaquina?.map(t => t.tag) || []);

  }
}, [initialData, modo]);

useEffect(() => {
  getTags().then(setTagsDisponibles);
  getPermisosEspeciales().then(setPermisosDisponibles);
  getTiposMaquina().then(setTiposMaquinaDisponibles);
}, [])

  const getTags = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/TagsMaquina/all");
      if (!response.ok) throw new Error("Error al obtener los tags");
      const data = await response.json();
      return data.map((tag) => tag.tag);
    } catch (error) {
      console.error("Error fetching tags:", error);
      return [];
    }
  };

  const getPermisosEspeciales = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/PermisosEspeciales/all");
      if (!response.ok) throw new Error("Error al obtener los permisos especiales");
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching permisos especiales:", error);
      return [];
    }
  };

  const getTiposMaquina = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/TiposMaquina/all");
      if (!response.ok) throw new Error("Error al obtener los tipos de maquinaria");
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching tipos de maquinaria:", error);
      return [];
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const data = {
      anioFabricacion: parseInt(anio),
      marca,
      modelo,
      tipo,
      permisosEspeciales: permisos.map((p) => ({ permiso: p })),
      tagsMaquina: tags.map((t) => ({ tag: t })),
    };

    onSubmit(data, setErrors);
  };

  const anios = Array.from({ length: 2025 - 1900 + 1 }, (_, i) => (1900 + i).toString());

  return (
    <div className="detalle-contenedor">
      <form onSubmit={handleSubmit} className="max-w-md mx-auto p-4 border rounded space-y-4">
        <h2 className="text-xl font-semibold text-center">{modo} Maquinaria</h2>

        <TextInput
          label="Modelo"
          class="w-full border p-1 rounded"
          type="text"
          name="modelo"
          placeholder="Ingrese el modelo"
          onChange={(e) => {
            setModelo(e.target.value);
            setErrors({ ...errors, modelo: "", maquinaria: "" });
          }}
          value={modelo}
        />
        {errors.modelo && <p className="error-message">{errors.modelo}</p>}

        <TextInput
          label="Marca"
          class="w-full border p-1 rounded"
          type="text"
          name="marca"
          placeholder="Ingrese la marca"
          onChange={(e) => {
            setMarca(e.target.value);
            setErrors({ ...errors, marca: "", maquinaria: "" });
          }}
          value={marca}
        />

        <SelectInput
          label="Año de Fabricación"
          options={anios}
          value={anio}
          onChange={(e) => {
            setAnio(e.target.value);
            setErrors({ ...errors, anio: "", maquinaria: "" });
          }}
          required
        />

        <SelectInput
          label="Tipo Maquinaria"
          options={tiposMaquinaDisponibles.map((t) => t.tipo)}
          value={tipo}
          onChange={(e) => {
            setTipo(e.target.value);
            setErrors({ ...errors, tipo: "", maquinaria: "" });
          }}
          required
        />

        <div className="dropdowns">
          <TagSelector
            modo="Permisos especiales"
            tags={permisos}
            setTags={setPermisos}
            opciones={permisosDisponibles.map((t) => t.permiso)}
            required
          />
          <TagSelector tags={tags} setTags={setTags} opciones={tagsDisponibles} />
        </div>

        <FormButtons modo={modo} onCancel={onCancel} />

        {errors.maquinaria && <p className="error-message">{errors.maquinaria}</p>}
      </form>
    </div>
  );
}