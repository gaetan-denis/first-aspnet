using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Domain> Domains { get; set; }
    public DbSet<PostDomain> PostDomains { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>()
        //Chaque post est lié à un seul utilisateur
        .HasOne(p => p.User)
        //Chaque user peut être lié à plusieurs post
        .WithMany(u => u.Posts);

        modelBuilder.Entity<PostDomain>()
        // On indique que la combinaison de PostId et de DomainId forme la clé primaire
        .HasKey(postdom => new { postdom.PostId, postdom.DomainId });

        //Relation entre PostDomain et Post
        modelBuilder.Entity<PostDomain>()
        //Chaque PostDomaine est lié à un post
        .HasOne(postdom => postdom.Post)
        // Un post peut avoir plusieurs Post Domain
        .WithMany(p => p.PostDomains)
        // Clé étrangère dans PostDomain
        .HasForeignKey(postdomain => postdomain.PostId);

        //Relation entre PostDomain et Domain 
        modelBuilder.Entity<PostDomain>()
        //Chaque PostDomaine est lié à un domaine
        .HasOne(postdom => postdom.Domain)
        // Chaque Domaine peut être associé à plusieurs posts
        .WithMany(p => p.PostDomains)
        // Clé étrangère dans PostDomain
        .HasForeignKey(postdomain => postdomain.DomainId);




    }
}