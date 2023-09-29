using InduMovel.Controllers;
using InduMovel.Models;
using Microsoft.AspNetCore.Identity;
namespace InduMovel.Services
{
    public class UserRoleInicial : IUserRoleInicial
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleInicial(UserManager<UserAccount> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult =
                _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult =
                _roleManager.CreateAsync(role).Result;
            }
        }
        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                UserAccount user = new UserAccount();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.Nome = "Administrador";
                user.Endereco = "Rua 1";
                user.Numero = 1;
                user.Bairro = "Bairro1";
                user.Cidade = "Ata";
                user.Cep = 16200000;
                IdentityResult result = _userManager.CreateAsync(user,

                "Sesisenai#23").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}