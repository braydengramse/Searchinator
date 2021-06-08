namespace Searchinator.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Person")]
    public class PersonEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }
    }
}