using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;
public class RolvsMaestroConfiguration : IEntityTypeConfiguration<RolvsMaestro>
{
    public void Configure(EntityTypeBuilder<RolvsMaestro> builder)
    {
        builder.ToTable("rolvsmaestro");

        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id);

        builder.Property(x=>x.FechaCreacion).HasColumnType("date");
        builder.Property(x=>x.FechaModificacion).HasColumnType("date");
        
        builder.HasOne(x=>x.Rols).WithMany(x=>x.RolvsMaestros).HasForeignKey(x=>x.IdRolFk);
        builder.HasOne(x=>x.ModulosMaestros).WithMany(x=>x.RolvsMaestros).HasForeignKey(x=>x.IdModulosMaestroFk);
    }
}
