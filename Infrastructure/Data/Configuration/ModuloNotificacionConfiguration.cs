using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;
public class ModuloNotificacionConfiguration : IEntityTypeConfiguration<ModuloNotificacion>
{
    public void Configure(EntityTypeBuilder<ModuloNotificacion> builder)
    {
        builder.ToTable("modulonotificacion");

        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id);

        builder.Property(x=>x.AsuntoNotificacion).IsRequired().HasMaxLength(80);
        builder.Property(x=>x.TextoNotificacion).IsRequired().HasMaxLength(2000);
        builder.Property(x=>x.FechaCreacion).HasColumnType("date");
        builder.Property(x=>x.FechaModificacion).HasColumnType("date");

        builder.HasOne(x=>x.Radicados).WithMany(x=>x.ModuloNotificaciones).HasForeignKey(x=>x.IdRadicadosFk);
        builder.HasOne(x=>x.Formatos).WithMany(x=>x.ModuloNotificaciones).HasForeignKey(x=>x.IdFormatosFk);
        builder.HasOne(x=>x.TipoRequerimientos).WithMany(x=>x.ModuloNotificaciones).HasForeignKey(x=>x.IdTipoRequerimientoFk);
        builder.HasOne(x=>x.EstadoNotificaciones).WithMany(x=>x.ModuloNotificaciones).HasForeignKey(x=>x.IdEstadoNotificacionFk);
        builder.HasOne(x=>x.TipoNotificaciones).WithMany(x=>x.ModuloNotificaciones).HasForeignKey(x=>x.IdTipoNotificacionesFk);
        builder.HasOne(x=>x.HiloRespuestaNotificaciones).WithMany(x=>x.ModuloNotificaciones).HasForeignKey(x=>x.IdHiloRespuestaFk);

    }
}
