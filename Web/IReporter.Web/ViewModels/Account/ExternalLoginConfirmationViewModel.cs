namespace IReporter.Web.ViewModels.Account
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using IReporter.Common;

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]

        // [Range(typeof(DateTime), "1/1/1900", "31/12/2020")]
        public DateTime? BirthDate { get; set; }

        [DisplayName(GlobalConstants.FirstNameDisplayName)]
        [StringLength(maximumLength: 30, ErrorMessage = "{0} should be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [DisplayName(GlobalConstants.LastNameDisplayName)]
        [StringLength(maximumLength: 30, ErrorMessage = "{0} should be between {2} and {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
