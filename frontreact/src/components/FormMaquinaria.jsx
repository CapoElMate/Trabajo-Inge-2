// components/MaquinariaForm.jsx
import { useState, useEffect, } from "react";
import TextInput from "./TextInput";
import SelectInput from "./SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "./FormButtons";


export default function MaquinariaForm({ initialData = {}, onSubmit={},onCancel={}, modo = "Crear" }) {
  const [marca, setMarca] = useState(initialData.marca|| "");/*marcaName */ 
  const [modelo, setModelo] = useState(initialData.modelos || "");/*marca?.modelo */
  const [anio, setAnio] = useState(initialData.anioFabricacion || "");
  const [tipo, setTipo] = useState(initialData.tipo || "");
  const [permisos, setPermisos] = useState(initialData.permisosEspeciales?.map(t => t.permiso.permiso) ||[]);
  const [tags, setTags] = useState(initialData.tagsMaquina?.map(t => t.tag.tag) || []);/*los seleccionados*/
  const opcionesDeTags = ["Pesado", "Ligero"]; /*opciones ahre */
  const opTagsPermisos = ["Permiso A", "Permiso B"]
  const modelosPorMarca = {
    "Marca A": ["Modelo A1", "Modelo A2"],
    "Marca B": ["Modelo B1", "Modelo B2"],
    "Caterpillar": ["320", "330"],
    "Komatsu": ["PC200", "PC210"]
  };

  const handleMarcaChange = (value) => {
    setMarca(value);
    setModelo("");
  };
  
  const handleSubmit = (e) => {
    e.preventDefault();
    const data = {
      anioFabricacion: parseInt(anio),
      marcaName: marca,
      marca: {
        marcaName: marca,
        modelos: [{ modeloName: modelo, marcaName: marca }]
      },
      tipo,
      permisosEspeciales: permisos.map(p => ({ permiso: p })),
      tagsMaquina: tags.map(t => ({ tag: t }))
    };
    const data2 = {
      ...(modo === "Editar" && { id: initialData.id || "maq106" }),
      marca, 
      modelo,
      anio,
      tipo,
      "permisos":permisos.map((p)=>({"permiso":p })),
      "tags":tags.map((t)=>({"tag":t })),
    };
    onSubmit(data2);
  };
   const anios = Array.from({ length: 2025 - 1900 + 1 }, (_, i) => (1900 + i).toString());
 
  return (
    <div className="detalle-contenedor">
    <form  onSubmit={handleSubmit} className="max-w-md mx-auto p-4 border rounded space-y-4">
      <h2 className="text-xl font-semibold text-center">{modo} Maquinaria</h2>
       
      <SelectInput
        label="Marca"
        options={Object.keys(modelosPorMarca)}
        value={marca}
        onChange={(e) => handleMarcaChange(e.target.value)}
        required
      />
      <SelectInput
        label="Modelo"
        options={modelosPorMarca[marca] || []} //diferencia
        value={modelo}
        onChange={(e) => setModelo(e.target.value)}
        required
      />
      
      <SelectInput label="Año de Fabricación" options={anios} value={anio} 
              onChange={(e) => setAnio(e.target.value)} 
              required/>
      <SelectInput label="Tipo Maquinaria" options={["Excavadora", "Grúa"]} value={tipo}
       onChange={(e) => setTipo(e.target.value)} 
       required/>
   
      <TagSelector modo="Permisos Especiales" tags={permisos} setTags={setPermisos} opciones={opTagsPermisos} required />
      <TagSelector  tags={tags} setTags={setTags} opciones={opcionesDeTags} />
      <FormButtons  modo={modo} onCancel={onCancel} />
    </form>
    </div>
  );
}
