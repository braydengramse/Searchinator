namespace Searchinator.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public ICollection<Interest> Interests { get; set; } = new List<Interest>();
    }
}