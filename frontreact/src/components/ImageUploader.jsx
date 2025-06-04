import "./ImageUploader.css";
import { useState, useRef } from "react";
import "./Publicacion/FormPublicacion.css";

export default function ImageUploader({ onChange }) {
  const [images, setImages] = useState([]);
  const [error, setError] = useState("");
  const dropRef = useRef(null);
  const inputRef = useRef(null);

  const allowedExtensions = ["jpg", "jpeg", "png"];

  const isValidFile = (file) => {
    const extension = file.name.split(".").pop().toLowerCase();
    return allowedExtensions.includes(extension);
  };

  const handleFiles = (files) => {
    const fileArray = Array.from(files);

    const validFiles = fileArray.filter(isValidFile);
    const invalidFiles = fileArray.filter((file) => !isValidFile(file));

    if (invalidFiles.length > 0) {
      setError("Solo se permiten archivos .jpg, .jpeg o .png");
    } else {
      setError("");
    }

    const imagePreviews = validFiles.map((file) => ({
      file,
      preview: URL.createObjectURL(file),
    }));

    const newImages = [...images, ...imagePreviews];
    setImages(newImages);
    onChange(newImages.map((img) => img.file));
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
  };

  const handleClick = () => {
    inputRef.current?.click();
  };

  const removeImage = (index) => {
    const newImages = [...images];
    URL.revokeObjectURL(newImages[index].preview);
    newImages.splice(index, 1);
    setImages(newImages);
    onChange(newImages.map((img) => img.file));
  };

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
        <p>Arrastrá las imágenes o hacé clic para seleccionarlas (.jpg, .jpeg, .png)</p>
        <input
          ref={inputRef}
          type="file"
          accept=".jpg,.jpeg,.png"
          multiple
          onChange={handleInputChange}
          className="hidden"
        />
      </div>

      {error && <p className="text-red-600 text-sm mt-1 error-message">{error}</p>}

      {images.length > 0 && (
        <div className="preview-grid">
          {images.map((img, index) => (
            <div key={index} className="thumbnail-wrapper">
              <img src={img.preview} alt={`img-${index}`} className="thumbnail" />
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