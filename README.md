# ApiNotas-Cala
# README NOTI-APP Brayan camilo cala lopez.

## Creacion del EF Code First SQL.

- ## [Creacion del proyecto.](#Creacion-de-Proyecto)
  

  1. [Creacion de la solucion del proyecto](#Creacion-solucion-de-proyecto)

  2. [Creacion del proyecto Webapi](#Creacion-proyecto-WebApi)

  3. [Creacion del proyecto Classlib](#Creacion-de-proyectos-Classlib)

  4. [Agregar proyectos a la solucion](#Agregar-proyectos-a-la-solucion.)

  5.[ Agregar la relacion entre proyectos](#Agregar-la-solucion-entre-proyectos)

- ### Instalacion de paquetes.
  

  1. [Instalacion de paquetes en proyecto webapi](#Proyecto-webapi)

  2.[ Instalacion de paquetes en proyecto infraestructura](#Proyecto-Infrastructure)

### [CORE.](#CORE)

- #### Entities.
  
  - [Ejemplo Entity](#Ejemplo-Entity)
    
- #### Interfaces
  
  - [Ejemplo Interface](#Ejemplo-Interface)
    
  - [Ejemplo IGenericRepository](#Ejemplo-IGenericRepository)
    
  - [Ejemplo IUnitOfWork](#IUnitOfWork)
    

### INFRASTRUCTURE.

- #### Data
  
  - Configuration
    
    - [Ejemplo configuration](#Ejemplo-Configuracion)
      
  - [Ejemplo Entidad de Contexto(DbContext)](#Ejemplo-Entidad-de-Contexto)
    
- #### Repositories
  
  - [Ejemplo Repository](#Ejemplo-Repository)
    
  - [Ejemplo GenericRepository](#Ejemplo-GenericRepository)
    
- #### UnitOfWork
  
  - [Ejemplo UnitOfWork](#Ejemplo-UnitOfWork)
    

### [API.](#API)

- #### Controllers
  
  - [Ejemplo controllers](#Ejemplo-Controllers)
    
- #### DTOs
  
  - [Ejemplo Dto](#Ejemplo-Dto)
    
- #### Extensions
  
  - [ApplicationServiceExtension](#ApplicationServiceExtension)
    
- #### Helpers
  
  - [Pager](#Pager)
    
  - [Params](#Params)
    
- #### Profiles
  
  - [MappingProfile](#MappingProfile)
    
- #### Conexion a la base de datos
  
  - [Ejemplo conexión Db](#Conexion-base-de-datos)
    
- #### [Program.cs](#Program)
  

### Creacion de Proyecto

##### Creacion solucion de proyecto

```dotnet
 dotnet new sln
```

##### Creacion proyecto WebApi

```dotnet
dotnet new webapi -o FolderDestino
```

##### Creacion de proyectos Classlib

```dotnet
dotnet new classlib -o Core
dotnet new classlib -o Infrastructure
```

##### Agregar proyectos a la solucion.

```dotnet
dotnet sln add ApiAnimals
dotnet sln add Core
dotnet sln add Infrastructure
```

##### Agregar la solucion entre proyectos

```dotnet
dotnet add reference ..\Infrastructure
dotnet add reference ..\Core
```

### Instalacion de paquetes

##### Proyecto webapi

```dotnet
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.11
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.11
dotnet add package Microsoft.Extensions.DependencyInjection --version 7.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.32.3
dotnet add package Serilog.AspNetCore --version 7.0.0
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
```

##### Proyecto Infrastructure

```dotnet
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.11
dotnet add package CsvHelper --version 30.0.1
```

### CORE

##### Ejemplo Entity

```dotnet
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class MaestrosvsSubmodulos : BaseEntity
{   
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public int IdModulosMaestroFk { get; set; }
    public ModulosMaestro ModulosMaestros { get; set; }
    public int IdSubModulosFk { get; set; }
    public SubModulos SubModulos { get; set; }
    public ICollection<GenericovsSubmodulos> GenericovsSubmodulos { get; set; }
}

```

##### Ejemplo Interface

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces;
public interface IMaestrovsSubmodulos : IGenericRepository<MaestrosvsSubmodulos>
{
    
}

```

##### Ejemplo IGenericRepository

```dotnet
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int Id);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    // Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}
```

##### IUnitOfWork

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;
public interface IUnitOfWork 
{
    IMaestrovsSubmodulos MaestrovsSubmodulos {get;}

    Task<int> SaveAsync();
}
```

### INFRASTRUCTURE

##### Ejemplo Configuracion

```dotnet
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

```

##### Ejemplo Entidad de Contexto

```dotnet

using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class NotiAppContext : DbContext
{
    public NotiAppContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<MaestrosvsSubmodulos> MaestrosvsSubmodulos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}



```

##### Ejemplo Repository

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MaestrovsSubmodulosRepository : GenericRepository<MaestrosvsSubmodulos>,IMaestrovsSubmodulos
    {
        private readonly NotiAppContext _context;

        public MaestrovsSubmodulosRepository(NotiAppContext context) : base(context)
        {
            _context = context;
        }
    }
}
```

##### Ejemplo GenericRepository

```dotnet
using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly NotiAppContext _context;

    public GenericRepository(NotiAppContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
        // return (IEnumerable<T>) await _context.Entities.FromSqlRaw("SELECT * FROM entity").ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string _search
    )
    {
        var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context
            .Set<T>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
```

##### Ejemplo UnitOfWork

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{

    private IAuditoria _auditorias;
    private IBlockchain _blockchains;
    private IEstadoNotificacion _estadoNotificaciones;
    private IFormato _formatos;
    private IGenericovsSubmodulos _genericovsSubmodulos;
    private IHiloRespuestaNotificacion _hiloRespuestaNotificaciones;
    private IMaestrovsSubmodulos _maestrovsSubmodulos;
    private IModuloNotificacion _moduloNotificaciones;
    private IModulosMaestro _modulosMaestros;
    private IPermisoGenericos _permisoGenericos;
    private IRadicados _radicados;
    private IRol _rols;
    private IRolvsMaestro _rolvsMaestros;
    private ISubModulos _subModulos;
    private ITipoNotificacion _tipoNotificaciones;
    private ITipoRequerimientos _tipoRequerimientos;

    private readonly NotiAppContext _context;
    public UnitOfWork(NotiAppContext context)
    {
        _context = context;
    }

    public IMaestrovsSubmodulos MaestrovsSubmodulos
    {
        get
        {
            if (_maestrovsSubmodulos == null)
            {
                _maestrovsSubmodulos = new MaestrovsSubmodulosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _maestrovsSubmodulos;
        }
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

}

```

### API

##### Ejemplo Controllers

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class MaestrovsSubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaestrovsSubmodulosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MaestrosvsSubmodulosDto>>> Get()
    {
        var maestrovssubmodulos = await _unitOfWork.MaestrovsSubmodulos.GetAllAsync();
        return _mapper.Map<List<MaestrosvsSubmodulosDto>>(maestrovssubmodulos);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaestrosvsSubmodulosDto>> Get(int id)
    {
        var maestrovssub = await _unitOfWork.MaestrovsSubmodulos.GetByIdAsync(id);
        if(maestrovssub == null)
        {
            return NotFound();
        }
        return _mapper.Map<MaestrosvsSubmodulosDto>(maestrovssub);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaestrosvsSubmodulosDto>> Post([FromBody] MaestrosvsSubmodulosDto maestrovssubDto)
    {
        var maestrovssub = _mapper.Map<MaestrosvsSubmodulos>(maestrovssubDto);

        if(maestrovssub == null)
            return BadRequest();
        if (maestrovssubDto.FechaCreacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (maestrovssub.FechaModificacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.MaestrovsSubmodulos.Add(maestrovssub);
        await _unitOfWork.SaveAsync();
        maestrovssubDto.Id = maestrovssub.Id;
        return CreatedAtAction(nameof(Post),new {id = maestrovssub.Id},maestrovssubDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaestrosvsSubmodulosDto>> Put(int id, [FromBody] MaestrosvsSubmodulosDto maestrovssubDto)
    {
        if(maestrovssubDto.Id == 0)
        {
            maestrovssubDto.Id = id;
        }
        if(maestrovssubDto.Id != id)
        {
            return NotFound();
        }
        var maestrovssub = _mapper.Map<MaestrosvsSubmodulos>(maestrovssubDto);
        if(maestrovssub==null)
            return BadRequest();
        if(maestrovssub.FechaCreacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(maestrovssubDto.FechaModificacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.MaestrovsSubmodulos.Update(maestrovssub);
        await _unitOfWork.SaveAsync();
        return maestrovssubDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var maestrovssub = await _unitOfWork.MaestrovsSubmodulos.GetByIdAsync(id);
        if(maestrovssub == null)
            return NotFound();
        _unitOfWork.MaestrovsSubmodulos.Remove(maestrovssub);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}

```

##### Ejemplo Dto

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class MaestrosvsSubmodulosDto
{
    public int Id { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
    public int IdModulosMaestroFk { get; set; }
    public int IdSubModulosFk { get; set; }
}

```

##### ApplicationServiceExtension

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.UnitOfWork;

namespace API.Extensions;
public static class ApplicationServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin() // WithOrigins("https://domain.com")
            .AllowAnyMethod() // WithMethods("GET", "POST")
            .AllowAnyHeader(); // WithHeaders("accept", "content-type")
        });
    }); // Remember to put 'static' on the class and to add builder.Services.ConfigureCors(); and app.UseCors("CorsPolicy"); to Program.cs
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    } // Remember to add builder.Services.AddApplicationServices(); to Program.cs
    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",  // Si quiere usar todos ponga *
                    Period = "NumberSecss", // Periodo de tiempo para hacer peticiones
                    Limit = 10        // Numero de peticiones durante el periodo de tiempo
                }
            };
        });
    } // Remember adding builder.Services.ConfigureRateLimiting(); and builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); and app.UseRateLimiting(); to Program.cs
    
}

```

##### Pager

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Helpers;

public class Pager<T> where T : class
    {
    public string Search { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Registers { get; private set; }

    public Pager()
    {
    }

    public Pager(List<T> registers, int total, int pageIndex, int pageSize, string search)
    {
        Registers = registers;
        Total = total;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Search = search;
    }

    public int TotalPages
    {
        get { return (int)Math.Ceiling(Total / (double)PageSize); }
        set { this.TotalPages = value; }
    }

    public bool HasPreviousPage
    {
        get { return (PageIndex > 1); }
        set { this.HasPreviousPage = value; }
    }

    public bool HasNextPage
    {
        get { return (PageIndex < TotalPages); }
        set { this.HasNextPage = value; }
    }
}

```

##### Params

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Helpers;

public class Params
{
    private int _pageSize = 5;
    private const int MaxPageSize = 50;
    private int _pageIndex = 1;
    private string _search;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value <= 0) ? 1 : value;
    }

    public string Search
    {
        get => _search;
        set => _search = (!String.IsNullOrEmpty(value)) ? value.ToLower() : "";
    }
}
```

##### MappingProfile

```dotnet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MaestrosvsSubmodulos,MaestrosvsSubmodulosDto>().ReverseMap();
    }
}

```

##### Conexion base de datos

```dotnet
"ConnectionStrings": {
      "MySqlConex": "server=localhost;user=root;password=6366740;database=notiappdb;"
  }
```

##### Program

```dotnet
using System.Reflection;
using API.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.ConfigureRateLimiting();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NotiAppContext>(optionsBuilder =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySqlConex");
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPoliccy");
app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

```