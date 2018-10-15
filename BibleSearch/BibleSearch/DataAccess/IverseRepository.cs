using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleSearch.Models;

namespace BibleSearch.DataAccess
{
	public interface IVerseRepository
	{
		/// <summary>
		/// Searches for verses that contain search term(s).
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="startIndex">The start index.</param>
		/// <returns></returns>
		List<VerseModel> SearchVerses(string searchTerm, int startIndex);

		/// <summary>
		/// Gets the verse count for search term(s).
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <returns></returns>
		int GetVerseCountForSearch(string searchTerm);
	}
}
