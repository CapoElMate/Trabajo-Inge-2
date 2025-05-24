export default function ImageUploader({ onChange }) {
  return (
    <div>
    <label className="block mb-1 font-medium">Cargar imágenes </label>
    <input
      type="file"
      accept="image/*"
      multiple
      onChange={onChange}
      className="block w-full"
    />  
  </div>
  );
}
