using System.ComponentModel.DataAnnotations.Schema;

namespace firstAspnet.Domain.Entities{
    public class Domain{
        [Column("DOMA_id")]
        public int Id {get;set;}
        [Column("DOMA_name")]
        public required string Name {get;set;}
    }
}