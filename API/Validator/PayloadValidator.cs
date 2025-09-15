namespace API.Validator
{
    public class PayloadValidator
    {

        public static bool ValidateObject<T>(T obj, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (obj == null)
            {
                errorMessage = "L'objet fourni est null.";
                return false;
            }

            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);

                if (value is string strValue)
                {
                    if (!ProtectAgainstSQLI(strValue) || !ProtectAgainstXSS(strValue))
                    {
                        errorMessage = $"Valeur invalide détectée dans {prop.Name}.";
                        return false;
                    }

                    
                    if (prop.Name.ToLower().Contains("email") && !BlockTemporaryEmails(strValue))
                    {
                        errorMessage = "Les emails jetables ne sont pas autorisés.";
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Prend une chaïne de caractèreet vérifie qu'elle ne contient pas de termes suscptibles de représenter une menace d'injection SQL.
        /// </summary>
        /// <param name="suspiciousString">La chaîne de caractère à analyser</param>
        /// <returns>false si l'un des caractères dangereux et inclus dans (<paramref name="suspiciousString"/>, true si la chaîne est sécurisée</returns>
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
        /// <returns>False si l'un des caractères dangereux est inclus dans (<paramref name="suspiciousString"/>, true si la chaîne est sécurisée</returns>
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

        /// <summary>
        /// <Prend une chaîne de caractères (<paramref name="suspiciousEmail"/> et vérifie qu'elle ne contient pas de nom de domaine assimilé à une adresse mail jetable.>
        /// </summary>
        /// <param name="suspiciousEmail">La chaîne de caractères à analyser</param>
        /// <returns>False si l'email provient d'un domaine de type mail jetable, true si l'email est valide.</returns>

        public static bool BlockTemporaryEmails(string suspiciousEmail)
        {
            suspiciousEmail = suspiciousEmail.Trim();
            string[] temporatyEmailDomain = new string[]
            {
                "10minutemail.com",
                "20minutemail.com",
                "30minutemail.com",
                "anonymail.net",
                "bouncemail.com",
                "burnermail.com",
                "discard.email",
                "disposableemail.com",
                "dropmail.me",
                "emailondeck.com",
                "fakeinbox.com",
                "getairmail.com",
                "guerrillamail.com",
                "hottempmail.com",
                "incognitomail.com",
                "instant-email.org",
                "mail-temp.com",
                "mail1a.de",
                "mailcatch.com",
                "maildrop.cc",
                "mailinator.com",
                "mailnesia.com",
                "moakt.com",
                "my10minutemail.com",
                "mytrashmail.com",
                "nowmymail.com",
                "sharklasers.com",
                "spam4.me",
                "temp-mail.org",
                "tempmail.com",
                "tempmail.de",
                "tempmail.net",
                "throwawaymail.com",
                "trash-mail.com",
                "trashmail.com",
                "trashmail.de",
                "yopmail.com",
                "yopmail.fr",
                "yopmail.net",
                "zmail.com"
            };

            foreach (var domain in temporatyEmailDomain)
            {
                if (suspiciousEmail.EndsWith(domain))
                {
                    return false;
                }
            }

            return true;
        }

        public static ServiceResponse<T> BuildError<T>(string message, EErrorType errorType = EErrorType.BAD_REQUEST)
        {
            return new ServiceResponse<T>
            {
                Message = message,
                ErrorType = errorType
            };
        }

    }
}
