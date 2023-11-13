namespace LanchesMac.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string CategoriaNome { get; set; }
        public string Descricao { get; set; }

        //1 categoria possui muitos lanches
        //Uma lista que identefica uma relação entre lanche e categoria (1 para N)
        public List<Lanche> Lanches { get; set;}
    }
}
