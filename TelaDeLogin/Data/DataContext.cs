using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TelaDeLogin.Models;

namespace TelaDeLogin.Data
{
    public class DataContext : DbContext
    {
        /* Configuração da conexão do banco de dados, criação do objeto builder apontando para o arquivo Json com a 
         configuração do banco de dados e sendo executado pela configuration, e nesse configuration é passado 
        os parâmetros de conexão DefaultConnection dentro do arquivo Json */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
        }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
