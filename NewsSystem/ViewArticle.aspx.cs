namespace NewsSystem
{
    using System;
    using System.Web.UI;

    using NewsSystem.Models;
    using System.Web.ModelBinding;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ViewArticle : Page
    {
        private NewsSystemDbContext db;

        public ViewArticle()
        {
            this.db = new NewsSystemDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Article FormViewArticleDetails_GetItem([QueryString]int? id)
        {
            if (id == null)
            {
                Response.Redirect("/");
            }

            return this.db.Articles.Find(id);
        }
    }
}