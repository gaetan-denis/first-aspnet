using System.ComponentModel.DataAnnotations.Schema;

//commentaire de test

namespace firstAspnet.Domain.Entities{
    public class User{

        [Column("USER_id")]
        public int Id {get;set;}
        [Column("USER_username")]
        public required string Username {get;set;}
        [Column("USER_password")]
        public required string Password {get;set;}
        [Column("USER_isAdmin")]
        public bool IsAdmin {get;set;}
        [Column("USER_createdAt")]
        public DateTime CreatedAt {get;set;}
        [Column("USER_UpdatedAt")]
        public DateTime UpdatedAt {get;set;}
    }
}

