using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage ="O nome do lanche deve ser informado.")]
        [Display(Name ="Nome do Lanche")]
        [StringLength(80, MinimumLength = 10,
            ErrorMessage = "O {0} deve estar dentro do intervalo {1} e {2}")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Favor informar a {0}.")]
        [StringLength(200, MinimumLength = 20, ErrorMessage ="A {0} deve ter algo entre {1} e {2} caracteres.")]
        [Display(Name ="Descricao do Lanche")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage ="A {0} deve ser informada.")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "A {0} deve ter algo entre {1} e {2} caracteres.")]
        [Display(Name ="Descricao detalhada do lanche")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage ="Favor informar o {0}.")]
        [Display(Name ="Preco")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1, 9999.99, ErrorMessage = "O {0} deve estar entre 1 e 9999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres.")]
        public string ImagemUrl { get; set; }


        [Display(Name = "Caminho da Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres.")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
