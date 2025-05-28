// components/MaquinariaForm.jsx
import { useState, useEffect, } from "react";
import TextInput from "./TextInput";
import SelectInput from "./SelectInput";
import TagSelector from "./TagSelector";
import FormButtons from "./FormButtons";


export default function MaquinariaForm({ initialData = {}, onSubmit={},onCancel={}, modo = "Crear" }) {
  const [marca, setMarca] = useState(initialData.marcaName || "");
  const [modelo, setModelo] = useState(initialData.marca?.modelo || "");
  const [anio, setAnio] = useState(initialData.anioFabricacion || "");
  const [tipo, setTipo] = useState(initialData.tipo || "");
  const [permisos, setPermisos] = useState([]);
  const [tags, setTags] = useState(initialData.tagsMaquina?.map(t => t.tag) || []);
  const opcionesDeTags = ["Pesado", "Ligero"];
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
    onSubmit(data);
  };
   const anios = Array.from({ length: 2025 - 1900 + 1 }, (_, i) => (1900 + i).toString());
 
  return (
    <div className="detalle-contenedor">
    <form  className="max-w-md mx-auto p-4 border rounded space-y-4">
      <h2 className="text-xl font-semibold text-center">{modo} Maquinaria</h2>

      <SelectInput
        label="Marca"
        options={Object.keys(modelosPorMarca)}
        value={marca}
        onChange={(e) => handleMarcaChange(e.target.value)}
      />
      <SelectInput
        label="Modelo"
        options={modelosPorMarca[marca] || []} //diferencia
        value={modelo}
        onChange={(e) => setModelo(e.target.value)}
      />
      
      <SelectInput label="Año de Fabricación" options={anios} value={anio} 
              onChange={(e) => setAnio(e.target.value)} />
      <SelectInput label="Tipo Maquinaria" options={["Excavadora", "Grúa"]} value={tipo} onChange={(e) => setTipo(e.target.value)} />
      //dif no tenia
      <SelectInput label="Permisos " options={["Permiso A", "Permiso B"]} onChange={(e) => setPermisos(e.target.value)} />        
      <SelectInput label="Permisos adicionales " options={["Extra 1", "Extra 2"]} />
      
      <TagSelector tags={tags} setTags={setTags} opciones={opcionesDeTags} />
      <FormButtons  onSubmit={onSubmit} onCancel={onCancel}  />
    </form>
    </div>
  );
}
