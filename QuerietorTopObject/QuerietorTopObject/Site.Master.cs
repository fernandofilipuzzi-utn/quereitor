using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerietorTopObject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ShowMessage(string titulo, string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", $"mostrarModal('{mensaje}');", true);
        }
    }
}