using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    public class PasswordManager : IPasswordManager
    {
        // Hash le mot de passe en clair de l'utiliszteur 
        public string HashPassword(string password, out string salt)
        {
            // génère un sel
            salt = GenerateSalt();
            //Retourne la combinaison du sel et du mot de passe
            return ComputedHash(password, salt);
        }
        // On vérifie que le mot de passe stocké en base de donnée soit identique à celui entré par l'utilisateur
        public bool VerifyPassword(string password, string salt, string storedHash)
        {
            // On applique le hash sur la combinaison du password et du sel
            string hashedInput = ComputedHash(password, salt);
            // On retourne un booléen; true si les deux mots de passe correspondent
            return storedHash == hashedInput;
        }
        // Génère le sel
        private string GenerateSalt()
        {
            //Crée un tableau de bytes et lui attribue 32 octets aléatoires
            byte[] saltBytes = RandomNumberGenerator.GetBytes(32);
            //Converti le tableau en Base64 pour le rendre lisible
            return Convert.ToBase64String(saltBytes);
        }
        // Combine le mot de passe et le sel en une seule chaîne de caractères
        private string ComputedHash(string password, string salt)
        {
            // Crée un objet SHA256
            using var sha256 = SHA256.Create();
            // Combine le mot de passe et le sel, puis les convertit en tableau d'octets
            byte[] saltedPassword = Encoding.UTF8.GetBytes(password + salt);
            // applique le hachage SHA256
            byte[] hashBytes = sha256.ComputeHash(saltedPassword);
            // Returne le résultat transformer en Base64
            return Convert.ToBase64String(hashBytes);
        }
    }

}
