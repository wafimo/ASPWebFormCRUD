using System;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (testDBEntities obj = new testDBEntities())
            {
                Department cpt = new Department
                {
                    Name = txtCourse.Text.Trim()
                };
                obj.Departments.Add(cpt);
                obj.SaveChanges();
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}