using Microsoft.EntityFrameworkCore;
using DemoApi.Data;
using DemoApi.Dtos;


namespace DemoApi.Services;

public interface IUserService
{
    Task<string> GetUserNameAsync();
    Task CreateUserAsync(CreateUserRequest request);
    Task<PagedResult<UserResponse>> GetUsersAsync(
    string? name,
    int? minAge,
    string sortBy,
    string sortDir,
    int page,
    int pageSize);


}

public class UserService : IUserService
{
    private readonly DemoApi.Data.AppDbContext _db;
    public UserService(DemoApi.Data.AppDbContext db)
    {
        _db = db;
    }
    public async Task<string> GetUserNameAsync()
    {
        var user = await _db.Users.FirstOrDefaultAsync();
        return user?.Name ?? "No user";
    }

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Age = request.Age
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task<PagedResult<UserResponse>> GetUsersAsync(
        string? name,
        int? minAge,
        string sortBy,
        string sortDir,
        int page,
        int pageSize)
    {
        IQueryable<User> query = _db.Users;

        // -------- FILTERING --------
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(u => u.Name.Contains(name));
        }

        if (minAge.HasValue)
        {
            query = query.Where(u => u.Age >= minAge.Value);
        }

        // -------- SORTING (SAFE) --------
        query = (sortBy.ToLower(), sortDir.ToLower()) switch
        {
            ("name", "desc") => query.OrderByDescending(u => u.Name),
            ("name", "asc") => query.OrderBy(u => u.Name),

            ("age", "desc") => query.OrderByDescending(u => u.Age),
            ("age", "asc") => query.OrderBy(u => u.Age),

            _ => query.OrderBy(u => u.Id) // default, ALWAYS
        };

        // -------- COUNT BEFORE PAGINATION --------
        var totalCount = await query.CountAsync();

        // -------- PAGINATION + PROJECTION --------
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserResponse(u.Name, u.Age))
            .ToListAsync();

        return new PagedResult<UserResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

}
