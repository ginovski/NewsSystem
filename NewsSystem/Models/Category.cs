namespace NewsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();   
        }

        public int ID { get; set; }

        [Required(ErrorMessage="Name cannot be empty")]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}