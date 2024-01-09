using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheID { get; set; }

        
        [Required(ErrorMessage = "O nome do Lanche deve ser Informado")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Nome { get; set; }

        
        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição Curta do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        public string DescricacoCurta { get; set; }

        
        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição Detalhada do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        public string DescricacoDetalhada { get; set; }

        
        [Required(ErrorMessage = "informe o preço do Lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99,ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        
        [Display(Name = "Caminho Imagem Nomral")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImageURL { get; set; }

        
        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnaiUrl { get; set; }

        
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        
        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        //PROPRIEDADE DE NAVEGAÇÃO
        //Chave estrangeira (Relação entre categoria e Lanche)
        [Display(Name = "Categorias")]
        public int CategoriaID { get; set; }
        //Define o relacionamento entre as classes Categoria e LAnche
        public virtual Categoria Categoria { get; set; }
    }
}
