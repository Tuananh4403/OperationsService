namespace OperationsService.Services;

using AutoMapper;
using BCrypt.Net;
using OperationsService.Authorization;
using OperationsService.Entities;
using OperationsService.Helpers;
using OperationsService.Models.Users;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Register(RegisterRequest model);
    void UpdateRole(int id, UpdateRequest model);
    void Delete(int id);
}

public class UserService : IUserService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

        // validate
        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
            throw new AppException("Username or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }

    public IEnumerable<User> GetAll()
    {
        
        return _context.Users;
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public void Register(RegisterRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Email == model.Email))
            throw new AppException("Email '" + model.Email + "' is already taken");
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.HashPassword(model.Password);
        _context.Users.Add(user);
        // user.UserRoles.Add(new UserRole { RoleId = role.RoleId });
        _context.SaveChanges();
    }

    public void UpdateRole(int id, UpdateRequest model)
    {
        var user = getUser(id);

        // validate
        // if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
        //     throw new AppException("Username '" + model.Email + "' is already taken");

        // hash password if it was entered
        // if (!string.IsNullOrEmpty(model.Password))
        //     user.PasswordHash = BCrypt.HashPassword(model.Password);
        var existingRole = _context.Role.FirstOrDefault(r => r.Name == model.Role);
        if (existingRole != null)
        {
            // If the role exists, associate it with the user
            user.UserRoles.Add(new UserRole { RoleId = existingRole.RoleId });
        }      
        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
    
    // helper methods

    private User getUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}