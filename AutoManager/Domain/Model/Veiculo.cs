using System.ComponentModel.DataAnnotations;

namespace AutoManager.Domain.Model
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor {  get; set; }
    }
}
