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
			var expectedColumnCount = csv.HeaderRecord?.Length; // Get correct column count

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

	public Library CreateAllRecords()
	{
		var bookId = 1;
		var authorId = 1;

		IList<Book> books = [];
		IList<Author> authors = [];
		IList<BookLinkAuthor> bookLinkAuthor = [];

		var badRecords = 0;
		IList<string> seenAuthors = [];
		foreach (var record in _records)
		{
			try
			{
				books.Add(CreateBook(record, bookId));

				var authorNames = record.authors.Split("/").Select((s) => s.Trim());
				foreach (var name in authorNames)
				{
					if (!seenAuthors.Contains(name))
					{
						var author = CreateAuthor(name, authorId);
						authors.Add(author);
						seenAuthors.Add(name);
						bookLinkAuthor.Add(CreateAuthorBook(bookId, authorId));
						authorId++;
					}
					else
					{
						var author = authors.First(a => a.Name == name);
						bookLinkAuthor.Add(CreateAuthorBook(bookId, author.Id));
					}
				}

				bookId++;
			}
			catch (Exception e)
			{
				badRecords++;
			}
		}

		var result = new Library()
		{
			Books = books,
			Authors = authors,
			BookLinkAuthor = bookLinkAuthor
		};

		Console.WriteLine($"{books.Count} book records created");
		Console.WriteLine($"{authors.Count} author records created");
		Console.WriteLine($"{bookLinkAuthor.Count} author link book records created");
		Console.WriteLine($"{badRecords} bad records skipped.");
		return result;
	}

	private static Book CreateBook(BooksCsv record, int id)
	{
		var book = new Book()
		{
			Id = id,
			Title = record.title,
			AverageRating = double.Parse(record.average_rating),
			Isbn = record.isbn,
			Isbn13 = record.isbn13,
			NumPages = int.Parse(record.num_pages),
			RatingsCount = int.Parse(record.ratings_count),
			TextReviewsCount = int.Parse(record.text_reviews_count),
			PublicationDate = DateOnly.Parse(record.publication_date),
			Publisher = record.publisher
		};
		return book;
	}

	private static Author CreateAuthor(string name, int id)
	{
		var author = new Author()
		{
			Id = id,
			Name = name
		};

		return author;
	}

	private static BookLinkAuthor CreateAuthorBook(int bookId, int authorId)
	{
		var authorBook = new BookLinkAuthor()
		{
			BooksBookId = bookId,
			AuthorsAuthorId = authorId
		};

		return authorBook;
	}
}