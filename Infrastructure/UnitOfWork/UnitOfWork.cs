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
    public IAuditoria Auditorias
    {
        get
        {
            if (_auditorias == null)
            {
                _auditorias = new AuditoriaRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _auditorias;
        }
    }
    public IBlockchain Blockchains
    {
        get
        {
            if (_blockchains == null)
            {
                _blockchains = new BlockchainRespository(_context); // Remember putting the base in the repository of this entity
            }
            return _blockchains;
        }
    }
    public IEstadoNotificacion EstadoNotificaciones
    {
        get
        {
            if (_estadoNotificaciones == null)
            {
                _estadoNotificaciones = new EstadoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _estadoNotificaciones;
        }
    }
    public IFormato Formatos
    {
        get
        {
            if (_formatos == null)
            {
                _formatos = new FormatoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _formatos;
        }
    }
    public IGenericovsSubmodulos GenericovsSubmodulos
    {
        get
        {
            if (_genericovsSubmodulos == null)
            {
                _genericovsSubmodulos = new GenericovsSubmodulosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _genericovsSubmodulos;
        }
    }
    public IHiloRespuestaNotificacion HiloRespuestaNotificaciones
    {
        get
        {
            if (_hiloRespuestaNotificaciones == null)
            {
                _hiloRespuestaNotificaciones = new HiloRespuestaNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _hiloRespuestaNotificaciones;
        }
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
    public IModuloNotificacion ModuloNotificaciones
    {
        get
        {
            if (_moduloNotificaciones == null)
            {
                _moduloNotificaciones = new ModuloNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _moduloNotificaciones;
        }
    }
    public IModulosMaestro ModulosMaestros
    {
        get
        {
            if (_modulosMaestros == null)
            {
                _modulosMaestros = new ModulosMaestroRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _modulosMaestros;
        }
    }
    public IPermisoGenericos PermisoGenericos
    {
        get
        {
            if (_permisoGenericos == null)
            {
                _permisoGenericos = new PermisosGenericosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _permisoGenericos;
        }
    }
    public IRadicados Radicados
    {
        get
        {
            if (_radicados == null)
            {
                _radicados = new RadicadosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _radicados;
        }
    }
    public IRol Rols
    {
        get
        {
            if (_rols == null)
            {
                _rols = new RolRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _rols;
        }
    }
    public IRolvsMaestro RolvsMaestros
    {
        get
        {
            if (_rolvsMaestros == null)
            {
                _rolvsMaestros = new RolvsMaestroRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _rolvsMaestros;
        }
    }
    public ISubModulos SubModulos
    {
        get
        {
            if (_subModulos == null)
            {
                _subModulos = new SubModulosRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _subModulos;
        }
    }
    public ITipoNotificacion TipoNotificaciones
    {
        get
        {
            if (_tipoNotificaciones == null)
            {
                _tipoNotificaciones = new TipoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _tipoNotificaciones;
        }
    }
    public ITipoRequerimientos TipoRequerimientos
    {
        get
        {
            if (_tipoRequerimientos == null)
            {
                _tipoRequerimientos = new TipoRequerimientoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _tipoRequerimientos;
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
