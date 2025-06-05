export default function SelectInput({
  optionCompleja = true,
  label,
  options = [],
  defaultOptionLabel = "Seleccionar...",
  ...props
}) {
  return (
    <div>
      <label>{label}</label>
      <select {...props} className="w-full border p-1 rounded">
        <option value="" disabled>
          {defaultOptionLabel}
        </option>
        {options.map(opt =>
          optionCompleja ? (
            <option key={opt} value={opt}>
              {opt}
            </option>
          ) : (
            <option key={opt.value} value={opt.value}>
              {opt.label}
            </option>
          )
        )}
      </select>
    </div>
  );
}