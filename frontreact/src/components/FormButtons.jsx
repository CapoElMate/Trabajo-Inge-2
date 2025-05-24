 function cargarPublicacion(){

 }

export default function FormButtons({ onSubmit,onCancel }) {
  return (
    <div className="flex justify-between mt-4">
      <button type="submit"  onClick={onSubmit} className="border px-4 py-2 rounded bg-blue-500 text-white">Crear</button>
      <button type="button" onClick={onCancel} className="border px-4 py-2 rounded">Cancelar</button>
    </div>
  );
}
