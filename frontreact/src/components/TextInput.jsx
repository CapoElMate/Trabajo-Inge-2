import { useState, useEffect } from "react";

export default function TextInput({
  label,
  isRequired = false,
  value: valueProp,
  onChange: onChangeProp,
  ...props
}) {
  const [value, setValue] = useState(String(valueProp ?? ""));

  useEffect(() => {
  setValue(String(valueProp ?? ""));
}, [valueProp]);

  const handleChange = (e) => {
    setValue(e.target.value);
    if (onChangeProp) onChangeProp(e);
  };

  const asteriscoColor = isRequired && String(value).trim() === "" ? "red" : "black";

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
      <input
        {...props}
        value={value}
        onChange={handleChange}
        className="w-full border p-1 rounded"
      />
    </div>
  );
}