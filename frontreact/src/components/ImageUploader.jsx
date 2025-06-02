import "./ImageUploader.css"

import { useState, useRef } from "react";

export default function ImageUploader({ onChange }) {
  const [images, setImages] = useState([]);
  const dropRef = useRef(null);

  const handleFiles = (files) => {
    const fileArray = Array.from(files);
    const imagePreviews = fileArray.map((file) => ({
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
    const files = e.dataTransfer.files;
    handleFiles(files);
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
        onDrop={handleDrop}
        onDragOver={handleDragOver}
        onDragLeave={handleDragLeave}
        className="image-drop-area"
      >
        <p>Arrastrá las imágenes o hacé clic para seleccionarlas</p>
        <input
          type="file"
          accept="image/*"
          multiple
          onChange={handleInputChange}
          className="hidden"
        />
      </div>

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