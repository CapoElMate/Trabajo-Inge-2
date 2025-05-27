using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _repo;
        private readonly IMapper _mapper;

        public EmpleadoService(IEmpleadoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmpleadoDTO>> GetAllAsync()
        {
            var empleados = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<EmpleadoDTO>>(empleados);
        }

        public async Task<EmpleadoDTO?> GetByDNIAsync(string dni)
        {
            var usuario = await _repo.GetByDNIAsync(dni);
            return usuario == null ? null : _mapper.Map<EmpleadoDTO>(usuario);
        }
        public async Task<EmpleadoDTO?> GetByNroEmpleadoAsync(int nroEmpleado)
        {
            var usuario = await _repo.GetByNroEmpleadoAsync(nroEmpleado);
            return usuario == null ? null : _mapper.Map<EmpleadoDTO>(usuario);
        }

        public async Task<EmpleadoDTO?> GetByEmailAsync(string email)
        {
            var usuario = await _repo.GetByEmailAsync(email);
            return usuario == null ? null : _mapper.Map<EmpleadoDTO>(usuario);
        }

        public async Task<EmpleadoDTO> CreateAsync(EmpleadoDTO dto)
        {
            var usuario = _mapper.Map<Empleado>(dto);
            await _repo.AddAsync(usuario);
            return _mapper.Map<EmpleadoDTO>(usuario);
        }

        public async Task<bool> UpdateAsync(string dni, EmpleadoDTO dto)
        {
            var usuario = await _repo.GetByDNIAsync(dni);
            if (usuario == null)
                return false;

            _mapper.Map(dto, usuario);
            await _repo.UpdateAsync(usuario);
            return true;
        }

        public async Task<bool> DeleteByDNIAsync(string DNI)
        {
            var usuario = await _repo.GetByDNIAsync(DNI);
            if (usuario == null)
                return false;

            await _repo.DeleteAsync(usuario);
            return true;
        }
        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var usuario = await _repo.GetByEmailAsync(email);
            if (usuario == null)
                return false;

            await _repo.DeleteAsync(usuario);
            return true;
        }
        public async Task<bool> DeleteByNroEmpleadoAsync(int nroEmpleado)
        {
            var usuario = await _repo.GetByNroEmpleadoAsync(nroEmpleado);
            if (usuario == null)
                return false;

            await _repo.DeleteAsync(usuario);
            return true;
        }
    }
}
