import { useState } from "react";
import TextInput from "../components/TextInput";
import SelectInput from "../components/SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "../components/FormButtons";
import ImageUploader from "./ImageUploader";
import ImagePreviewList from "./ImagePreviwList"; 
import Header2 from "./Header2";

export default function CargarPublicacion() {
  const [tags, setTags] = useState([]);
  const opcionesDeTags = ["Urgente", "Nuevo", "Oferta"];
  const [imagenes, setImagenes] = useState([]);

  const handleImagenesChange = (e) => { 
    const files = Array.from(e.target.files);
    console.log("Archivos seleccionados:", files);
    setImagenes((prev) => [...prev, ...Array.from(e.target.files)]); // o podés agregarlas a las ya cargadas
  };
  const handleEliminar = (index) => {
  setImagenes((prev) => prev.filter((_, i) => i !== index));
  };
  return (
    < >
    <Header2/>
     <div className="detalle-contenedor">
    <form className="max-w-md mx-auto p-4 border rounded space-y-4">
      <h2 className="text-xl font-semibold text-center">Cargar Publicación</h2>

      <SelectInput label="Maquinaria " options={["Maquina A", "Maquina B"]} />
      <SelectInput label="Politica de Cancelacion " options={["Politica por Tardanza", "Politica por daños"]} />
      
      <TextInput label="Ubicacion " />
      <TextInput label="Precio por dia " type="number" />
      
      <TagSelector tags={tags} setTags={setTags} opciones={opcionesDeTags} />
      
      <ImageUploader onChange={handleImagenesChange} />
      <ImagePreviewList imagenes={imagenes} handleEliminar= {handleEliminar} />
      <FormButtons onCancel={() => console.log("Cancelado")} />
     </form>
     </div>
    </>
  );
}

