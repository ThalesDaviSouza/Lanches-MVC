using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Display(Name = "Nome da categoria")]
        [Required(ErrorMessage = "Por favor, informe o nome da categoria.")]
        [StringLength(100, ErrorMessage = "O tamanho do {0} excede o máximo.")]
        public string CategoriaNome { get; set; }
        
        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Por favor, informe a descrição da categoria.")]
        [StringLength(200, ErrorMessage = "O tamanho da {0} excede o máximo.")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}
