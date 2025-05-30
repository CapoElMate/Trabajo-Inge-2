import { useState, useEffect } from "react";
import TextInput from "../components/TextInput";
import SelectInput from "../components/SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "../components/FormButtons";
import ImageUploader from "./ImageUploader";
import ImagePreviewList from "./ImagePreviwList"; 
import Header from "./Header";

 const fileToBase64 = (file) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);

        reader.onload = () => {
            resolve(reader.result);
        };

        reader.onerror = (error) => {
            reject(error);
        };
    });
  }

export default function PublicacionForm ({ initialData = {}, onSubmit={},onCancel={}, modo = "Crear" }) {
  const [tags, setTags] = useState(initialData.tags?.map(t => t.tag) || []);
  const opcionesDeTags = ["Urgente", "Nuevo", "Oferta"];

  const [imagenes, setImagenes] = useState(initialData.imagenes?.map(t => t.img) || []);

  const [maquinaria, setMaquinaria] = useState(initialData.maquinaria || null);
  const [politica, setPolitica] = useState(initialData.politica || "");

  const [ubicacion, setUbicacion] = useState(initialData.ubicacion || "");
  const [precio, setPrecio] = useState(initialData.precio|| "");
  const [titulo, setTitulo] = useState(initialData.titulo|| "");
  const [descripcion, setDescripcion] = useState(initialData.descripcion|| "");
  const [opcionesMaquinaria, setOpcionesMaquinaria] = useState([]);
  const [maquinarias, setMaquinarias ] = useState([]);
    useEffect(() => {
        fetch("http://localhost:3001/maquinas")
        .then((res) => res.json())
        .then((data) => {setOpcionesMaquinaria(data.map(m => ({
                label: `${m.marca} ${m.modelo}`,
                value: m.id
            })));
          setMaquinarias(data);
          }
          ).then((data)=> console.log(opcionesMaquinaria)
            )}, []); 

  const handleImagenesChange = async (e) => { 
    const files = Array.from(e.target.files);
    console.log("Archivos seleccionados:", files);

    const base64Array = [];
    for (const file of e.target.files) {
        const base64 = await fileToBase64(file);
        base64Array.push({"name":file.name,"src":base64});
    }
     
    

    setImagenes((prev) => [...prev, ...base64Array]); 
  };
  const handleEliminarImagenes = (index) => {
  setImagenes((prev) => prev.filter((_, i) => i !== index));
  };

 

    const handleSubmit = (e) => {
        e.preventDefault();
        //const data = {//como deberia ir al bd real
        //};
        const data2 = {
        ...(modo === "Editar" && { id: initialData?.id || "pub106" }),
        titulo,
        descripcion,
        ubicacion,
        precio,
        maquinaria,  
        "politica" : politica,
        "tags" : tags.map((t)=>({"tag":t })),
        "imagenes" : imagenes.map((img)=>({"img":img }))
        };
        onSubmit(data2);
    };

  
  return (
    < > 
     <div className="detalle-contenedor">
    <form  onSubmit={handleSubmit}className="max-w-md mx-auto p-4 border rounded space-y-4">
      <h2 className="text-xl font-semibold text-center">{modo} Publicación</h2>
     {maquinaria && modo == "Editar"? 
      (<>
      <h2 className="text-xl mb-2"> {maquinaria.marca} {maquinaria.modelo}</h2>
      <p>Año: {maquinaria.anioFabricacion}</p>
      <p>Tipo: {maquinaria.tipo}</p>
      <p>Permisos: {maquinaria.permisosEspeciales?.map(p => p.permiso.permiso).join(", ")}</p>
      </>
      )
      :(
      <SelectInput optionCompleja={false} label="Maquinaria " options={opcionesMaquinaria}
        value={maquinaria?.id}
        onChange={(e) => {
            const selectedId = e.target.value;
            const seleccionada = maquinarias.find(m => m.id === selectedId);
            setMaquinaria(seleccionada);
            console.log("Maquinaria elegida ", seleccionada);
            
        }} 
      required/>)}
      
      <TextInput label="Título" onChange={(e) => setTitulo(e.target.value)} required value={titulo}/>
      <TextInput type="text" label="Descripcion" value={descripcion}
       onChange={(e) => setDescripcion(e.target.value)} required/>

      <SelectInput label="Politica de Cancelacion " options={["Politica por Tardanza", "Politica por daños"]}
       onChange={(e) => setPolitica(e.target.value)} value={politica}
       required/>
      
      <SelectInput label="Ubicacion " 
        options={["Ubicacion A", "Ubicadion B"]} 
        onChange={(e) => setUbicacion(e.target.value)} value={ubicacion}
        required/>
      <TextInput label="Precio por dia " type="number"  value={precio}
      onChange={(e) => setPrecio(e.target.value)} required/>
      
      <TagSelector tags={tags} setTags={setTags} opciones={opcionesDeTags} />
      
      <ImageUploader onChange={handleImagenesChange} required/>
      <ImagePreviewList imagenes={imagenes} handleEliminar= {handleEliminarImagenes} />
      <FormButtons  modo={modo}  onCancel={onCancel} />
     </form>
     </div>
    </>
  );
}
