namespace Searchinator.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PersonEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }
    }
}