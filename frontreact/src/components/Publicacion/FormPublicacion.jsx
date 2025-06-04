import { useState, useEffect } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import ImageUploader from "../ImageUploader";
import ImagePreviewList from "../ImagePreviwList";
import Header from "../Header";
import "../FormStructure.css";
import "./FormPublicacion.css";

export default function PublicacionForm({
  initialData = {},
  onSubmit = {},
  onCancel = {},
  modo = "Crear",
}) {
  const [tags, setTags] = useState(initialData.tags?.map((t) => t.tag) || []);
  const [imagenes, setImagenes] = useState(
    initialData.imagenes?.map((t) => t.img) || []
  );
  const [maquinaria, setMaquinaria] = useState(initialData.maquinaria || null);
  const [politica, setPolitica] = useState(initialData.politica || "");
  const [ubicacion, setUbicacion] = useState(initialData.ubicacion || "");
  const [precio, setPrecio] = useState(initialData.precio || "");
  const [titulo, setTitulo] = useState(initialData.titulo || "");
  const [descripcion, setDescripcion] = useState(initialData.descripcion || "");

  const [opcionesMaquinaria, setOpcionesMaquinaria] = useState([]);
  const [maquinarias, setMaquinarias] = useState([]);
  const [opcionesDeTags, setOpcionesDeTags] = useState([]);
  const [ubicaciones, setUbicaciones] = useState([]);
  const [opcionesPoliticas, setOpcionesPoliticas] = useState([]);
  const [errores, setErrores] = useState({});

  const getMaquinas = async () => {
    try {
      const res = await fetch("http://localhost:5000/api/Maquinas/all");
      const data = await res.json();
      setOpcionesMaquinaria(
        data.map((m) => ({
          label: `${m.modelo.marca.marca} ${m.modelo.modelo}`,
          value: m.idMaquina,
        }))
      );
      setMaquinarias(data);
    } catch (error) {
      console.error("Error al obtener máquinas:", error);
    }
  };

  const getTags = async () => {
    try {
      const res = await fetch("http://localhost:5000/api/TagsPublicacion/all");
      const data = await res.json();
      setOpcionesDeTags(data.map((t) => t.tag));
    } catch (error) {
      console.error("Error al obtener tags:", error);
    }
  };

  const getUbicaciones = async () => {
    try {
      const res = await fetch("http://localhost:5000/api/Ubicacion/all");
      const data = await res.json();
      setUbicaciones(data.map((u) => u.ubicacionName));
    } catch (error) {
      console.error("Error al obtener ubicaciones:", error);
    }
  };

  const getPoliticas = async () => {
    try {
      const res = await fetch(
        "http://localhost:5000/api/PoliticaDeCancelacion/all"
      );
      const data = await res.json();
      setOpcionesPoliticas(data);
    } catch (error) {
      console.error("Error al obtener ubicaciones:", error);
    }
  };

  useEffect(() => {
    const cargarDatos = async () => {
      await Promise.all([
        getMaquinas(),
        getTags(),
        getUbicaciones(),
        getPoliticas(),
      ]);
    };
    cargarDatos();
  }, []);

  useEffect(() => {
    if (
      initialData &&
      Object.keys(initialData).length > 0 &&
      modo === "Editar"
    ) {
      setMaquinaria({
        label: `${initialData.maquina.modelo.marca.marca} ${initialData.maquina.modelo.modelo}`,
        value: initialData.maquina.idMaquina,
      });
      setPolitica(initialData.politicaDeCancelacion.politica);
      setUbicacion(initialData.ubicacion.ubicacionName);
      setDescripcion(initialData.descripcion);
      setTitulo(initialData.titulo);
      setPrecio(initialData.precioPorDia);
      setTags(initialData.tagsPublicacion.map((t) => t.tag));
    }
  }, [initialData, modo]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const nuevosErrores = {};

    // Validaciones de campos de texto
    if (!titulo.trim()) {
      nuevosErrores.titulo = "El título es obligatorio.";
    } else if (titulo.trim().length < 5) {
      nuevosErrores.titulo = "El título debe tener al menos 5 caracteres.";
    }

    if (!descripcion.trim()) {
      nuevosErrores.descripcion = "La descripción es obligatoria.";
    } else if (descripcion.trim().length < 10) {
      nuevosErrores.descripcion =
        "La descripción debe tener al menos 10 caracteres.";
    }

    // Selects
    if (!maquinaria)
      nuevosErrores.maquinaria = "Debes seleccionar una maquinaria.";
    if (!politica) nuevosErrores.politica = "Debes seleccionar una política.";
    if (!ubicacion) nuevosErrores.ubicacion = "La ubicación es obligatoria.";

    // Precio
    if (!precio || isNaN(parseFloat(precio))) {
      nuevosErrores.precio = "El precio debe ser un número válido.";
    } else if (parseFloat(precio) <= 0) {
      nuevosErrores.precio = "El precio debe ser mayor a 0.";
    }

    // Validación de imágenes
    if (!imagenes || imagenes.length === 0) {
      nuevosErrores.imagenes = "Debes subir al menos una imagen.";
    } else {
      const extensionesValidas = [".png", ".jpg", ".jpeg"];
      const imagenesInvalidas = imagenes.filter((img) => {
        const ext = img.name?.toLowerCase().split(".").pop();
        return !extensionesValidas.includes(`.${ext}`);
      });

      if (imagenesInvalidas.length > 0) {
        nuevosErrores.imagenes =
          "Solo se permiten imágenes .png, .jpg o .jpeg.";
      }
    }

    if (Object.keys(nuevosErrores).length > 0) {
      setErrores(nuevosErrores);
      return;
    }

    setErrores({}); // limpiar errores

    const publicacionData = {
      ...(modo === "Editar" && { idPublicacion: initialData?.idPublicacion }),
      status: "Activa",
      titulo,
      precioPorDia: parseFloat(precio),
      descripcion,
      maquina: maquinarias.find((m) => m.idMaquina === maquinaria.value),
      tagsPublicacion: tags.map((t) => ({ tag: t })),
      politicaDeCancelacion: opcionesPoliticas.find(
        (p) => p.politica === politica
      ),
      ubicacion: {
        ubicacionName: ubicacion,
      },
    };

    onSubmit(publicacionData, imagenes);
  };

  return (
    <>
      <div className="detalle-contenedor">
        <form
          onSubmit={handleSubmit}
          className="max-w-md mx-auto p-4 border rounded space-y-4"
        >
          <h2 className="text-xl font-semibold text-center">
            {modo} Publicación
          </h2>

          <TextInput
            label="Título"
            onChange={(e) => setTitulo(e.target.value)}
            value={titulo}
          />
          {errores.titulo && (
            <p className="text-red-500 text-sm error-message">
              {errores.titulo}
            </p>
          )}

          <TextInput
            type="text"
            label="Descripcion"
            value={descripcion}
            onChange={(e) => setDescripcion(e.target.value)}
          />
          {errores.descripcion && (
            <p className="text-red-500 text-sm error-message">
              {errores.descripcion}
            </p>
          )}

          <SelectInput
            optionCompleja={false}
            label="Maquinaria"
            options={
              modo === "Editar"
                ? maquinaria
                  ? [maquinaria]
                  : []
                : opcionesMaquinaria
            }
            defaultOptionLabel="Seleccionar..."
            value={maquinaria?.value || ""}
            onChange={(e) => {
              const selectedId = e.target.value;
              const seleccionada = maquinarias.find(
                (m) => m.idMaquina === parseInt(selectedId)
              );
              if (seleccionada) {
                setMaquinaria({
                  value: seleccionada.idMaquina,
                  label: `${seleccionada.marca} ${seleccionada.modelo}`,
                });
                console.log("Maquinaria elegida", seleccionada);
              }
            }}
            disabled={modo === "Editar"}
          />
          {errores.maquinaria && (
            <p className="text-red-500 text-sm error-message">
              {errores.maquinaria}
            </p>
          )}

          <SelectInput
            label="Política de Cancelación"
            options={opcionesPoliticas.map((p) => p.politica)}
            onChange={(e) => setPolitica(e.target.value)}
            value={politica || ""}
          />
          {errores.politica && (
            <p className="text-red-500 text-sm error-message">
              {errores.politica}
            </p>
          )}

          <SelectInput
            label="Ubicacion "
            options={ubicaciones}
            onChange={(e) => setUbicacion(e.target.value)}
            value={ubicacion}
          />
          {errores.ubicacion && (
            <p className="text-red-500 text-sm error-message">
              {errores.ubicacion}
            </p>
          )}

          <TextInput
            label="Precio por dia "
            type="text"
            value={precio}
            onChange={(e) => setPrecio(e.target.value)}
          />
          {errores.precio && (
            <p className="text-red-500 text-sm error-message">
              {errores.precio}
            </p>
          )}

          <TagSelector
            tags={tags}
            setTags={setTags}
            opciones={opcionesDeTags}
          />

          <ImageUploader onChange={(files) => setImagenes(files)} />
          {errores.imagenes && (
            <p className="text-red-500 text-sm error-message">
              {errores.imagenes}
            </p>
          )}
          <FormButtons modo={modo} onCancel={onCancel} />
        </form>
      </div>
    </>
  );
}
