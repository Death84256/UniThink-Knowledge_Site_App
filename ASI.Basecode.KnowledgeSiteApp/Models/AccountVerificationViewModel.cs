namespace ASI.Basecode.KnowledgeSiteApp.Models
{
    public class AccountVerificationViewModel
    {
        public string Email { get; set; } // The email address for account verification
        public string MobileNumber { get; set; } // The mobile number for account verification
        public string VerificationCode { get; set; } // The code entered by the user for verification
    }
}
