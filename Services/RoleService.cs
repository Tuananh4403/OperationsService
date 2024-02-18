namespace OperationsService.Services;

using AutoMapper;
using OperationsService.Entities;
using OperationsService.Helpers;
using OperationsService.Models.Roles;


public interface IRoleService
{
    IEnumerable<Role> GetAll();
    Role GetByTile(string RoleTilte);
    void Create(CreateRole model);
    void Delete(int id);
}

public class RoleService : IRoleService
{
    private DataContext _context;

    private readonly IMapper _mapper;

    public RoleService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Role> GetAll()
    {
        return _context.Role;
    }

    public Role GetByTile(string RoleTilte)
    {
        return GetByTile(RoleTilte);
    }

    public void Create(CreateRole model)
    {
        if (_context.Role.Any(x => x.Title == model.Title))
            throw new AppException("Role Title'" + model.Title + "' is already taken");

        // map model to new user object
        var role = _mapper.Map<Role>(model);
        _context.Role.Add(role);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

}