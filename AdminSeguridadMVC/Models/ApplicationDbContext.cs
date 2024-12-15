using AdminSeguridadMVC.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<PermisoMenu> PermisoMenus { get; set; }
    public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación Usuario - Rol
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.RolID)
            .OnDelete(DeleteBehavior.Cascade);

        // Tabla relacional PermisoMenu
        modelBuilder.Entity<PermisoMenu>()
            .HasKey(pm => new { pm.MenuID, pm.PermisoID });

        modelBuilder.Entity<PermisoMenu>()
            .HasOne(pm => pm.Menu)
            .WithMany(m => m.PermisoMenus)
            .HasForeignKey(pm => pm.MenuID);

        modelBuilder.Entity<PermisoMenu>()
            .HasOne(pm => pm.Permiso)
            .WithMany(p => p.PermisoMenus)
            .HasForeignKey(pm => pm.PermisoID);

        // Tabla relacional UsuarioPermiso
        modelBuilder.Entity<UsuarioPermiso>()
            .HasKey(up => new { up.UsuarioID, up.PermisoID });

        modelBuilder.Entity<UsuarioPermiso>()
            .HasOne(up => up.Usuario)
            .WithMany(u => u.UsuarioPermisos)
            .HasForeignKey(up => up.UsuarioID);

        modelBuilder.Entity<UsuarioPermiso>()
            .HasOne(up => up.Permiso)
            .WithMany(p => p.UsuarioPermisos)
            .HasForeignKey(up => up.PermisoID);
    }
}
