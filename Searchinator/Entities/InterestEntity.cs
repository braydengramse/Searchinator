namespace Searchinator.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Interest")]
    public class InterestEntity
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public PersonEntity PersonEntity { get; set; }
    }
}