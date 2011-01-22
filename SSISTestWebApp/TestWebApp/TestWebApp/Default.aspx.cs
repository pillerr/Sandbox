using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RunButton_Click(object sender, EventArgs e)
        {
               PKService.Service1 launchPackageService = new PKService.Service1();
                int packageResult = 0;
                bool t = false;
                

      try
      {
         launchPackageService.LaunchPackage(PkType.Text.ToString(), PKLoc.Text.ToString(), PKName.Text.ToString(),out packageResult, out t) ;
         Response.Write((PackageExecutionResult)packageResult);
      }
      catch (Exception ex)
      {
        //PKName.Text = ex.Message;
      }

    

    }
        private enum PackageExecutionResult
        {
            PackageSucceeded,
            PackageFailed,
            PackageCompleted,
            PackageWasCancelled
        };


  

        
    }
}
