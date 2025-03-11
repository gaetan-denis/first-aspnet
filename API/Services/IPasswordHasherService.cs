using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPasswordHasherService
    {   
        
        string HashPassword(string password, out string salt); 
        string  VerifyPassword (string password, string salt, string storedHash);
    }
}