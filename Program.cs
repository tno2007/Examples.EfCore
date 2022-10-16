// See https://aka.ms/new-console-template for more information
using System.Text;
using Examples.EfCore.Contexts;
using Examples.EfCore.Entities.Form;
using Examples.EfCore.Entities.Library;

Console.WriteLine("Hello, World!");


Seed();
//InsertData();
//PrintData();


static void Seed()
{
    using (var context = new FormContext())
    {
        // create db if not exists
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // add publisher
        var questionnaire = new Questionnaire
        {
            Name = "Client Questionnaire"
        };

        if (context.Questionnaire == null) return;

        context.Questionnaire.Add(questionnaire);

        // add some books
        var questions = new List<Question>() {
            new Question {
                Questionnaire = questionnaire,
                Label = "First name",
                Type = "string"
            },
            new Question {
                Questionnaire = questionnaire,
                Label = "Last name",
                Type = "string"
            }
        };

        if (context.Question == null) return;

        context.Question.AddRange(questions);

        // saves changes
        context.SaveChanges();
    }
}


static void InsertData()
{
    using (var context = new LibraryContext())
    {
        // create db if not exists
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // add publisher
        var publisher = new Publisher
        {
            Name = "Mariner Books"
        };

        context.Publisher.Add(publisher);

        // add some books
        var books = new List<Book>();

        books.Add(new Book
        {
            Isbn = "978-0544003415",
            Title = "The Lord of the Rings",
            Author = "J.R.R. Tolkien",
            Language = "English",
            Pages = 1216,
            Publisher = publisher
        });

        books.Add(new Book
        {
            Isbn = "978-0547247762",
            Title = "The Sealed Letter",
            Author = "Emma Donoghue",
            Language = "English",
            Pages = 416,
            Publisher = publisher
        });

        context.Book.AddRange(books);

        // saves changes
        context.SaveChanges();
    }
}

static void PrintData()
{
    // Gets and prints all books in database
    using (var context = new LibraryContext())
    {
        //var books = context.Book.
        //.Include(p => p.Publisher);

        var books = context.Book;

        if (books == null) return;

        foreach (var book in books)
        {
            var data = new StringBuilder();
            data.AppendLine($"ISBN: {book.Isbn}");
            data.AppendLine($"Title: {book.Title}");
            data.AppendLine($"Publisher: {book.Publisher.Name}");
            Console.WriteLine(data.ToString());
        }
    }
}