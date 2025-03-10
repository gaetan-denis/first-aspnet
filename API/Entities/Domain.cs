using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities{
    public class Domain{
        [Column("DOMA_id")]
        public int Id {get;set;}
        [Column("DOMA_name")]
        public required string Name {get;set;}

        // Définit la relation avec les domaines (Un post peut avoir plusieurs domaines).
        public ICollection<PostDomain>Domains {get;set;} = new List<PostDomain>(); //Liste des domaines associés
    }
}