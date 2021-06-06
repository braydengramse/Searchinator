namespace Searchinator.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Interest
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual Person Person { get; set; }
    }
}