
using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using LanchesMac.Services;
using LanchesMac.Areas.Admin.Services;
using ReflectionIT.Mvc.Paging;
using FastReport.Data;

var builder = WebApplication.CreateBuilder(args);

//Conex�o Sql
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseSqlServer(connection));

//Relat�rio PDF (FASTREPORT)
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

//Identity (LOGIN)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Gerenciamneto de Imagem
builder.Services.Configure<ConfigurationImagens>(builder.
    Configuration.GetSection("ConfigurationPastaImagens"));



//Senha padr�o
builder.Services.Configure<IdentityOptions>(options =>
{
    //Default Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});


//Inje��o de dependecia
builder.Services.AddTransient<ILanchesRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();

//Escopo de servi�o - Container de Inje��o de independ�ncia
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<RelatorioVendaService>();
builder.Services.AddScoped<GraficoVendasService>();
builder.Services.AddScoped<RelatorioLanchesService>();
builder.Services.AddFastReport();

//Adquirindo a politica
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        politica =>
        {
            politica.RequireRole("Admin");
        });
});

//session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Metodo carrinho de compra
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

//Controladora
builder.Services.AddControllersWithViews();

//Pagina��o
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});

//Iniciando a ses�o
builder.Services.AddMemoryCache();
builder.Services.AddSession();

//Registrar os Servi�os

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseFastReport();


app.UseRouting();

CriarPerfilUsuario(app);

////Cria os perfis
//seedUserRoleInitial.SeedRoles();
////cria os usuarios e atribui ao perfil
//seedUserRoleInitial.SeedUsers();

//Ativando o session
app.UseSession();

//Entity(LOGIN)
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //Area

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );


    //endpoints.MapControllerRoute(
    //    name: "teste",
    //    pattern: "testeme",
    //    defaults: new { controller = "teste", Action = "index" });

    //endpoints.MapControllerRoute(
    //    name: "admin",
    //    pattern: "admin/{action=index}/{id?}",
    //    defaults: new { controller = "admin" }
    //    );

    //endpoints.MapControllerRoute(
    //    name: "home",
    //    pattern: "{home}",
    //    defaults: new { Controller = "Home", Action = "Index" });

    //endpoints.MapControllerRoute(
    //    name: "admin",
    //    pattern: "admin",
    //    defaults: new { Controller = "Admin", Action = "Index" });


    endpoints.MapControllerRoute(
        name: "categoriaFiltro",
        pattern: "Lanche/{action}/{categoria?}",
        defaults: new { controller = "Lanche", action = "List" });




    //roteamento padr�o
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});




//configurar o pipeline do request da aplica��o
app.Run();

void CriarPerfilUsuario(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var services = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        services.SeedRoles();
        services.SeedUsers();
    }


}