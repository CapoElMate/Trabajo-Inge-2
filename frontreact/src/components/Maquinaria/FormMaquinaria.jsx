import { useState, useEffect } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import "./FormMaquinaria.css";

export default function MaquinariaForm({
  initialData = {},
  onSubmit,
  onCancel,
  modo = "Crear",
}) {
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
  const [modelos, setModelos] = useState([]);
  const [marcas, setMarcas] = useState([]);
  const [mensajeExito, setMensajeExito] = useState("");

  useEffect(() => {
    if (
      initialData &&
      Object.keys(initialData).length > 0 &&
      modo === "Editar"
    ) {
      setMarca(initialData.modelo?.marca?.marca || "");
      setModelo(initialData.modelo?.modelo || "");
      setAnio(initialData.anioFabricacion?.toString() || "");
      setTipo(initialData.tipoMaquina?.tipo || "");
      setPermisos(initialData.permisosEspeciales?.map((p) => p.permiso) || []);
      setTags(initialData.tagsMaquina?.map((t) => t.tag) || []);
    }
  }, [initialData, modo]);

  useEffect(() => {
    getMarcas().then(setMarcas);
    getTags().then(setTagsDisponibles);
    getPermisosEspeciales().then(setPermisosDisponibles);
    getTiposMaquina().then(setTiposMaquinaDisponibles);
  }, []);

  useEffect(() => {
    if (marca) {
      getModelos(marca).then(setModelos);
    } else {
      setModelos([]);
    }
  }, [marca]);

  const getModelos = async (marca = "") => {
    if (!marca) return [];
    try {
      const response = await fetch("http://localhost:5000/api/Modelos/all");
      if (!response.ok) throw new Error("Error al obtener los modelos");
      const data = await response.json();
      return data.filter((m) => m.marca.marca === marca);
    } catch (error) {
      console.error("Error fetching modelos:", error);
      return [];
    }
  };

  const getMarcas = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Marcas/all");
      if (!response.ok) throw new Error("Error al obtener las marcas");
      const data = await response.json();
      return data.map((m) => m.marca);
    } catch (error) {
      console.error("Error fetching marcas:", error);
      return [];
    }
  };

  const getTags = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/TagsMaquina/all");
      if (!response.ok) throw new Error("Error al obtener los tags");
      const data = await response.json();
      return data.map((t) => t.tag);
    } catch (error) {
      console.error("Error fetching tags:", error);
      return [];
    }
  };

  const getPermisosEspeciales = async () => {
    try {
      const response = await fetch(
        "http://localhost:5000/api/PermisosEspeciales/all"
      );
      if (!response.ok)
        throw new Error("Error al obtener los permisos especiales");
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching permisos especiales:", error);
      return [];
    }
  };

  const getTiposMaquina = async () => {
    try {
      const response = await fetch(
        "http://localhost:5000/api/TiposMaquina/all"
      );
      if (!response.ok)
        throw new Error("Error al obtener los tipos de maquinaria");
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching tipos de maquinaria:", error);
      return [];
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const newErrors = {};
    if (marca === "") newErrors.marca = "Seleccione una marca.";
    if (modelo === "") newErrors.modelo = "Seleccione un modelo.";
    if (anio === "") newErrors.anio = "Seleccione un año de fabricacion.";
    if (tipo === "") newErrors.tipo = "Seleccione el tipo de maquinaria.";

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    const data = {
      anioFabricacion: parseInt(anio),
      marca,
      modelo,
      tipo,
      permisosEspeciales: permisos.map((p) => ({ permiso: p })),
      tagsMaquina: tags.map((t) => ({ tag: t })),
    };

    setMensajeExito(
      modo === "Crear"
        ? "La maquinaria fue creada correctamente."
        : "La máquina se ha modificado satisfactoriamente."
    );
    onSubmit(data, setErrors);
  };

  const anios = Array.from({ length: 2025 - 1900 + 1 }, (_, i) =>
    (1900 + i).toString()
  );

  return (
    <div className="detalle-contenedor">
      <form
        onSubmit={handleSubmit}
        className="max-w-md mx-auto p-4 border rounded space-y-4"
      >
        <h2 className="text-xl font-semibold text-center">{modo} Maquinaria</h2>

        <SelectInput
          label="Marca"
          options={marcas}
          value={marca}
          onChange={(e) => {
            setMarca(e.target.value);
            setModelo("");
            setErrors({ ...errors, marca: "", maquinaria: "" });
          }}
          className={errors.marca ? "input-error" : ""}
          isRequired={true}
        />
        {errors.marca && <p className="error-message">{errors.marca}</p>}

        <SelectInput
          label="Modelo"
          options={modelos.map((m) => m.modelo)}
          value={modelo}
          onChange={(e) => {
            setModelo(e.target.value);
            setErrors({ ...errors, modelo: "", maquinaria: "" });
          }}
          disabled={!marca}
          className={errors.modelo ? "input-error" : ""}
          isRequired={true}
        />
        {errors.modelo && <p className="error-message">{errors.modelo}</p>}

        <SelectInput
          label="Tipo Maquinaria"
          options={tiposMaquinaDisponibles.map((t) => t.tipo)}
          value={tipo}
          onChange={(e) => {
            setTipo(e.target.value);
            setErrors({ ...errors, tipo: "", maquinaria: "" });
          }}
          className={errors.tipo ? "input-error" : ""}
          isRequired={true}
        />
        {errors.tipo && <p className="error-message">{errors.tipo}</p>}

        <SelectInput
          label="Año de Fabricación"
          options={anios}
          value={anio}
          onChange={(e) => {
            setAnio(e.target.value);
            setErrors({ ...errors, anio: "", maquinaria: "" });
          }}
          className={errors.anio ? "input-error" : ""}
          isRequired={true}
        />
        {errors.anio && <p className="error-message">{errors.anio}</p>}

        <div className="dropdowns">
          <TagSelector
            modo="Permisos especiales"
            tags={permisos}
            setTags={setPermisos}
            opciones={permisosDisponibles.map((t) => t.permiso)}
          />
          <TagSelector
            tags={tags}
            setTags={setTags}
            opciones={tagsDisponibles}
          />
        </div>

        {!mensajeExito ? (
          <FormButtons
            modo={modo === "Editar" ? "Confirmar modificacion" : modo}
            onCancel={onCancel}
          />
        ) : (
          <div className="success-message">{mensajeExito}</div>
        )}

        {errors.maquinaria && (
          <p className="error-message">{errors.maquinaria}</p>
        )}
      </form>
    </div>
  );
}
