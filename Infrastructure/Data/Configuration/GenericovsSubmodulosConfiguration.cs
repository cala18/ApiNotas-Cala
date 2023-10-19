using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;
public class GenericovsSubmodulosConfiguration : IEntityTypeConfiguration<GenericovsSubmodulos>
{
    public void Configure(EntityTypeBuilder<GenericovsSubmodulos> builder)
    {
        builder.ToTable("genericovssubmodulo");

        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id);

        builder.Property(x=>x.FechaCreacion).HasColumnType("date");
        builder.Property(x=>x.FechaModificacion).HasColumnType("date");
        builder.HasOne(x=>x.PermisosGenericos).WithMany(x=>x.GenericovsSubmodulos).HasForeignKey(x=>x.IdPermisoGenericoFk);
        builder.HasOne(x=>x.Rols).WithMany(x=>x.GenericovsSubmodulos).HasForeignKey(x=>x.IdRolFk);
        builder.HasOne(x=>x.MaestrosvsSubmodulos).WithMany(x=>x.GenericovsSubmodulos).HasForeignKey(x=>x.IdMaestrovsSubmodulosFk);
    }
}
