using Humanizer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASI.Basecode.KnowledgeSiteAdminApp.Models
{
    public class ForgotAccountViewModel
    {
        /*This class will contain the properties needed to represent the data on the "Forgot Account" form*/
        [EmailAddress]
        public string Email { get; set; }

    }
}
