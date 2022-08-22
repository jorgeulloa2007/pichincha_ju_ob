using creditoautomovilistico.Infrastructure.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace creditoautomovilistico.Infrastructure.Context
{
    public class DatabaseContext: DbContext
    {
        private readonly IConfiguration _configuration;

        #region Context DbSets
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Ejecutivo> Ejecutivos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<SolicitudCredito> Solicitudes { get; set; }

        #endregion

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("AutoCreditosDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Persona>().ToTable("Personas");

            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            modelBuilder.Entity<Ejecutivo>().ToTable("Ejecutivos");

            //Marca should not be duplicated
            modelBuilder.Entity<Marca>()
                .HasIndex(m => m.MarcaAuto)
                .IsUnique();
            
            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.Identificacion)
                .IsUnique();
            
            modelBuilder.Entity<Vehiculo>()
                .HasIndex(v => v.Placa)
                .IsUnique();
            
            base.OnModelCreating(modelBuilder);
            
        }

        public void Seed()
        {
            // Will not seed if there´s data already loaded in tables
            if (Personas.Any())
                return;

            var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture);
            csvConfig.HeaderValidated = null;
            csvConfig.MissingFieldFound = null;

            Assembly assembly = Assembly.GetExecutingAssembly();

            try
            {
                //Seeding Clientes
                string resourceName = "creditoautomovilistico.Infrastructure.SeedData.Clientes.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader, csvConfig);
                        var clientes = csvReader.GetRecords<Cliente>().ToArray();
                        Clientes.AddRange(clientes);
                    }
                }

                //Seeding Patios (we need to do this before seeding Ejecutivos, because Ejecutivos has a foreign key with Patios)
                resourceName = "creditoautomovilistico.Infrastructure.SeedData.Patios.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader, csvConfig);
                        var patios = csvReader.GetRecords<Patio>().ToArray();
                        Patios.AddRange(patios);
                    }
                }

                // Seeding Ejecutivos
                resourceName = "creditoautomovilistico.Infrastructure.SeedData.Ejecutivos.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader, csvConfig);
                        var ejecutivos = csvReader.GetRecords<Ejecutivo>().ToArray();
                        Ejecutivos.AddRange(ejecutivos);
                    }
                }

                // Seeding Marcas
                resourceName = "creditoautomovilistico.Infrastructure.SeedData.Marcas.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader, csvConfig);
                        var marcas = csvReader.GetRecords<Marca>().ToArray();
                        Marcas.AddRange(marcas);
                    }
                }

                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error en carga inicial de datos: " 
                    + ex.Message 
                    + ex.InnerException?.Message);
            }
        }
    }
}
