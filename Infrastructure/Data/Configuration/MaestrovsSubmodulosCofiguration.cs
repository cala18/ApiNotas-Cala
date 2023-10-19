using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;
public class MaestrovsSubmodulosCofiguration : IEntityTypeConfiguration<MaestrosvsSubmodulos>
{
    public void Configure(EntityTypeBuilder<MaestrosvsSubmodulos> builder)
    {
        builder.ToTable("maestrovssubmodulos");
        
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id);

        builder.Property(x=>x.FechaCreacion).HasColumnType("date");
        builder.Property(x=>x.FechaModificacion).HasColumnType("date");

        builder.HasOne(x=>x.ModulosMaestros).WithMany(x=>x.MaestrosvsSubmodulos).HasForeignKey(x=>x.IdModulosMaestroFk);
        builder.HasOne(x=>x.SubModulos).WithMany(x=>x.MaestrosvsSubmodulos).HasForeignKey(x=>x.IdSubModulosFk);

    }
}
