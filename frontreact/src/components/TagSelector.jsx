import { useState } from "react";
import './HomePage.css';
export default function TagSelector({ tags, setTags, opciones, modo="Tags" }) {
  const [selected, setSelected] = useState("");

  const handleAdd = () => {
    if (selected && !tags.includes(selected)) {
      setTags([...tags, selected]);
      setSelected("");
    }
  };

  const handleRemove = (tag) => {
    setTags(tags.filter(t => t !== tag));
  };

  return (
    <div>
      <label className="block mb-1 font-medium">{modo}</label>
      <div className="flex gap-2">
        <select
          value={selected}
          onChange={(e) => setSelected(e.target.value)}
          className="border p-1 rounded"
        >
          <option value="">Seleccionar tag</option>
          {opciones.map((opt) => (
            <option key={opt} value={opt}>{opt}</option>
          ))}
        </select>
        <button type="button" onClick={handleAdd}>+</button>
      </div>

      <div className="flex gap-2 mt-2 flex-wrap">
        {tags.map(tag => (
          <span key={tag} className="bg-gray-300 px-2 py-1 rounded-full text-sm flex items-center gap-1">
            {tag}
            <button onClick={() => handleRemove(tag)}>x</button>
          </span>
        ))}
      </div>
    </div>
  );
} 
