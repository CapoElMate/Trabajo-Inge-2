using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Domain_Layer.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly ApplicationDbContext _context;

        public ArchivoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Archivo?> GetByIdAsync(int id)
        {
            // Retorna un solo archivo por su ID.
            // No hay Includes específicos aquí a menos que Archivo tenga relaciones con otras entidades que necesites cargar.
            return await _context.Archivos.FirstOrDefaultAsync(a => a.idArchivo == id);
        }

        public async Task<IEnumerable<Archivo>> GetByEntidadAsync(int entidadId, TipoEntidadArchivo tipoEntidad)
        {
            // Retorna todos los archivos asociados a una entidad específica y tipo de entidad.
            // Asumimos que quieres solo los archivos activos si estás implementando borrado lógico.
            return await _context.Archivos
                                 .Where(a => a.EntidadID == entidadId &&
                                             a.TipoEntidad == tipoEntidad)
                                 .ToListAsync();
        }

        public async Task AddAsync(Archivo archivo)
        {
            // Agrega un nuevo registro de archivo a la base de datos.
            await _context.Archivos.AddAsync(archivo);
            await _context.SaveChangesAsync(); // Guarda los cambios para persistir el nuevo archivo.
        }

        public async Task UpdateAsync(Archivo archivo)
        {
            // Actualiza un registro de archivo existente en la base de datos.
            // Esto es útil para el borrado lógico (cambiar 'EstaActivo') o para actualizar metadatos.
            _context.Archivos.Update(archivo);
            await _context.SaveChangesAsync(); // Guarda los cambios.
        }

        public async Task DeleteAsync(Archivo archivo)
        {
            // Elimina un registro de archivo de la base de datos de forma permanente.
            // Usa esto con precaución, ya que también deberías eliminar el archivo físico del servidor.
            _context.Archivos.Remove(archivo);
            await _context.SaveChangesAsync(); // Guarda los cambios.
        }

        // Opcional: Si quieres un método 'Exists' para archivos, similar al de Alquiler.
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Archivos.AnyAsync(a => a.idArchivo == id);
        }
    }
}
