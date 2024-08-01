using System;
using System.Collections.Generic;


#region Part-1
public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string[] Authors { get; set; }
    public DateTime PublicationDate { get; set; }
    public decimal Price { get; set; }

    public Book(string _ISBN, string _Title, string[] _Authors, DateTime _PublicationDate, decimal _Price)
    {
        ISBN = _ISBN;
        Title = _Title;
        Authors = _Authors;
        PublicationDate = _PublicationDate;
        Price = _Price;
    }

    public override string ToString()
    {
        return $"ISBN: {ISBN}, Title: {Title}, Authors: {string.Join(", ", Authors)}, Publication Date: {PublicationDate.ToShortDateString()}, Price: {Price:C}";
    }
}

public class BookFunctions
{
    public static string GetTitle(Book B)
    {
        return B.Title;
    }

    public static string GetAuthors(Book B)
    {
        return string.Join(", ", B.Authors);
    }

    public static string GetPrice(Book B)
    {
        return B.Price.ToString("C");
    }
}

public class LibraryEngine
{

    public delegate string BookFunctionDelegate(Book B);

    public static void ProcessBooks(List<Book> bList, Func<Book, string> fPtr)
    {
        foreach (Book B in bList)
        {
            Console.WriteLine(fPtr(B));
        }
    }

    public static void Main()
    {
        string[] authors = { "Author1", "Author2" };
        List<Book> books = new List<Book>
        {
            new Book("123-456-789", "Sample Book 1", authors, DateTime.Now, 29.99m),
            new Book("987-654-321", "Sample Book 2", authors, DateTime.Now.AddYears(-1), 39.99m)
        };


        BookFunctionDelegate titleDelegate = new BookFunctionDelegate(BookFunctions.GetTitle);
        ProcessBooks(books, new Func<Book, string>(titleDelegate));


        Func<Book, string> authorsDelegate = new Func<Book, string>(BookFunctions.GetAuthors);
        ProcessBooks(books, authorsDelegate);


        Func<Book, string> isbnDelegate = delegate (Book B)
        {
            return B.ISBN;
        };
        ProcessBooks(books, isbnDelegate);


        Func<Book, string> pubDateDelegate = B => B.PublicationDate.ToShortDateString();
        ProcessBooks(books, pubDateDelegate);
    }
}
#endregion



#region Part-2
public class CustomList<T>
{
    private T[] items;
    private int count;

    public CustomList()
    {
        items = new T[4];
        count = 0;
    }

    public int Count => count;

    public void Add(T item)
    {
        if (count == items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
        }
        items[count++] = item;
    }

    public bool Exists(Predicate<T> match)
    {
        foreach (T item in items)
        {
            if (match(item))
            {
                return true;
            }
        }
        return false;
    }

    public T Find(Predicate<T> match)
    {
        foreach (T item in items)
        {
            if (match(item))
            {
                return item;
            }
        }
        return default(T);
    }

    public CustomList<T> FindAll(Predicate<T> match)
    {
        CustomList<T> results = new CustomList<T>();
        foreach (T item in items)
        {
            if (match(item))
            {
                results.Add(item);
            }
        }
        return results;
    }

    public int FindIndex(Predicate<T> match)
    {
        for (int i = 0; i < count; i++)
        {
            if (match(items[i]))
            {
                return i;
            }
        }
        return -1;
    }

    public T FindLast(Predicate<T> match)
    {
        for (int i = count - 1; i >= 0; i--)
        {
            if (match(items[i]))
            {
                return items[i];
            }
        }
        return default(T);
    }

    public int FindLastIndex(Predicate<T> match)
    {
        for (int i = count - 1; i >= 0; i--)
        {
            if (match(items[i]))
            {
                return i;
            }
        }
        return -1;
    }

    public void ForEach(Action<T> action)
    {
        for (int i = 0; i < count; i++)
        {
            action(items[i]);
        }
    }

    public bool TrueForAll(Predicate<T> match)
    {
        foreach (T item in items)
        {
            if (!match(item))
            {
                return false;
            }
        }
        return true;
    }
}
#endregion