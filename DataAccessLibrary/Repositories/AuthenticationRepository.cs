//using Dapper;
//using Dapper.Contrib.Extensions;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLibrary.Repositories
//{
//    public class AuthenticationRepository :  IAuthenticationRepository, IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>
//    {
//        private readonly string connectionString;

//        public AuthenticationRepository(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }

//        //create new user
//        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            cancellationToken.ThrowIfCancellationRequested();

//            //insert Super Admin User to DB
//            using (var conn = new SqlConnection(connectionString))
//            {
//                await conn.OpenAsync(); // Ensure the connection is explicitly opened before executing commands
//                user.Id = Guid.NewGuid().ToString(); // Generate a new GUID for the user ID
//                var insertQuery = "INSERT INTO Users (Id, Email, UserName) VALUES (@Id, @Email, @UserName)";
//                conn.Execute(insertQuery, user);
//            }
//            return IdentityResult.Success;
//        }

//        public void SeedRoles()
//        {
//            var superAdminRoleId = "fe53c01d-8f71-409a-b82f-12069f44c776"; //Guid static role id
//            var adminRoleId = "99b2a41b-9708-441e-96b5-da82b4546940"; //Guid static role id
//            var userRoleId = "5b49cb39-7b36-4093-8b45-97a198ae3d33"; //Guid static role id

//            //Seed Roles (User, Admin, Super Admin)
//            var roles = new List<IdentityRole>() {
//                new IdentityRole()
//                {
//                    Name =  "SuperAdmin",
//                    NormalizedName = "SuperAdmin",
//                    Id = superAdminRoleId,
//                    ConcurrencyStamp = superAdminRoleId
//                },
//                new IdentityRole()
//                {
//                    Name =  "Admin",
//                    NormalizedName = "Admin",
//                    Id = adminRoleId,
//                    ConcurrencyStamp = adminRoleId
//                },
//                new IdentityRole()
//                {
//                    Name =  "User",
//                    NormalizedName = "User",
//                    Id = userRoleId,
//                    ConcurrencyStamp = userRoleId
//                }
//            };

//            //insert the roles to DB
//            using (var conn = new SqlConnection(connectionString))
//            {
//                conn.Open(); // Ensure the connection is explicitly opened before executing commands
//                foreach (var role in roles)
//                {
//                    var insertQuery = "INSERT INTO Roles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (@Id, @Name, @NormalizedName, @ConcurrencyStamp)";
//                    conn.Execute(insertQuery, role);
//                }
//            }

//            //Seed Super Admin User (there is only one super admin user)
//            var superAdminId = "780f9e98-f6db-4264-9cbd-daf0b3e7e14f"; //Guid static role id
//            var superAdminUser = new IdentityUser()
//            {
//                UserName = "Doron@m-m-c.co.il",
//                Email = "Doron@m-m-c.co.il",
//                Id = superAdminId
//            };
//            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superAdmin07091981");

//            //insert Super Admin User to DB
//            using (var conn = new SqlConnection(connectionString))
//            {
//                conn.Open(); // Ensure the connection is explicitly opened before executing commands
//                var insertQuery = "INSERT INTO Users (Id, Email, UserName) VALUES (@Id, @Email, @UserName)";
//                conn.Execute(insertQuery, superAdminUser);
//            }

//            //Add Roles to super admin user
//            var superAdminRoles = new List<IdentityUserRole<string>>()
//            {
//                new IdentityUserRole<string>
//                {
//                    RoleId = superAdminRoleId,
//                    UserId = superAdminId
//                },
//                new IdentityUserRole<string>
//                {
//                    RoleId = adminRoleId,
//                    UserId = superAdminId
//                },
//                new IdentityUserRole<string>
//                {
//                    RoleId = userRoleId,
//                    UserId = superAdminId
//                }
//            };

//            //insert super admin user roles to DB
//            using (var conn = new SqlConnection(connectionString))
//            {
//                conn.Open(); // Ensure the connection is explicitly opened before executing commands
//                foreach (var role in superAdminRoles)
//                {
//                    var insertQuery = "INSERT INTO UserRoles (Id, UserId) VALUES (@Id, @UserId)";
//                    conn.Execute(insertQuery, role);
//                }
//            }
//        }
//        public Task<IEnumerable<IdentityUser>> Get()
//        {
//            throw new NotImplementedException();
//        }


//        #region UserStore
//        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }

//        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        #endregion UserStore
//    }
//}
