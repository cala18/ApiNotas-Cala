using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;
public class FormatoConfiguration : IEntityTypeConfiguration<Formato>
{
    public void Configure(EntityTypeBuilder<Formato> builder)
    {
        builder.ToTable("formato");
        
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id);

        builder.Property(x=>x.NombreFormato).IsRequired().HasMaxLength(50);
        builder.Property(x=>x.FechaCreacion).HasColumnType("date");
        builder.Property(x => x.FechaModificacion).HasColumnType("date");
        
    }
}
