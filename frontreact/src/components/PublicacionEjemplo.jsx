import axios, { Axios } from 'axios';
import { useEffect, useState } from 'react';

function PublicacionEjemplo(){
    const [publicaciones,setPubs] = useState([]);
    const [imagenes,setImagenes]=useState({});
    useEffect(() => {
  axios.get('http://localhost:5000/api/Publicacion/all')
    .then(response => {
      const publicaciones = response.data;
      setPubs(publicaciones);

      // Para cada publicacion, buscar su imagen
      publicaciones.forEach(pub => {
        axios.get(`http://localhost:5000/api/Archivo/byEntidad?entidadId=${pub.idPublicacion}&tipoEntidad=0`)
          .then(res => {
            const imagenB64 = res.data[0]; // asumimos que es un array y tomamos la primera
            setImagenes(prev => ({
              ...prev,
              [pub.idPublicacion]: imagenB64
            }));
          })
          .catch(err => console.error(`Error obteniendo imagen para pub ${pub.idPublicacion}`, err));
      });
    })
    .catch(error => console.error('Error en petición GET de publicaciones:', error));
}, []);



    return (<>
        <h1>Probando endpoint de publicaciones</h1>
       <div>
        {publicaciones.map(pub=>(
            <div key={pub.idPublicacion}> 
            ID :{pub.idPublicacion}- Estado : {pub.status} Precio por dia{pub.precioPorDia}-
            Descripcion :{pub.descripcion}
            <img src={`data:image/jpeg;base64,${imagenes[pub.idPublicacion]}`} alt={`IMgen de maquina ${pub.maquina.modelo.modelo}`} />
            </div>
            
        ))}
      </div>
    </>);
}
export default PublicacionEjemplo;