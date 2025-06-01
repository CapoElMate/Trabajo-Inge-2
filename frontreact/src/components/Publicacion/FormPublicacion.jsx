import { useState, useEffect } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import ImageUploader from "../ImageUploader";
import ImagePreviewList from "../ImagePreviwList";
import Header from "../Header";
import "../FormStructure.css";

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
    // Supuesto: se carga desde un array fijo o similar si no hay endpoint real
    setOpcionesPoliticas(["Política por Tardanza", "Política por Daños"]);
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

  const handleSubmit = async (e) => {
    e.preventDefault();

    const publicacionData = {
      ...(modo === "Editar" && { id: initialData?.id }),
      status: "Activa",
      precioPorDia: precio,
      descripcion,
      idMaquina: maquinaria,
      tagsPublicacion: tags.map((t) => ({ tag: t })),
      politicaDeCancelacion: {
        politica: politica.politica,
        descripcion: politica.descripcion,
      },
      ubicacion: {
        ubicacionName: ubicacion,
      },
    };

    try {
      // Paso 1: Enviar publicación
      const res = await fetch("http://localhost:5000/api/Publicaciones", {
        method: modo === "Editar" ? "PUT" : "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(publicacionData),
      });

      if (!res.ok) throw new Error("Error al crear publicación");

      const nuevaPublicacion = await res.json();
      const publicacionId =
        nuevaPublicacion.id || nuevaPublicacion.idPublicacion;

      // Paso 2: Subir imágenes
      for (let i = 0; i < imagenes.length; i++) {
        const formData = new FormData();
        formData.append("EntidadID", publicacionId);
        formData.append("TipoEntidad", 0);
        formData.append("Nombre", `Imagen ${i + 1}`);
        formData.append("Descripcion", `Imagen ${i + 1} de la publicación`);
        formData.append("Archivo", imagenes[i]);

        const imgRes = await fetch("http://localhost:5000/api/Archivo", {
          method: "POST",
          body: formData,
        });

        if (!imgRes.ok) {
          console.error(`Error al subir imagen ${i + 1}`);
        }
      }

      onSubmit();
    } catch (error) {
      console.error("Error al crear publicación con imágenes:", error);
    }
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
          {maquinaria && modo == "Editar" ? (
            <>
              <h2 className="text-xl mb-2">
                {" "}
                {maquinaria.marca} {maquinaria.modelo}
              </h2>
              <p>Año: {maquinaria.anioFabricacion}</p>
              <p>Tipo: {maquinaria.tipo}</p>
              <p>
                Permisos:{" "}
                {maquinaria.permisosEspeciales
                  ?.map((p) => p.permiso.permiso)
                  .join(", ")}
              </p>
            </>
          ) : (
            <SelectInput
              optionCompleja={false}
              label="Maquinaria "
              options={opcionesMaquinaria}
              value={maquinaria?.id}
              onChange={(e) => {
                const selectedId = e.target.value;
                const seleccionada = maquinarias.find(
                  (m) => m.id === selectedId
                );
                setMaquinaria(seleccionada);
                console.log("Maquinaria elegida ", seleccionada);
              }}
              required
            />
          )}

          <TextInput
            label="Título"
            onChange={(e) => setTitulo(e.target.value)}
            required
            value={titulo}
          />
          <TextInput
            type="text"
            label="Descripcion"
            value={descripcion}
            onChange={(e) => setDescripcion(e.target.value)}
            required
          />

          <SelectInput
            label="Politica de Cancelacion "
            options={opcionesPoliticas}
            onChange={(e) => setPolitica(e.target.value)}
            value={politica}
            required
          />

          <SelectInput
            label="Ubicacion "
            options={ubicaciones}
            onChange={(e) => setUbicacion(e.target.value)}
            value={ubicacion}
            required
          />
          <TextInput
            label="Precio por dia "
            type="number"
            value={precio}
            onChange={(e) => setPrecio(e.target.value)}
            required
          />

          <TagSelector
            tags={tags}
            setTags={setTags}
            opciones={opcionesDeTags}
          />

          <ImageUploader onChange={(files) => setImagenes(files)} />
          <FormButtons modo={modo} onCancel={onCancel} />
        </form>
      </div>
    </>
  );
}
