import "./ImageUploader.css";
import { useState, useRef, useEffect } from "react";
import "./Publicacion/FormPublicacion.css";

export default function ImageUploader({ onChange, initialImages = [] }) {
  const [images, setImages] = useState([]);
  const [error, setError] = useState("");
  const dropRef = useRef(null);
  const inputRef = useRef(null);

  const allowedExtensions = ["jpg", "jpeg", "png"];

  const isValidFile = (file) => {
    if (!file.name) return true; // para base64 u objetos existentes
    const extension = file.name.split(".").pop().toLowerCase();
    return allowedExtensions.includes(extension);
  };

  const handleFiles = (files) => {
    const fileArray = Array.from(files);
    const validFiles = fileArray.filter(isValidFile);

    if (fileArray.length !== validFiles.length) {
      setError("Las imágenes deben ser en formato .png, .jpg o .jpeg");
    } else {
      setError("");
    }

    const imagePreviews = validFiles.map((file) => ({
      file,
      preview: URL.createObjectURL(file),
    }));

    const newImages = [...images, ...imagePreviews];
    setImages(newImages);
    notifyChange(newImages);
  };

  const handleDrop = (e) => {
    e.preventDefault();
    e.stopPropagation();
    handleFiles(e.dataTransfer.files);
    dropRef.current.classList.remove("dragover");
  };

  const handleDragOver = (e) => {
    e.preventDefault();
    dropRef.current.classList.add("dragover");
  };

  const handleDragLeave = () => {
    dropRef.current.classList.remove("dragover");
  };

  const handleInputChange = (e) => {
    handleFiles(e.target.files);
    e.target.value = null; // Limpiar el input para permitir subir la misma imagen de nuevo
  };

  const handleClick = () => {
    inputRef.current?.click();
  };

  const removeImage = (index) => {
    const newImages = [...images];
    const removed = newImages.splice(index, 1)[0];
    if (removed.preview?.startsWith("blob:")) {
      URL.revokeObjectURL(removed.preview);
    }
    setImages(newImages);
    notifyChange(newImages);
  };

  const notifyChange = (imagenes) => {
    const result = imagenes.map((img) => {
      if (img.original) {
        return {
          original: img.original,
          archivoBase64: img.original.archivoBase64,
        };
      } else {
        return {
          file: img.file,
          archivoBase64: img.preview, // se convierte a base64 en el submit
        };
      }
    });
    onChange(result);
  };

  useEffect(() => {
    if (initialImages.length > 0) {
      const base64Images = initialImages.map((img) => ({
        file: null,
        preview: img.archivoBase64.startsWith("data:")
          ? img.archivoBase64
          : `data:image/jpeg;base64,${img.archivoBase64}`,
        original: img,
      }));
      setImages(base64Images);
      notifyChange(base64Images); // notifica al iniciar
    }
  }, [initialImages]);

  return (
    <div>
      <label className="block mb-1 font-medium">Cargar imágenes</label>
      <div
        ref={dropRef}
        onClick={handleClick}
        onDrop={handleDrop}
        onDragOver={handleDragOver}
        onDragLeave={handleDragLeave}
        className="image-drop-area"
      >
        <p>
          Arrastrá las imágenes o hacé clic para seleccionarlas (.jpg, .jpeg,
          .png)
        </p>
        <input
          ref={inputRef}
          type="file"
          accept=".jpg,.jpeg,.png"
          multiple
          onChange={handleInputChange}
          className="hidden"
        />
      </div>

      {error && (
        <p className="text-red-600 text-sm mt-1 error-message">{error}</p>
      )}

      {images.length > 0 && (
        <div className="preview-grid">
          {images.map((img, index) => (
            <div key={index} className="thumbnail-wrapper">
              <img
                src={img.preview}
                alt={`img-${index}`}
                className="thumbnail"
              />
              <button
                type="button"
                onClick={() => removeImage(index)}
                className="remove-button"
              >
                ✖
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
