// components/MaquinariaForm.jsx
import { useState, useEffect, } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";


export default function MaquinariaForm({ initialData = {}, onSubmit={},onCancel={}, modo = "Crear" }) {
  const [marca, setMarca] = useState(initialData.marca|| "");/*marcaName */ 
  const [modelo, setModelo] = useState(initialData.modelos || "");/*marca?.modelo */
  const [anio, setAnio] = useState(initialData.anioFabricacion || "");
  const [tipo, setTipo] = useState(initialData.tipo || "");
  const [permisos, setPermisos] = useState(initialData.permisosEspeciales?.map(t => t.permiso) ||[]);
  const [tags, setTags] = useState(initialData.tagsMaquina?.map(t => t.tag.tag) || []);/*los seleccionados*/
  const [errors, setErrors] = useState({});
  //const [modelos, setModelos] = useState([]);
  const [tagsDisponibles, setTagsDisponibles] = useState([]);
  const [permisosDisponibles, setPermisosDisponibles] = useState([]);
  const [tiposMaquinaDisponibles, setTiposMaquinaDisponibles] = useState([]);

  useEffect(() => {
  //getModelos().then(setModelos);
  getTags().then(setTagsDisponibles);
  getPermisosEspeciales().then(setPermisosDisponibles);
  getTiposMaquina().then(setTiposMaquinaDisponibles);
  }, []);

  const getTags = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/TagsMaquina/all");
      if (!response.ok) {
        throw new Error("Error al obtener los tags");
      }
      const data = await response.json();
      return data.map(tag => tag.tag);
    } catch (error) {
      console.error("Error fetching tags:", error);
      return [];
    }
  }

  const getPermisosEspeciales = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/PermisosEspeciales/all");
      if (!response.ok) {
        throw new Error("Error al obtener los permisos especiales");
      }
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching permisos especiales:", error);
      return [];
    }
  }

  const getTiposMaquina = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/TiposMaquina/all");
      if (!response.ok) {
        throw new Error("Error al obtener los tipos de maquinaria");
      }
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching tipos de maquinaria:", error);
      return [];
    }
  };

  const getModelos = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Modelos/all");
      if (!response.ok) {
        throw new Error("Error al obtener los modelos");
      }
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error fetching modelos:", error);
      return [];
    }
  }

  // const handleMarcaChange = (value) => {
  //   setMarca(value);
  //   setModelo("");
  // };
  
  const handleSubmit = (e) => {
    e.preventDefault();
    const data = {
      anioFabricacion: parseInt(anio),
      marca: marca,
      modelo: modelo,
      tipo: tipo,
      permisosEspeciales: permisos.map(p => ({ permiso: p })),
      tagsMaquina: tags.map(t => ({ tag: t }))
    };
    // const data2 = {
    //   ...(modo === "Editar" && { id: initialData.id || "maq106" }),
    //   marca, 
    //   modelo,
    //   anio,
    //   tipo,
    //   "permisos":permisos.map((p)=>({"permiso":p })),
    //   "tags":tags.map((t)=>({"tag":t })),
    // };
    onSubmit(data, setErrors);
  };
  const anios = Array.from({ length: 2025 - 1900 + 1 }, (_, i) => (1900 + i).toString());
 
  return (
    <div className="detalle-contenedor">
    <form  onSubmit={handleSubmit} className="max-w-md mx-auto p-4 border rounded space-y-4">
      <h2 className="text-xl font-semibold text-center">{modo} Maquinaria</h2>
      <TextInput
        label="Modelo"
        class="w-full border p-1 rounded"
        type="text"
        name="modelo"
        placeholder="Ingrese el modelo"
        onChange={(e) => {
          setModelo(e.target.value) 
          setErrors({ ...errors, modelo: "" , maquinaria: ""}); // Clear error on change}
          }
        }
        value={modelo}
      />
      {errors.modelo && (
      <p className="error-message">{errors.modelo}</p>
      )}

      <TextInput
        label="Marca"
        class="w-full border p-1 rounded"
        type="text"
        name="marca"
        placeholder="Ingrese la marca"
        onChange={(e) => {
          setMarca(e.target.value)
          setErrors({ ...errors, marca: "", maquinaria: "" }) // Clear error on change
          }
        }
        value={marca}
      />
      {/* <SelectInput
        label="Marca"
        options={modelos.map(m => m.marca.marca || [])}
        value={marca}
        onChange={(e) => setMarca(e.target.value)}
        required
      />
      <SelectInput
        label="Modelo"
        options={modelos.map(m => m.modelo) || []} //diferencia
        value={modelo}
        onChange={(e) => setModelo(e.target.value)}
        required
      /> */}
      
      <SelectInput label="Año de Fabricación" options={anios} value={anio} 
              onChange={(e) => {
                setAnio(e.target.value)
                setErrors({ ...errors, anio: "", maquinaria: "" }) // Clear error on change
                }
              } 
              required/>
      <SelectInput label="Tipo Maquinaria" options={tiposMaquinaDisponibles.map(t => t.tipo)} value={tipo}
       onChange={(e) => {
          setTipo(e.target.value)
          setErrors({ ...errors, tipo: "", maquinaria: "" }) // Clear error on change
          }
       } 
       required/>
      <div class="dropdowns">
        <TagSelector modo="Permisos especiales" tags={permisos} setTags={setPermisos} opciones={permisosDisponibles.map(t => t.permiso)} required />
        <TagSelector  tags={tags} setTags={setTags} opciones={tagsDisponibles.map(t => t.tag)} />
      </div>
      <FormButtons  modo={modo} onCancel={onCancel} />
      {errors.maquinaria && (
      <p className="error-message">{errors.maquinaria}</p>
      )}
    </form>
    </div>
  );
}
