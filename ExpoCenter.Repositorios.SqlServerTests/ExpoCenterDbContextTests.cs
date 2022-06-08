﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpoCenter.Repositorios.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ExpoCenter.Dominio.Entidades;

namespace ExpoCenter.Repositorios.SqlServer.Tests
{
    [TestClass()]
    public class ExpoCenterDbContextTests
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly DbContextOptions<ExpoCenterDbContext> dbContextOptions;

        public ExpoCenterDbContextTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<ExpoCenterDbContext>()
                .UseSqlServer(new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExpoCenter;Integrated Security=True"))
                .LogTo(ExibirQuery, LogLevel.Information)
                .Options;

            dbContext = new ExpoCenterDbContext(dbContextOptions);
        }

        private void ExibirQuery(string query)
        {
            Console.WriteLine(query);
        }

        [TestMethod()]
        public void InserirEventoTeste()
        {
            var evento = new Evento();
            evento.Local = "Qatar";
            evento.Data = new DateTime(2022, 11, 16);
            evento.Preco = 22.35m;
            evento.Descricao = "Copa do Mundo Fifa";
                        
            dbContext.Eventos.Add(evento);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void InserirParticipanteTeste() 
        {
            Participante participante = new ();
            participante.Cpf = "12345678910";
            participante.Email = "hugo@tciiformatica.com.br";
            participante.DataNascimento = Convert.ToDateTime("30/03/1989");
            participante.Nome = "Hugo";

            participante.Eventos = new List<Evento>
            {
                //dbContext.Eventos.First()
                dbContext.Eventos.Single( e => e.Descricao == "Copa do Mundo Fifa")
            };

            dbContext.Participantes.Add(participante);
            dbContext.SaveChanges();

            foreach (var evento in participante.Eventos)
            {
                Console.WriteLine(evento.Descricao);
            }
        }

        [TestMethod]
        public void SelecionarParticipantesTeste()
        {
            foreach (var participante in dbContext.Participantes)
            {
                Console.WriteLine(participante.Nome);
            }
        }
    }
}