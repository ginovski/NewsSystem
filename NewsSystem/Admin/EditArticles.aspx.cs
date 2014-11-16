namespace NewsSystem.Admin
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;

    using NewsSystem.Models;
    using NewsSystem.Extensions;
    using NewsSystem.Controls.ErrorHandlerControl;

    public partial class EditArticle : Page
    {
        private NewsSystemDbContext db;

        public EditArticle()
        {
            this.db = new NewsSystemDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Article> ListViewEditArticle_GetData()
        {
            return this.db.Articles.OrderBy(a => a.ID);
        }

        public IQueryable<Category> GetAllCategories()
        {
            return this.db.Categories;
        }

        public void ListViewEditArticle_UpdateItem(int id)
        {
            Article item = this.db.Articles.Find(id);

            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                this.db.SaveChanges();
                ErrorHandlerControl.AddSuccessMessage("Article has been modified successfuly");
            }
            else
            {
                if (ModelState["Title"].Errors.Count > 0)
                {
                    ErrorHandlerControl.AddErrorMessage(ModelState["Title"].Errors.FirstOrDefault().ErrorMessage);
                }
                if (ModelState["Content"].Errors.Count > 0)
                {
                    ErrorHandlerControl.AddErrorMessage(ModelState["Content"].Errors.FirstOrDefault().ErrorMessage);
                }
            }
        }

        public void ListViewEditArticle_InsertItem()
        {
            var item = new Article();

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                item.DateCreated = DateTime.Now;
                item.AuthorID = User.Identity.GetUserId();
                this.db.Articles.Add(item);
                this.db.SaveChanges();
                ErrorHandlerControl.AddSuccessMessage("Article was added successfuly");
                ListViewEditArticle.InsertItemPosition = InsertItemPosition.None;
            }
            else
            {
                if (ModelState["Title"].Errors.Count > 0)
                {
                  ErrorHandlerControl.AddErrorMessage(ModelState["Title"].Errors.FirstOrDefault().ErrorMessage);
                }
                if (ModelState["Content"].Errors.Count > 0)
                {
                    ErrorHandlerControl.AddErrorMessage(ModelState["Content"].Errors.FirstOrDefault().ErrorMessage);
                }
            }
        }

        public void ListViewEditArticle_DeleteItem(int id)
        {
            var article = this.db.Articles.Find(id);

            if (this.db.Articles.Remove(article) != null)
            {
                ErrorHandlerControl.AddSuccessMessage("Article has been removed successfuly");
            }
            this.db.SaveChanges();
        }

        protected void LinkButtonInsert_Click(object sender, EventArgs e)
        {
            ListViewEditArticle.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            ListViewEditArticle.InsertItemPosition = InsertItemPosition.None;
        }

    }
}