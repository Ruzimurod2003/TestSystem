using Models;
using SqlServer_Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer_Database.Repository
{
    public interface ISqlOperation
    {
        bool UpdateRoleInUser(Guid userId, Guid roleId);
        Guid GetRoleId(string role);
        Role GetRole(Guid id);
        bool AddUser(User user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(Guid id, User new_user);
        List<User> GetUsers();
        Task<List<User>> GetAllUsers();
        bool FindByEmail(string email);
        User? GetUserByEmail(string email);
        User GetUserById(Guid id);
        List<string> GetDepartmentNames();
    }
    public class SqlOperation : ISqlOperation
    {
        private readonly ApplicationDbContext? dbContext;

        public SqlOperation(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public bool AddUser(User user)
        {
            bool result = false;
            try
            {
                dbContext?.Users?.Add(user);
                dbContext?.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public Task<bool> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool FindByEmail(string email)
        {
            //agar shunday user bo'lsa true qaytarsin
            bool result = false;
            try
            {
                var user = dbContext?.Users?.FirstOrDefault(dbContext => dbContext.Email == email);
                if (user is not null)
                {
                    result = true;
                }
            }
            catch
            {
                result = true;
            }
            return result;
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Role GetRole(Guid id)
        {
            Role? role = dbContext?.Roles?.FirstOrDefault(i => i.RoleId == id);
            return role ?? new Role { };
        }

        public List<User> GetUsers()
        {
            List<User>? users = dbContext?.Users?.ToList();
            return users ?? new List<User>();
        }

        public User GetUserById(Guid id)
        {
            var user = dbContext?.Users?.FirstOrDefault(i => i.Id == id);
            return user ?? new User { Id = id };
        }

        public Task<bool> UpdateUser(Guid id, User new_user)
        {
            throw new NotImplementedException();
        }

        public Guid GetRoleId(string role)
        {
            Guid? result = dbContext?.Roles?.FirstOrDefault(i => i.RoleName == role)?.RoleId;
            return result ?? new Guid();
        }

        public bool UpdateRoleInUser(Guid userId, Guid roleId)
        {
            bool result = false;
            try
            {
                User user = dbContext?.Users?.FirstOrDefault(i => i.Id == userId) ?? new User { };
                user.RoleId = roleId;
                dbContext?.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public List<string> GetDepartmentNames()
        {
            List<string> result = dbContext?.Departments?.ToList().Select(item => item.Name ?? "").ToList() ?? new List<string> { };
            return result;
        }

        public User? GetUserByEmail(string email)
        {
            User? user = dbContext?.Users?.FirstOrDefault(i => i.Email == email);
            return user;
        }
    }
}
