namespace API.Enums
{
    public enum EErrorType : int
    {
        // Description des réponses https : https://developer.mozilla.org/fr/docs/Web/HTTP/Reference/Status

        /// <summary>
        /// 404 Not Found : Le serveur n'a pas trouvé la ressource demandée.
        /// </summary>
        NOT_FOUND,

        /// <summary>
        /// 200 Ok : lya requête a réussi.
        /// </summary>
        SUCCESS,

        /// <summary>
        /// 201 Created : le code de statut HTTP 201 Created indique que la requête a réussi et qu'une ressource a été créée en conséquence.
        /// </summary>
        CREATED,

        /// <summary>
        /// 204 No Content : il n'y a pas de contenu à envoyer pour cette requête
        /// </summary>
        NOT_CONTENT,

        /// <summary>
        /// 401 Unauthorized : le code de statut de réponse HTTP 401 Unauthorized indique que la requête n'a pas été effectuée, car il manque des informations d'authentification valides pour la ressource visée
        /// </summary>
        UNAUTHORIZED,
        
        /// <summary>
        /// Cette réponse indique que le serveur n'a pas pu comprendre la requête à cause d'une syntaxe invalide.
        /// </summary>
        BAD_REQUEST,
        
        /// <summary>
        /// Indique que le serveur a compris le type de contenu de la requête et que la syntaxe de la requête est correcte mais 
        /// que le serveur n'a pas été en mesure de réaliser les instructions demandées.
        /// </summary>
        VALIDATION_ERROR,          
        
        /// <summary>
        ///  409 Conflict : cette réponse est envoyée quand une requête entre en conflit avec l'état actuel du serveur.
        /// </summary>               
        CONFLICT,   
        
        /// <summary>
        /// 500 Internal Server Error : le serveur a rencontré une situation qu'il ne sait pas traiter.
        /// </summary>
        INTERNAL_SERVER_ERROR, 
       
        /// <summary>
        /// 503 Service Unavailable : le serveur n'est pas prêt pour traiter la requête.
        /// </summary>
        SERVICE_UNAVAILABLE, 
        
        /// <summary>
        /// 405 Method Not Allowed : la méthode de la requête est connue du serveur mais n'est pas prise en charge pour la ressource cible. 
        /// </summary>
        METHOD_NOT_ALLOWED,  

        /// <summary>
        /// 422 Unprocessable Entity : Le code de statut de réponse HTTP 422 Unprocessable Entity indique que le serveur a compris le type de contenu de la requête et que la syntaxe de la requête est correcte mais que le serveur n'a pas été en mesure de réaliser les instructions demandées.
        /// </summary>
        UNPROCESSABLE_ENTITY
    }
}