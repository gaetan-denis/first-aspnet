namespace API.Validator
{
    public class PayloadValidator
    {
        /// <summary>
        /// Prend une chaïne de caractèreet vérifie qu'elle ne contient pas de termes suscptibles de représenter une menace d'injection SQL.
        /// </summary>
        /// <param name="suspiciousString">La chaîne de caractère à analyser</param>
        /// <returns>false si l'un des caractères dangereux et inclus dans `suspiciousString`, true si la chaîne est sécurisée</returns>
        public static bool ProtectAgainstSQLI(string suspiciousString)
        {
            suspiciousString = suspiciousString.Trim();
            string[] dangerousPatterns = new string[]
            {
                "SELECT", "INSERT", "DELETE", "UPDATE", "DROP", "TRUNCATE", "EXEC", "UNION",
                "--", ";", "'", "\"", "/*", "*/", "xp_"
            };
            foreach (var pattern in dangerousPatterns)
            {
                if (suspiciousString.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// Prend une chaîne de caractères et vérifie qu'elle ne contient pas de termes susceptibles de représenter une menace d'attaque XSS.
        /// </summary>
        /// <param name="suspiciousString">La chaîne de caractères à analyser</param>
        /// <returns>false si l'un des caractères dangereux est inclus dans `suspiciousString`, true si la chaîne est sécurisée</returns>
        public static bool ProtectAgainstXSS(string suspiciousString)
        {
            if (string.IsNullOrWhiteSpace(suspiciousString))
            {
                return false;
            }

            suspiciousString = suspiciousString.Trim();

            string[] dangerousPatterns = new string[]
            {
            "<script", "</script>", "javascript:", "onload=", "onerror=", "alert(", "document.cookie",
            "eval(", "window.location", "window.open", "<iframe", "</iframe>", "<object", "</object>",
            "<embed", "</embed>", "<form", "</form>", "<input", "</input>", "<style", "</style>",
            "<svg", "</svg>", "<img", "onmouseover=", "onfocus="
            };

            foreach (var pattern in dangerousPatterns)
            {
                if (suspiciousString.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
