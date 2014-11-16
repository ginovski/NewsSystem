namespace NewsSystem
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.UI;

    using NewsSystem.Models;

    public partial class News : Page
    {
        private NewsSystemDbContext db;

        public News()
        {
            this.db = new NewsSystemDbContext();  
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Article> RepeaterNews_GetData()
        {
            return this.db.Articles.OrderByDescending(a => a.Likes.Count()).Take(3);
        }

        public IQueryable<Category> ListViewCategories_GetData()
        {
            return this.db.Categories;
        }
    }
}