import { useState, useEffect } from "react";

export default function SelectInput({
  optionCompleja = true,
  label,
  options = [],
  defaultOptionLabel = "Seleccionar...",
  isRequired = false,
  value: valueProp,
  onChange: onChangeProp,
  ...props
}) {
  const [value, setValue] = useState(valueProp || "");

  useEffect(() => {
    setValue(valueProp || "");
  }, [valueProp]);

  const handleChange = (e) => {
    setValue(e.target.value);
    if (onChangeProp) onChangeProp(e);
  };

  const asteriscoColor = isRequired && (value === "" || value === null) ? "red" : "black";

  return (
    <div>
      <label
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          marginBottom: "0.25rem",
        }}
      >
        {label}
        {isRequired && (
          <span
            style={{
              color: asteriscoColor,
              marginLeft: "0.25rem",
            }}
          >
            *
          </span>
        )}
      </label>
      <select
        {...props}
        value={value}
        onChange={handleChange}
        className="w-full border p-1 rounded"
      >
        <option value="" disabled>
          {defaultOptionLabel}
        </option>
        {options.map((opt) =>
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