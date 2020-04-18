using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Controllers
{
    public class RolesController : Controller
    {
        private readonly AccountsContext _context;
        private UserManager<IdentityUser> _userManager;

        public RolesController(AccountsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                _context.Roles.Add
                (
                    new IdentityRole
                    {
                        Name = collection["RoleName"],
                        NormalizedName = collection["RoleName"].ToString().Normalize()
                    }
                );
                await _context.SaveChangesAsync();
                ViewBag.ResultMessage = "Role Created";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync
                (r => r.Name.ToUpper().Equals(roleName.ToUpper()));
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync
                (r => r.Name.ToUpper().Equals(roleName.ToUpper()));
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManageUserRoles()
        {
            var roles = await _context.Roles.OrderBy(r => r.Name).ToListAsync();
            var list = roles.Select
            (
                r =>
                    new SelectListItem
                    {
                        Value = r.Name, Text = r.Name
                    }
            ).ToList();

            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAddToUser(string username, string rolename)
        {
            var user = await _context.Users.FirstOrDefaultAsync
                (u => u.UserName.ToUpper().Equals(username.ToUpper()));

            await _userManager.AddToRoleAsync(user, rolename);
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == rolename);

            ViewBag.ResultMessage = "Added to role.";
            var list = await _context.Roles.OrderBy(r => r.Name).ToListAsync();
            ViewBag.Roles = list.Select
            (
                r => new SelectListItem
                {
                    Value = r.Name, Text = r.Name
                }
            );

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetRoles(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync
                (u => u.UserName.ToUpper().Equals(username.ToUpper()));

            ViewBag.UserRoles = await _userManager.GetRolesAsync(user);

            var list = await _context.Roles.OrderBy(r => r.Name).ToListAsync();
            ViewBag.Roles = list.Select
            (
                r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }
            );

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(string username, string rolename)
        {
            var user = await _context.Users.FirstOrDefaultAsync
                (u => u.UserName.ToUpper().Equals(username.ToUpper()));

            if ((await _userManager.IsInRoleAsync(user, rolename)) || user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, rolename);
                ViewBag.ResultMessage = "Removed from role.";
            }
            else
            {
                ViewBag.ResultMessage = "Role not assigned to selected user.";
            }

            var list = await _context.Roles.OrderBy(r => r.Name).ToListAsync();
            ViewBag.Roles = list.Select
            (
                r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }
            );

            return View("ManageUserRoles");
        }
    }
}