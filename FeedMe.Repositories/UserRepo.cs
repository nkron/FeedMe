﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FeedMe.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FeedMe.Repositories
{ 
    public class UserRepo:IUserStore<User>, IUserPasswordStore<User>
    {
        public UserRepo()
        {
            
        }
        public IEnumerable<User> GetUsers()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<User>("select * from Users");
                return output;
            }
        }

        //public User GetByID(int ID)
        //{

        //    using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
        //    {
        //        User output = connection.Query<User>("dbo.GetUser @UserID", new { UserID = ID }).First();
        //        return output;
        //    }
        //}
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserID.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                await connection.OpenAsync(cancellationToken);
                user.UserID = await connection.QuerySingleAsync<int>($@"INSERT INTO [Users] ([Username], [NormalizedUserName],
                    [PasswordHash])
                    VALUES (@{nameof(User.Username)}, @{nameof(User.NormalizedUserName)},
                     @{nameof(User.PasswordHash)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User u)
        {
            //cancellationToken.ThrowIfCancellationRequested();
  
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", u.UserID, DbType.Int32);
                param.Add("@FirstName", u.FirstName, DbType.String);
                param.Add("@LastName", u.LastName, DbType.String);    
                param.Add("@TargetCals", u.TargetCals, DbType.Int32);
                param.Add("@TargetMacC", u.TargetMacC, DbType.Int32);
                param.Add("@TargetMacP", u.TargetMacP, DbType.Int32);
                param.Add("@TargetMacF", u.TargetMacF, DbType.Int32);

                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "dbo.UpdateUser",
                    param,
                    commandType: CommandType.StoredProcedure);
            }

            return IdentityResult.Success;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string ID, CancellationToken cancellationToken)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>("dbo.GetUser @UserID", new { UserID = ID });

            }
        }

        public async Task<User> FindByNameAsync(string Username, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [Users]
                    WHERE [Username] = @{nameof(Username)}", new { Username });
            }
        }

        public void Dispose()
        {
            //Nothing to dispose
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User u, CancellationToken cancellationToken)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", u.UserID, DbType.Int32);
                param.Add("@FirstName", u.FirstName, DbType.String);
                param.Add("@LastName", u.LastName, DbType.String);
                param.Add("@TargetCals", u.TargetCals, DbType.Int32);
                param.Add("@TargetMacC", u.TargetMacC, DbType.Int32);
                param.Add("@TargetMacP", u.TargetMacP, DbType.Int32);
                param.Add("@TargetMacF", u.TargetMacF, DbType.Int32);

                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "dbo.UpdateUser",
                    param,
                    commandType: CommandType.StoredProcedure);
            }
            return IdentityResult.Success;
        }
    }
}
