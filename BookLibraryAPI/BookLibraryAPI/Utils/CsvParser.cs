using System.Globalization;
using BookLibraryAPI.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace BookLibraryAPI.Utils;

public class CsvParser
{
	private List<BooksCsv> _records { get; }

	public CsvParser(string path)
	{
		_records = Parse(path);
	}

	private static List<BooksCsv> Parse(string path)
	{
		var config = InitConfig();
		using var reader = new StreamReader(path);
		using var csv = new CsvReader(reader, config);
		var records = new List<BooksCsv>();

		try
		{
			csv.Read(); // Moves the reader to the first row (header row)
			csv.ReadHeader(); // Reads the header row and maps column names
			var expectedColumnCount = csv.HeaderRecord.Length; // Get correct column count

			while (csv.Read())
			{
				if (csv.Parser.Count != expectedColumnCount)
				{
					continue;
				}

				var record = csv.GetRecord<BooksCsv>();
				records.Add(record);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Critical error: {ex.Message}");
		}

		Console.WriteLine($"Successfully read {records.Count} records.");
		return records;
	}

	private static CsvConfiguration InitConfig()
	{
		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			BadDataFound = null, // Ignore bad data records
			MissingFieldFound = null, // Ignore missing fields
			IgnoreBlankLines = true,
			TrimOptions = TrimOptions.Trim,
			DetectColumnCountChanges = false, // Ignore column count mismatches
			Delimiter = ",", // Ensure proper CSV parsing
			Quote = '"', // Enforce double quotes for enclosed fields
		};
		return config;
	}

	public IList<Book> CreateBookRecords()
	{
		var count = 1;
		IList<Book> books = [];
		var badRecords = 0;
		foreach (var record in _records)
		{
			try
			{
				books.Add(new Book()
				{
					Id = count,
					Title = record.title,
					AverageRating = double.Parse(record.average_rating),
					Isbn = record.isbn,
					Isbn13 = record.isbn13,
					NumPages = int.Parse(record.num_pages),
					RatingsCount = int.Parse(record.ratings_count),
					TextReviewsCount = int.Parse(record.text_reviews_count),
					PublicationDate = DateOnly.Parse(record.publication_date),
					Publisher = record.publisher
				});
				count++;
			}
			catch (Exception e)
			{
				badRecords++;
			}
		}

		Console.WriteLine($"BOOKS: {books.Count} records created");
		Console.WriteLine($"BOOKS: {badRecords} bad records skipped.");
		return books;
	}

	public IList<Author> CreateAuthorRecords()
	{
		IList<Author> authors = [];

		var count = 0;
		var badRecords = 0;

		var authorNames = GetAuthorNames();


		Console.WriteLine($"AUTHORS: {authors.Count} records created");
		Console.WriteLine($"AUTHORS: {badRecords} bad records found and were skipped.");
		return authors;
	}

	private string[] GetAuthorNames()
	{
		string[] authorNames = [];

		foreach (var record in _records)
		{
			Console.WriteLine(record.authors);
		}

		return authorNames;
	}
}