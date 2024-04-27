using Microsoft.EntityFrameworkCore;

namespace tp5mvc.Models.RestosModel
{
    public class RestosDbContext : DbContext
    {
        public RestosDbContext(DbContextOptions<RestosDbContext> options) : base(options)
        {
        }

        public DbSet<Proprietaire> Proprietaires { get; set; }
        public DbSet<Avis> Avis { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proprietaire>()
                .ToTable("TProprietaire", "resto") 
                .HasKey(p => p.Numero);

            modelBuilder.Entity<Proprietaire>()
                .Property(p => p.Nom)
                .HasColumnName("NomProp")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Proprietaire>()
                .Property(p => p.Email)
                .HasColumnName("EmailProp")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Proprietaire>()
                .Property(p => p.Gsm)
                .HasColumnName("GsmProp")
                .HasMaxLength(8)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .ToTable("TRestaurant", "resto") 
                .HasKey(r => r.CodeResto); 

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.NomResto)
                .HasColumnName("NomResto")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Specialite)
                .HasColumnName("SpecResto")
                .HasMaxLength(20)
                .IsRequired()
                .HasDefaultValue("Tunisienne"); 

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Ville)
                .HasColumnName("VilleResto")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Tel)
                .HasColumnName("TelResto")
                .HasMaxLength(8)
                .IsRequired();

            modelBuilder.Entity<Proprietaire>()
                .HasMany(p => p.LesRestos)
                .WithOne(r => r.LeProprio)
                .HasForeignKey(r => r.NumProp)
                .IsRequired() 
                .HasConstraintName("Relation_Proprio_Restos");
            modelBuilder.Entity<Avis>()
       .ToTable("TAvis", "admin") // Nom de la table et schéma
       .HasKey(a => a.CodeAvis); // Clé primaire

            modelBuilder.Entity<Avis>()
                .Property(a => a.NomPersonne)
                .HasColumnName("NomPersonne")
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Avis>()
                .Property(a => a.Note)
                .HasColumnName("Note")
                .IsRequired();

            modelBuilder.Entity<Avis>()
                .Property(a => a.Commentaire)
                .HasColumnName("Commentaire")
                .HasMaxLength(256)
                .IsRequired(false); // Commentaire non obligatoire
            modelBuilder.Entity<Restaurant>()
    .HasMany(r => r.LesAvis)
    .WithOne(a => a.LeResto)
    .HasForeignKey(a => a.NumResto)
    .IsRequired()
    .HasConstraintName("Relation_Resto_Avis");
        }

    

    }
}
