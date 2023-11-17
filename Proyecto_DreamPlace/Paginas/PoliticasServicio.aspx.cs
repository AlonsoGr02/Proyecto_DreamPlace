using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class PoliticasServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                topnav.Visible = false;
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}