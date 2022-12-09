using Application.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services;
internal class AccountService : IAccountService
{
    public string ComputeHash(string input)
    {
        SHA512 hashSvc = SHA512.Create();
        byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hash).Replace("-", "");
    }

    public bool ConfirmEmail<T>(string v, Guid guid)
    {
        throw new NotImplementedException();
    }

    public User GetUser(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserbyEmail(string email)
    {
        throw new NotImplementedException();
    }

    public bool IsEmailConfirmed(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(int id, string v1, string v2)
    {
        throw new NotImplementedException();
    }
}
