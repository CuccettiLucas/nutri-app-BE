using App_Nutri.Models;
using Microsoft.EntityFrameworkCore;

namespace App_Nutri.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<RegistroAlimento> RegistrosAlimento { get; set; }
        public DbSet<ListaAlimentos> ListasAlimentos { get; set; }
        public DbSet<ListaAlimentosAlimento> ListaAlimentosAlimentos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<ImagenesPaciente> ImagenesPacientes { get; set; }
        public DbSet<HistoriaClinica> HistoriaClinica { get; set; }
        public DbSet<PacienteListaPersonalizada> PacienteListaPersonalizadas { get; set; }
        public DbSet<AlimentoPacientePersonalizado> alimentoPacientePersonalizados { get; set; }
        public DbSet<SubCategoriaPacientePersonalizada> SubCategoriaPacientePersonalizadas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlimentoPaciente>()
                .HasKey(ap => new { ap.AlimentoId, ap.PacienteId });

            modelBuilder.Entity<Paciente>()
                .OwnsOne(p => p.Porcion);

            modelBuilder.Entity<Paciente>()
                .OwnsOne(p => p.Anamnesis);

            modelBuilder.Entity<SubCategoria>()
            .HasOne(sc => sc.Categoria)
            .WithMany(c => c.SubCategorias)
            .HasForeignKey(sc => sc.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Alimento>()
            .HasOne(a => a.Categoria)
            .WithMany(c => c.Alimentos)
            .HasForeignKey(a => a.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Alimento>()
            .HasOne(a => a.SubCategorias)
            .WithMany(sc => sc.Alimentos)
            .HasForeignKey(a => a.SubCategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

            // Relacion de Alimentos -> ListaAlimentos
            modelBuilder.Entity<ListaAlimentosAlimento>()
                .HasKey(la => new { la.ListaAlimentosId, la.AlimentoId });

            modelBuilder.Entity<ListaAlimentosAlimento>()
                .HasOne(la => la.ListaAlimentos)
                .WithMany(l => l.ListaAlimentosAlimentos)
                .HasForeignKey(la => la.ListaAlimentosId);

            modelBuilder.Entity<ListaAlimentosAlimento>()
                .HasOne(la => la.Alimento)
                .WithMany(a => a.ListaAlimentosAlimentos)
                .HasForeignKey(la => la.AlimentoId);

            // Relación Paciente -> ListaAlimentos
            modelBuilder.Entity<Paciente>()
            .HasOne<ListaAlimentos>()
            .WithMany()
            .HasForeignKey(p => p.ListaAlimentoId)
            .OnDelete(DeleteBehavior.Restrict);

            // Relación Paciente -> Profesional
            modelBuilder.Entity<Paciente>()
            .HasOne<Profesional>()
            .WithMany()
            .HasForeignKey(p => p.ProfAsignadoId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ListaAlimentosAlimento>()
                   .HasKey(la => new { la.ListaAlimentosId, la.AlimentoId });

            modelBuilder.Entity<ListaAlimentosAlimento>()
                .HasOne(la => la.ListaAlimentos)
                .WithMany(l => l.ListaAlimentosAlimentos)
                .HasForeignKey(la => la.ListaAlimentosId);

            modelBuilder.Entity<ListaAlimentosAlimento>()
                .HasOne(la => la.Alimento)
                .WithMany(a => a.ListaAlimentosAlimentos)
                .HasForeignKey(la => la.AlimentoId);

            //Relación Alimentos personalizados
            modelBuilder.Entity<PacienteListaPersonalizada>()
                .HasOne(plp => plp.Paciente)
                .WithMany(p => p.ListasPersonalizadas)
                .HasForeignKey(plp => plp.PacienteId);

            modelBuilder.Entity<PacienteListaPersonalizada>()
                .HasOne(plp => plp.ListaAlimentos)
                .WithMany(la => la.PacientesAsignados)
                .HasForeignKey(plp => plp.ListaAlimentosId);

            modelBuilder.Entity<AlimentoPacientePersonalizado>()
                .HasOne(ap => ap.PacienteListaPersonalizada)
                .WithMany(plp => plp.AlimentosPersonalizados)
                .HasForeignKey(ap => ap.PacienteListaPersonalizadaId);

            modelBuilder.Entity<AlimentoPacientePersonalizado>()
                .HasOne(ap => ap.Alimento)
                .WithMany()
                .HasForeignKey(ap => ap.AlimentoId);


        }
    }

}
