using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Phonebook.Models
{
	public class NewContact
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string Street { get; set; }
		public int? PostalCode { get; set; }
		public string City { get; set; }
		public string Country { get; set; }

		public string UserID { get; set; }
	}
}
