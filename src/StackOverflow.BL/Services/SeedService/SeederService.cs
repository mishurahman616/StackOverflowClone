using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL.Membership.Entities;

public class SeederService : ISeederService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    public SeederService(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task Seed()
    {

        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }

        var adminUser = await _userManager.FindByEmailAsync("admin@stackoverflow.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin@stackoverflow.com",
                Email = "admin@stackoverflow.com",
                FirstName = "Mishu",
                LastName = "Rahman"
            };
            await _userManager.CreateAsync(adminUser, "123456");
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
