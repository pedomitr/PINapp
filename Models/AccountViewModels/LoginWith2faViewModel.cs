using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
		[Required]
        [StringLength(7, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} and at max {1characters long.")]

		[DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }

        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }

		/*", ErrorMessage = "The {0} must be at least {2} and at max {1characters long."*/
		/*string.Format(@Phonebook.Resources.Resources.Account_LoginWith2fa_Error*/
	}
}
