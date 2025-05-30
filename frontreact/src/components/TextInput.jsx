 
export default function TextInput({ label, ...props }) {
  return (
    <div>
      <label>{label}</label>
      <input {...props} className="w-full border p-1 rounded" />
    </div>
  );
}
