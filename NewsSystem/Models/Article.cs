namespace NewsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public Article()
        {
            this.Likes = new HashSet<Like>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage="Title cannot be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content cannot be empty")]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public string AuthorID { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
