using LanchesMac.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        //Construtor
        //Ele carregas as informações necessárias para carregar DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Propriedade DbSEt
        //Estou referenciando as classes para as tabelas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set;}
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set;}
        public DbSet<Pedido> Pedidos { get; set;}
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

    }
}
