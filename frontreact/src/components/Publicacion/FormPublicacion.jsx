import { useState, useEffect } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import ImageUploader from "../ImageUploader";
import "../FormStructure.css";
import "./FormPublicacion.css";

export default function PublicacionForm({
  initialData = {},
  onSubmit = {},
  onCancel = {},
  modo = "Crear",
}) {
  const [tags, setTags] = useState([]);
  //const [imagenes, setImagenes] = useState([]);
  const [maquinaria, setMaquinaria] = useState(null);
  const [politica, setPolitica] = useState("");
  const [ubicacion, setUbicacion] = useState("");
  const [precio, setPrecio] = useState("");
  const [titulo, setTitulo] = useState("");
  const [descripcion, setDescripcion] = useState("");

  const [opcionesMaquinaria, setOpcionesMaquinaria] = useState([]);
  const [maquinarias, setMaquinarias] = useState([]);
  const [opcionesDeTags, setOpcionesDeTags] = useState([]);
  const [ubicaciones, setUbicaciones] = useState([]);
  const [opcionesPoliticas, setOpcionesPoliticas] = useState([]);
  const [errores, setErrores] = useState({});
  const [mensajeExito, setMensajeExito] = useState("");

  // 🔄 Fetch data
  useEffect(() => {
    const fetchData = async () => {
      try {
        const [resMaq, resTags, resUbic, resPol] = await Promise.all([
          fetch("http://localhost:5000/api/Maquinas/all"),
          fetch("http://localhost:5000/api/TagsPublicacion/all"),
          fetch("http://localhost:5000/api/Ubicacion/all"),
          fetch("http://localhost:5000/api/PoliticaDeCancelacion/all"),
        ]);

        const [maq, tags, ubic, politicas] = await Promise.all([
          resMaq.json(),
          resTags.json(),
          resUbic.json(),
          resPol.json(),
        ]);

        setOpcionesMaquinaria(
          maq
            .filter((m) => m.status !== "Eliminada")
            .map((m) => ({
              label: `${m.modelo.marca.marca} ${m.modelo.modelo}`,
              value: m.idMaquina,
            }))
        );
        setMaquinarias(maq);
        setOpcionesDeTags(tags.map((t) => t.tag));
        setUbicaciones(ubic.map((u) => u.ubicacionName));
        setOpcionesPoliticas(politicas);
      } catch (error) {
        console.error("Error al cargar datos:", error);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    if (initialData.imagenes?.length) {
      setImagenesOriginales(initialData.imagenes); // info completa
    }
  }, [initialData]);

  // 🔄 Modo edición
  useEffect(() => {
    if (initialData && modo === "Editar") {
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
      //setImagenes(initialData.imagenes || []);
    }
  }, [initialData, modo]);

  const [imagenesActuales, setImagenesActuales] = useState([]);
  const [imagenesOriginales, setImagenesOriginales] = useState([]);

  const handleImageChange = (imagenes) => {
    if (!imagenes || imagenes.length === 0) {
      setErrores((prev) => ({
        ...prev,
        imagenes: "Debes subir al menos una imagen.",
      }));
    } else {
      setErrores((prev) => ({
        ...prev,
        imagenes: "", // Limpia el error aquí
      }));
    }
    setImagenesActuales(imagenes);
  };

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
    const regexPrecio = /^-?\d+(?:[.,]\d+)?$/;

    const precioStr = String(precio);
    const precioFormateado = precioStr.includes(",")
      ? precioStr.replace(",", ".")
      : precioStr;

    const precioNumerico = parseFloat(precioFormateado);

    if (!precio || isNaN(precioNumerico)) {
      nuevosErrores.precio = "El precio debe ser un número válido.";
    } else if (!regexPrecio.test(precio)) {
      nuevosErrores.precio = "Solo se permiten números y decimales.";
    } else if (precioNumerico <= 0) {
      nuevosErrores.precio = "El precio debe ser mayor a 0.";
    }

    // Validación de imágenes
    if (!imagenesActuales || imagenesActuales.length === 0) {
      nuevosErrores.imagenes = "Debes subir al menos una imagen.";
    }

    if (Object.keys(nuevosErrores).length > 0) {
      setErrores(nuevosErrores);
      return;
    }

    setErrores({}); // limpiar errores

    // Datos de la publicación
    const publicacionData = {
      ...(modo === "Editar" && { idPublicacion: initialData?.idPublicacion }),
      status: "Disponible",
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

    // Separar imágenes originales mantenidas, eliminadas y nuevas
    const originalesEnviadas = imagenesActuales
      .filter((img) => img.original)
      .map((img) => img.original);

    const imagenesEliminadas = imagenesOriginales.filter(
      (orig) =>
        !originalesEnviadas.some((img) => img.idArchivo === orig.idArchivo)
    );

    const imagenesNuevas = imagenesActuales
      .filter((img) => !img.original)
      .map((img) => img.file);

    setMensajeExito(
      modo === "Crear"
        ? "La publicación de la maquinaria se creó correctamente"
        : "La modificación de la publicación se realizó exitosamente."
    );

    onSubmit(publicacionData, {
      imagenesOriginales: originalesEnviadas,
      imagenesNuevas,
      imagenesEliminadas,
    });
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

          <ImageUploader
            onChange={handleImageChange}
            initialImages={initialData.imagenes}
          />
          {errores.imagenes && (
            <p className="text-red-500 text-sm error-message">
              {errores.imagenes}
            </p>
          )}

          {!mensajeExito ? (
            <FormButtons
              modo={modo === "Editar" ? "Confirmar modificacion" : modo}
              onCancel={onCancel}
            />
          ) : (
            <div className="success-message">{mensajeExito}</div>
          )}
        </form>
      </div>
    </>
  );
}
