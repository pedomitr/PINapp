using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private NewContactContext _context;
		private string userId;


		public HomeController(NewContactContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			var _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			userId = _userId;
		}

		// GET: NewContacts
		public async Task<IActionResult> Index(string searchTerm)
		{

			var model =
					_context.NewContact
					.OrderBy(c => c.FirstName)
					.Where(c => ((searchTerm == null || c.FirstName.StartsWith(searchTerm) ||
						c.LastName.StartsWith(searchTerm) || c.PhoneNumber.Contains(searchTerm)) &&
						c.UserID.Equals(userId)))
					.Select(c => new NewContact
					{
						ID = c.ID,
						FirstName = c.FirstName,
						LastName = c.LastName,
						PhoneNumber = c.PhoneNumber,
						Email = c.Email,
						Street = c.Street,
						PostalCode = c.PostalCode,
						City = c.City,
						Country = c.Country,
						UserID = c.UserID
					});


			return View(await model.ToListAsync());
		}

		// GET: NewContacts/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var newContact = await _context.NewContact
				.SingleOrDefaultAsync(m => m.ID == id);
			if (newContact == null)
			{
				return NotFound();
			}

			return View(newContact);
		}

		// GET: NewContacts/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: NewContacts/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,PhoneNumber,Email,Street,PostalCode,City,Country,UserID")] NewContact newContact)
		{
			newContact.UserID = userId;
			if (ModelState.IsValid)
			{
				_context.Add(newContact);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(newContact);
		}

		// GET: NewContacts/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var newContact = await _context.NewContact.SingleOrDefaultAsync(m => m.ID == id);
			if (newContact == null)
			{
				return NotFound();
			}
			return View(newContact);
		}

		// POST: NewContacts/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,PhoneNumber,Email,Street,PostalCode,City,Country,UserID")] NewContact newContact)
		{
			if (id != newContact.ID)
			{
				return NotFound();
			}
			newContact.UserID = userId;
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(newContact);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!NewContactExists(newContact.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(newContact);
		}

		// GET: NewContacts/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var newContact = await _context.NewContact
				.SingleOrDefaultAsync(m => m.ID == id);
			if (newContact == null)
			{
				return NotFound();
			}

			return View(newContact);
		}

		// POST: NewContacts/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var newContact = await _context.NewContact.SingleOrDefaultAsync(m => m.ID == id);
			_context.NewContact.Remove(newContact);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool NewContactExists(int id)
		{
			return _context.NewContact.Any(e => e.ID == id);
		}

		//NewContacts/DeletAlle/5
		public async Task<IActionResult> DeleteAll()
		{
			foreach (var contact in _context.NewContact)
			{
				if (contact.UserID == userId)
				{
					_context.NewContact.Remove(contact);
				}
			}
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}	
	}
}

