using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibleSearch.Models
{
	/// <summary>
	/// Represents a Bible verse
	/// </summary>
	public class VerseModel
	{
		/// <summary>
		/// Gets or sets the verse id.
		/// </summary>
		/// <value>
		/// The verse id.
		/// </value>
		public uint VerseId { get; set; }

		/// <summary>
		/// Gets or sets the book.
		/// </summary>
		/// <value>
		/// The book.
		/// </value>
		public string Book { get; set; }

		/// <summary>
		/// Gets or sets the chapter.
		/// </summary>
		/// <value>
		/// The chapter.
		/// </value>
		public byte Chapter { get; set; }

		/// <summary>
		/// Gets or sets the verse.
		/// </summary>
		/// <value>
		/// The verse.
		/// </value>
		public byte Verse { get; set; }

		/// <summary>
		/// Gets or sets the text for this verse.
		/// </summary>
		/// <value>
		/// The text for this verse.
		/// </value>
		public string Text { get; set; }
	}
}