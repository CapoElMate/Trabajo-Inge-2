namespace Bussines_Logic_Layer.DTOs
{
    public class MarcaDto
    {
        public string Marca { get; set; } = null!;
        public ICollection<ModeloDto> Modelos { get; set; } = new List<ModeloDto>();
    }
}