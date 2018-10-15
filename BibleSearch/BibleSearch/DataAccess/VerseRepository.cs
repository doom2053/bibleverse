using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace BibleSearch.DataAccess
{
	public class VerseRepository : IVerseRepository
	{
		/// <summary>
		/// Searches for verses that contain search term(s).
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="startIndex">The start index.</param>
		/// <returns></returns>
		public List<Models.VerseModel> SearchVerses(string searchTerm, int startIndex)
		{
			// Get search string compatible with SQL statement
			string finalSearchString = GetSearchString(searchTerm);

			// Create the database connection and the command to execute the SQL statement
			DataTable dataTable = GetVersesTable(finalSearchString, startIndex);

			// Get the list of verse models
			List<Models.VerseModel> verseList = GetVerseList(dataTable);

			return verseList;
		}

		/// <summary>
		/// Gets the verse count for search term(s).
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <returns></returns>
		public int GetVerseCountForSearch(string searchTerm)
		{
			// Get search string compatible with SQL statement
			string finalSearchString = GetSearchString(searchTerm);

			// Get the search count
			int count = GetVerseCount(finalSearchString);

			return count;
		}

		private MySqlConnection CreateMySqlConnection()
		{
			// Get the Bible database connection string
			ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["BibleDatabase"];

			if (connectionString == null)
			{
				throw new ApplicationException(
					"Bible database connection string is missing from the application configuration.");
			}

			MySqlConnection connection = new MySqlConnection(connectionString.ConnectionString);

			return connection;
		}

		private static string GetSearchString(string searchTerm)
		{
			string finalSearchString;

			if (searchTerm.Trim().StartsWith("\"") && searchTerm.Trim().EndsWith("\""))
			{
				// If the search term is contained in quotes, then search by phrase
				finalSearchString = "(" + searchTerm.Trim(new[] { '"' }) + ")";
			}
			else
			{
				// If the search term is not quoted, get the search string with keyword(s)
				string[] terms = searchTerm.Split(new[] { ' ' });

				// If a term does not start with "+" or "-", add "+" to ensure it's included in search
				for (int i = 0; i < terms.Length; ++i)
				{
					if (!(terms[i].StartsWith("+") || terms[i].StartsWith("-")))
					{
						terms[i] = "+" + terms[i];
					}
				}

				// Create the final search string  from modified terms
				finalSearchString = String.Join(" ", terms);
			}

			return finalSearchString;
		}

		private int GetVerseCount(string searchString)
		{
			int count;

			// Open a connection to the database and get the results using the supplied parameter
			using (MySqlConnection connection = CreateMySqlConnection())
			using (MySqlCommand command = new MySqlCommand("SearchBibleGetCount", connection))
			{
				connection.Open();

				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("searchTerm", searchString);

				// Get the count from the database
				object result = command.ExecuteScalar();
				count = int.Parse(result.ToString());
			}

			return count;
		}

		private List<Models.VerseModel> GetVerseList(DataTable dataTable)
		{
			// Create a list of verse models from the database result
			var verseList = (from r in dataTable.AsEnumerable()
							 select new Models.VerseModel
							 {
								 Book = r.Field<string>("book"),
								 Chapter = r.Field<byte>("cap"),
								 Text = r.Field<string>("line"),
								 Verse = r.Field<byte>("verse"),
								 VerseId = r.Field<uint>("id")
							 }).ToList();

			return verseList;
		}

		private DataTable GetVersesTable(string searchString, int startIndex)
		{
			DataTable dataTable;

			// Open a connection to the database and get the results using the supplied parameters
			using (MySqlConnection connection = CreateMySqlConnection())
			using (MySqlCommand command = new MySqlCommand("SearchBibleUsingBoolean", connection))
			using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command))
			{
				connection.Open();

				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("searchTerm", searchString);
				command.Parameters.AddWithValue("startIndex", startIndex);

				// Fill a datatable
				dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
			}

			return dataTable;
		}
	}