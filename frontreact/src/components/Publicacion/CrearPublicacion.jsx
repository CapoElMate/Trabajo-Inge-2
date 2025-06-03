import { useState } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import ImageUploader from "../ImageUploader";
import ImagePreviewList from "../ImagePreviwList";
import Header from "../Header";
import { useNavigate } from "react-router-dom";
import PublicacionForm from "./FormPublicacion";

export default function CrearPublicacion() {
  const navigate = useNavigate();

  const handleSubmit = async () => {
    console.log("Publicación creada.");  
    navigate("/HomePage");
  };

  const handleCancel = () => {
    console.log("Cancelado");
    navigate("/HomePage");
  };

  return (
    <>
      <Header />
      <PublicacionForm onSubmit={handleSubmit} onCancel={handleCancel} />
    </>
  );
}
