using System;
using System.Collections.Generic;
using System.IO;

namespace BookLibrary
{
    class Program
    {
        struct Book
        {
            public string Title;
            public string Author;
            public int Pages;
            public string Genre;
            public string Location;

            public Book(string title, string author, int pages, string genre, string location)
            {
                Title = title;
                Author = author;
                Pages = pages;
                Genre = genre;
                Location = location;
            }
        }

        static List<Book> books = new List<Book>();
        const string fileName = "books.txt";

        static void Main(string[] args)
        {
            LoadBooksFromFile();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Könyvtár kezelése:");
                Console.WriteLine("1. Új könyv hozzáadása");
                Console.WriteLine("2. Könyv törlése");
                Console.WriteLine("3. Könyvek kilistázása");
                Console.WriteLine("4. Kilépés");

                Console.Write("Válasszon egy menüpontot: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DeleteBook();
                        break;
                    case "3":
                        ListBooks();
                        break;
                    case "4":
                        SaveBooksToFile();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás. Kérem, válasszon újra.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void LoadBooksFromFile()
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    Book book = new Book(parts[0], parts[1], int.Parse(parts[2]), parts[3], parts[4]);
                    books.Add(book);
                }
            }
        }

        static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("Új könyv hozzáadása:");

            Console.Write("Cím: ");
            string title = Console.ReadLine();
            Console.Write("Szerző: ");
            string author = Console.ReadLine();
            Console.Write("Oldalszám: ");
            int pages;
            while (!int.TryParse(Console.ReadLine(), out pages) || pages <= 0)
            {
                Console.Write("Hibás adat! Kérem adjon meg egy pozitív egész számot az oldalszámhoz: ");
            }
            Console.Write("Műfaj: ");
            string genre = Console.ReadLine();
            Console.Write("Hely (könyvespolc száma-könyvespolc sora): ");
            string location = Console.ReadLine();

            Book newBook = new Book(title, author, pages, genre, location);
            books.Add(newBook);
            SaveBooksToFile();

            Console.WriteLine("Könyv hozzáadva a könyvtárhoz.");
            Console.ReadKey();
        }

        static void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("Könyv törlése:");

            Console.WriteLine("Könyvek listája:");
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Cím: {books[i].Title}, Szerző: {books[i].Author}, Hely: {books[i].Location}");
            }

            Console.WriteLine("0. Kilépés");

            Console.Write("Adja meg a törlendő könyv sorszámát: ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // Kilépés a törlésből
            }

            int index;
            while (!int.TryParse(input, out index) || index < 1 || index > books.Count)
            {
                Console.Write($"Hibás adat! Kérem adjon meg egy sorszámot 1 és {books.Count} között, vagy 0-t a kilépéshez: ");
                input = Console.ReadLine();
                if (input == "0")
                {
                    return; // Kilépés a törlésből
                }
            }
            index--; // Adjusting to zero-based index

            books.RemoveAt(index);
            Console.WriteLine("Könyv törölve a könyvtárból.");
            Console.ReadKey();
        }

        static void ListBooks()
        {
            Console.Clear();
            Console.WriteLine("Könyvek a könyvtárban:");

            foreach (var book in books)
            {
                Console.WriteLine($"Cím: {book.Title}, Szerző: {book.Author}, Oldalszám: {book.Pages}, Műfaj: {book.Genre}, Hely: {book.Location}");
            }

            Console.ReadKey();
        }

        static void SaveBooksToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Title},{book.Author},{book.Pages},{book.Genre},{book.Location}");
                }
            }
            Console.WriteLine($"Könyvek elmentve a(z) {fileName} fájlba.");
            Console.ReadKey();
        }
    }
}
