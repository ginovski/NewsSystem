namespace NewsSystem.Admin
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using NewsSystem.Models;
    using NewsSystem.Controls.ErrorHandlerControl;

    public partial class EditCategories : Page
    {
        private NewsSystemDbContext db;

        public EditCategories()
        {
            this.db = new NewsSystemDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Category> ListViewEditCategories_GetData()
        {
            return this.db.Categories.OrderBy(c => c.ID);
        }

        public void ListViewEditCategories_UpdateItem(int id)
        {
            Category item = this.db.Categories.Find(id);

            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                this.db.SaveChanges();
                ErrorHandlerControl.AddSuccessMessage("Category has been modified successfuly");
            }
            else
            {
                if (ModelState["Name"].Errors.Count > 0)
                {
                    ErrorHandlerControl.AddErrorMessage(ModelState["Name"].Errors.FirstOrDefault().ErrorMessage);
                }
            }
        }

        public void ListViewEditCategories_InsertItem()
        {
            var item = new Category();

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                this.db.Categories.Add(item);
                this.db.SaveChanges();
                ErrorHandlerControl.AddSuccessMessage("Category has been added successfuly");
            }
            else
            {
                if (ModelState["Name"].Errors.Count > 0)
                {
                    ErrorHandlerControl.AddErrorMessage(ModelState["Name"].Errors.FirstOrDefault().ErrorMessage);
                }
            }
        }

        public void ListViewEditCategories_DeleteItem(int id)
        {
            var item = this.db.Categories.Find(id);

            if (this.db.Categories.Remove(item) != null)
            {
                ErrorHandlerControl.AddSuccessMessage("Category has been removed successfuly");
            }
            this.db.SaveChanges();
        }
    }
}