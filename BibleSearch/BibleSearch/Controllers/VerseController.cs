using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BibleSearch.DataAccess;

namespace BibleSearch.Controllers
{
	public class VerseController : Controller
	{
		//
		// GET: /Verse/

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Results(string searchTerms, int startIndex)
		{
			// Get the list of verses to send to view
			IVerseRepository repository = new VerseRepository();
			List<Models.VerseModel> verseList = repository.SearchVerses(searchTerms, startIndex);

			// Get the search count
			ViewBag.Count = repository.GetVerseCountForSearch(searchTerms);

			// Store the search term viewbag for use by the view
			ViewBag.SearchTerms = searchTerms;
			// Store the start index, incremented by 20 for the next 20 results
			ViewBag.StartIndex = startIndex + 20;
			// Store a friendly start and end number to display to user
			// Do this here rather than in view to put as little code in view as necessary
			ViewBag.FriendlyStart = startIndex + 1;

			if (startIndex + 20 < ViewBag.Count)
			{
				// Set the end value
				ViewBag.FriendlyEnd = startIndex + 20;
			}
			else
			{
				// If the end value would be greater than the count, then set end value to count
				ViewBag.FriendlyEnd = ViewBag.Count;
			}


			// Store previous start index to allow for a "previous" link
			ViewBag.PreviousIndex = startIndex - 20;

			// Ensure that the previous index is not less than 0
			if (ViewBag.PreviousIndex < 0)
			{
				ViewBag.PreviousIndex = 0;
			}

			return View(verseList);
		}
	}
}
