using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 Caracteres")]
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho máximo é 200 Caracteres")]
        [Required(ErrorMessage = "Informe o descrição da categoria")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        //1 categoria possui muitos lanches
        //Uma lista que identefica uma relação entre lanche e categoria (1 para N)
        public List<Lanche> Lanches { get; set; }
    }
}
