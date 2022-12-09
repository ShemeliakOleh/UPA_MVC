using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
public interface IAccountService
{
    Task<Domain.Models.User> GetUserbyEmail(string email);

    string ComputeHash(string input);
    bool IsEmailConfirmed(int id);
    bool ConfirmEmail<T>(string v, Guid guid);
    User GetUser(Guid guid);
    void UpdateUser(int id, string v1, string v2);
}
