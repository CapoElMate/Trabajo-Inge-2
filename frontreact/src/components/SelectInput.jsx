 
export default function SelectInput({ label, options = [], ...props }) {
  return (
    <div>
      <label>{label}</label>
      <select {...props} className="w-full border p-1 rounded" >
        <option value="">Seleccionar...</option>
        {options.map(opt => (
          <option key={opt} value={opt}>{opt}</option>
        ))}
      </select>
    </div>
  );
}
