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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var usuarios = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(usuarios);
        }

        public async Task<ClienteDto?> GetByDNIAsync(string dni)
        {
            var usuario = await _repo.GetByDNIAsync(dni);
            return usuario == null ? null : _mapper.Map<ClienteDto>(usuario);
        }

        public async Task<ClienteDto?> GetByEmailAsync(string email)
        {
            var usuario = await _repo.GetByEmailAsync(email);
            return usuario == null ? null : _mapper.Map<ClienteDto>(usuario);
        }

        public async Task<ClienteDto> CreateAsync(ClienteDto dto)
        {
            var usuario = _mapper.Map<Cliente>(dto);
            await _repo.AddAsync(usuario);
            return _mapper.Map<ClienteDto>(usuario);
        }

        public async Task<bool> UpdateAsync(string dni, ClienteDto dto)
        {
            var usuario = await _repo.GetByDNIAsync(dni);
            if (usuario == null)
                return false;

            _mapper.Map(dto, usuario);
            await _repo.UpdateAsync(usuario);
            return true;
        }

        public async Task<bool> ConfirmDNI(string dni)
        {
            var usuario = await _repo.GetByDNIAsync(dni);
            if (usuario == null)
                return false;

            usuario.UsuarioRegistrado.dniVerificado = true; 
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
    }
}
