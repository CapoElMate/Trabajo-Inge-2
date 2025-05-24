export default function ImagePreviewList({ imagenes, handleEliminar }) {
  if (!imagenes?.length) return null;

  return (
    <div style={{
    display: "flex",
    justifyContent: "center",
    flexWrap: "wrap",
    gap: "8px",       // equivalente a gap-2 (2 * 4px)
    marginTop: "8px"  // equivalente a mt-2 (2 * 4px)
  }}> 
        {imagenes.map((img, idx) => (
        <div key={img.name} className="relative w-24 h-24">
            <img
            src={URL.createObjectURL(img)}
            alt={`img-${idx}`}
            className="w-full h-full object-cover rounded"
            style={{
                width: "70px",
                height: "70px",
                objectFit: "cover",
                borderRadius: "8px"
            }}
            />
            <button
            type="button"
            onClick={() => handleEliminar(idx)} 
            style={{ width: "20px",
                    height: "20px",
                    borderRadius: "50%",
                    backgroundColor: "red",
                    color: "white",
                    fontSize: "12px",
                    border: "none", 
                    alignItems: "center",
                    justifyContent: "center",
                    cursor: "pointer",  
                }} 
            >
            ×
            </button>
        </div>
        ))}
     </div>
  );
}