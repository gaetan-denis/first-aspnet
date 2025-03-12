using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPasswordManager
    {   
        
        string HashPassword(string password, out string salt); 
        bool  VerifyPassword (string password, string salt, string storedHash);
    }
}