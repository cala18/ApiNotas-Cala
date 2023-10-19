
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class NotiAppContext : DbContext
{
    public NotiAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Auditoria> Auditorias { get; set; }
    public DbSet<Blockchain> Blockchains { get; set; }
    public DbSet<EstadoNotificacion> EstadoNotificaciones { get; set; }
    public DbSet<Formato> Formatos { get; set; }
    public DbSet<GenericovsSubmodulos> GenericovsSubmodulos { get; set; }
    public DbSet<HiloRespuestaNotificacion> HiloRespuestaNotificaciones { get; set; }
    public DbSet<MaestrosvsSubmodulos> MaestrosvsSubmodulos { get; set; }
    public DbSet<ModuloNotificacion> ModuloNotificaciones { get; set; }
    public DbSet<ModulosMaestro> ModulosMaestros { get; set; }
    public DbSet<PermisosGenericos> PermisosGenericos { get; set; }
    public DbSet<Radicados> Radicados { get; set; }
    public DbSet<Rol> Rols { get; set; }
    public DbSet<RolvsMaestro> RolvsMaestros { get; set; }
    public DbSet<SubModulos> SubModulos { get; set; }
    public DbSet<TipoNotificacion> TipoNotificaciones { get; set; }
    public DbSet<TipoRequerimiento> TipoRequerimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}


