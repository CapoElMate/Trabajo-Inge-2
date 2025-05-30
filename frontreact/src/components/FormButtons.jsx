import { TbBackground } from "react-icons/tb";

 export default function FormButtons({ modo="Enviar",onSubmit,onCancel }) {
  return (
    <div className="flex justify-between mt-4">
      <button type="submit"  onClick={onSubmit} 
        style={{
          backgroundColor: "#2563eb", // azul
          color: "white",
          padding: "10px 16px",
          border: "none",
          borderRadius: "8px",
          cursor: "pointer",
          fontSize: "16px",
          fontWeight: "500",
          boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
          transition: "background-color 0.2s ease"
        }}
        onMouseOver={(e) => (e.target.style.backgroundColor = "#1e40af")}
        onMouseOut={(e) => (e.target.style.backgroundColor = "#2563eb")}
      >{modo}</button>
      <button type="button" onClick={onCancel}
      style={{
        backgroundColor: "#e5e7eb", // gris claro
        color: "#111827",           // casi negro
        padding: "10px 16px",
        border: "1px solid #d1d5db",
        borderRadius: "8px",
        cursor: "pointer",
        fontSize: "16px",
        fontWeight: "500",
        boxShadow: "0 1px 3px rgba(0,0,0,0.05)",
        transition: "background-color 0.2s ease"
      }}
      onMouseOver={(e) => (e.target.style.backgroundColor = "#d1d5db")}
      onMouseOut={(e) => (e.target.style.backgroundColor = "#e5e7eb")}
      >Cancelar</button>
    </div>
  );
}
