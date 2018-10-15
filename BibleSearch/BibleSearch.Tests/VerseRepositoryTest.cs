using System;
using BibleSearch.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using BibleSearch.Models;
using System.Collections.Generic;

namespace BibleSearch.Tests
{


	/// <summary>
	///This is a test class for VerseRepositoryTest and is intended
	///to contain all VerseRepositoryTest Unit Tests
	///</summary>
	[TestClass()]
	public class VerseRepositoryTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///An integration test for SearchVerses
		///</summary>
		[TestMethod()]
		public void SearchVersesTest()
		{
			VerseRepository target = new VerseRepository(); // TODO: Initialize to an appropriate value
			string searchTerm = "Thou"; // TODO: Initialize to an appropriate value
			int startIndex = 0; // TODO: Initialize to an appropriate value
			List<VerseModel> actual;
			actual = target.SearchVerses(searchTerm, startIndex);
			Assert.AreNotEqual(actual.Count, 0);
		}
	}
}
