﻿using DataAccess.Entities.Users;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;


        public UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<IEnumerable<string>> GetUserRolesByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) {
                throw new ArgumentException("User not found");
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) {
                throw new ArgumentException("User not found");
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null) {
                throw new ArgumentException("Role not found");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded) {
                throw new ArgumentException("Role not removed");
            }

            return true;
        }

        public async Task<bool> SetRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) {
                throw new ArgumentException("User not found");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) {
                throw new ArgumentException("Role not found");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded) {
                throw new ArgumentException("Role not added");
            }

            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var userToUpdate = await _userManager.FindByEmailAsync(user.Email);
            var result = await _userManager.UpdateAsync(userToUpdate);
            return result.Succeeded;
        }
    }
}
