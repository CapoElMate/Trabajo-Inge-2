import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const schema = yup.object().shape({
  dni: yup
    .string()
    .matches(/^\d{8}$/, '')
    .required(''),
  email: yup
    .string()
    .email('Correo inválido')
    .required(''),
  birthDate: yup
    .date()
    .max(new Date(new Date().setFullYear(new Date().getFullYear() - 18)), 'Debes tener al menos 18 años')
    .required(''),
  street: yup.string().required(''),
  height: yup.string().required(''),
  dpto: yup.string(),
  nro: yup.string(),
  phone: yup
    .string()
    .matches(/^\d{10}$/, '')
    .required(''),
  password: yup
    .string()
    .min(8, 'La contraseña debe tener al menos 8 caracteres')
    .matches(/[A-Z]/, 'Debe contener una letra mayúscula')
    .matches(/[a-z]/, 'Debe contener una letra minúscula')
    .matches(/\d/, 'Debe contener un número')
    .matches(/[!@#$%^&*(),.?":{}|<>]/, 'Debe contener un símbolo')
    .required(''),
  confirmPassword: yup
    .string()
    .oneOf([yup.ref('password')], 'Las contraseñas no coinciden')
    .required('Confirma tu contraseña'),
  dniImage: yup
    .mixed()
    .required('Debes subir una imagen del DNI')
    .test('', ' Solo JPG, JPEG o PNG.', (value) => {
      if (!value || !value[0]) return false;
      const allowedTypes = ['image/jpeg', 'image/png', 'image/jpg'];
      return allowedTypes.includes(value[0].type);
    }),
});

export default function SignUp() {
    const navigate = useNavigate();
  const [usuarios, setUsuarios] = useState([]);
  const [customErrors, setCustomErrors] = useState({ dni: '', email: '' });

  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
    clearErrors,
  } = useForm({
    resolver: yupResolver(schema),
  });

  useEffect(() => {
    axios
      .get('http://localhost:5000/api/Usuario/all')
      .then((res) => setUsuarios(res.data))
      .catch((err) => console.error('Error al cargar usuarios:', err));
  }, []);

  const onSubmit = (data) => {
    const dniExists = usuarios.some((u) => u.dni === data.dni);
    const emailExists = usuarios.some((u) => u.email === data.email);

    // Reiniciar errores previos
    setCustomErrors({ dni: '', email: '' });
    clearErrors('dni');
    clearErrors('email');

    if (dniExists || emailExists) {
      if (dniExists) {
        setError('dni', { type: 'manual', message: 'El DNI ya está registrado' });
      }
      if (emailExists) {
        setError('email', { type: 'manual', message: 'El correo ya está registrado' });
      }
      return;
    }

    console.log(data);
    navigate("/Login");
    // Aquí podrías redirigir al login
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="max-w-md mx-auto p-4 border rounded-md space-y-4">
      <h2 className="text-xl font-bold text-center">Registro de Usuario</h2>

      <div>
        <label>DNI</label>
        <input {...register('dni')} className="w-full border p-2" />
        {errors.dni && <p className="text-red-600 text-sm">{errors.dni.message}</p>}
      </div>

      <div>
        <label>Email</label>
        <input {...register('email')} className="w-full border p-2" />
        {errors.email && <p className="text-red-600 text-sm">{errors.email.message}</p>}
      </div>

      <div>
        <label>Fecha de nacimiento</label>
        <input type="date" {...register('birthDate')} className="w-full border p-2" />
        {errors.birthDate && <p className="text-red-600 text-sm">{errors.birthDate.message}</p>}
      </div>

      <div>
        <label>Calle</label>
        <input {...register('street')} className="w-full border p-2" />
        {errors.street && <p className="text-red-600 text-sm">{errors.street.message}</p>}
      </div>

      <div>
        <label>Altura</label>
        <input {...register('height')} className="w-full border p-2" />
        {errors.height && <p className="text-red-600 text-sm">{errors.height.message}</p>}
      </div>

      <div>
        <label>Departamento</label>
        <input {...register('dpto')} className="w-full border p-2" />
      </div>

      <div>
        <label>Número</label>
        <input {...register('nro')} className="w-full border p-2" />
      </div>

      <div>
        <label>Teléfono</label>
        <input {...register('phone')} className="w-full border p-2" />
        {errors.phone && <p className="text-red-600 text-sm">{errors.phone.message}</p>}
      </div>

      <div>
        <label>Contraseña</label>
        <input type="password" {...register('password')} className="w-full border p-2" />
        {errors.password && <p className="text-red-600 text-sm">{errors.password.message}</p>}
      </div>

      <div>
        <label>Confirmar Contraseña</label>
        <input type="password" {...register('confirmPassword')} className="w-full border p-2" />
        {errors.confirmPassword && <p className="text-red-600 text-sm">{errors.confirmPassword.message}</p>}
      </div>

      <div>
        <label>Imagen del DNI (JPG, JPEG, PNG)</label>
        <input type="file" {...register('dniImage')} accept="image/jpeg, image/png, image/jpg" className="w-full" />
        {errors.dniImage && <p className="text-red-600 text-sm">{errors.dniImage.message}</p>}
      </div>

      <button type="submit" className="bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700 w-full">
        Registrarse
      </button>
    </form>
  );
}
