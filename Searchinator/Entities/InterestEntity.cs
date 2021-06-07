namespace Searchinator.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class InterestEntity
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public PersonEntity PersonEntity { get; set; }
    }
}