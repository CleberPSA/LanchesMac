namespace LanchesMac.Models
{
    public class Lanche
    {
        public int LancheID { get; set; }
        public string Nome { get; set; }
        public string DescricacoCurta { get; set;}
        public string DescricacoDetalhada { get; set; }
        public decimal Preco { get; set; }
        public string ImageURL { get; set; }
        public string ImagemThumbnaiUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }

        //PROPRIEDADE DE NAVEGAÇÃO
        //Chave estrangeira (Relação entre categoria e Lanche)
        public int CategoriaID { get; set; }
        //Define o relacionamento entre as classes Categoria e LAnche
        public virtual Categoria Categoria { get; set; }
    }
}
