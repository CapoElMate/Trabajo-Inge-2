import React, { useState, useRef } from 'react';
import './PermitFileUploader.css';
import Header from './Header';

function PermitFileUploader({ onFileUpload }) {
  const [selectedFile, setSelectedFile] = useState(null);
  const [filePreview, setFilePreview] = useState(null);
  const fileInputRef = useRef(null);

  const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      setSelectedFile(file);
      if (file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onloadend = () => {
          setFilePreview(reader.result);
        };
        reader.readAsDataURL(file);
      } else {
        setFilePreview(null);
      }
    } else {
      setSelectedFile(null);
      setFilePreview(null);
    }
  };

  const handleUploadClick = () => {
    if (selectedFile) {
      console.log('Archivo a subir:', selectedFile.name, selectedFile);
      if (onFileUpload) {
        onFileUpload(selectedFile);
      }
      alert(`¡Archivo "${selectedFile.name}" listo para subir! (Implementar lógica de subida real)`);
      setSelectedFile(null);
      setFilePreview(null);
      if (fileInputRef.current) {
        fileInputRef.current.value = '';
      }
    } else {
      alert('Por favor, selecciona un archivo primero.');
    }
  };

  return (<>
    <Header/>
    <div className="permit-file-uploader-container">
      <h2>Subir Permiso de Maquinaria</h2>
      <p>Sube una foto o PDF de tu permiso para operar maquinarias.</p>

      <div className="file-selector-area">
        <input
          type="file"
          accept="image/*, application/pdf"
          onChange={handleFileChange}
          ref={fileInputRef}
          style={{ display: 'none' }}
        />
        <button
          className="select-file-button"
          onClick={() => fileInputRef.current.click()}
        >
          {selectedFile ? 'Cambiar Archivo' : 'Seleccionar Archivo'}
        </button>

        {filePreview ? (
          <img src={filePreview} alt="Vista previa del permiso" className="file-preview-image" />
        ) : selectedFile ? (
          <p className="selected-file-name">Archivo seleccionado: <strong>{selectedFile.name}</strong></p>
        ) : (
          <p className="no-file-selected">Ningún archivo seleccionado.</p>
        )}
      </div>

      <button
        onClick={handleUploadClick}
        className="upload-button"
        disabled={!selectedFile}
      >
        Subir Archivo
      </button>

      {selectedFile && !filePreview && selectedFile.type.startsWith('image/') && (
        <p className="loading-preview">Cargando vista previa de la imagen...</p>
      )}

      {selectedFile && selectedFile.type && !['image/jpeg', 'image/png', 'application/pdf'].includes(selectedFile.type) && (
        <p className="file-type-warning">
          Tipo de archivo no compatible. Por favor, sube una imagen (JPG, PNG) o un PDF.
        </p>
      )}
    </div>
  </>);
}

export default PermitFileUploader;