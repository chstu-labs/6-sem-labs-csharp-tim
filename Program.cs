using System.Collections.Generic;
using System;
using System.Text.Json;
using System.IO;

namespace Lab1
{
    class Program
    {
        private static HashSet<Book> bookCollection = new HashSet<Book>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter command add, remove, list, or exit:");
                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "add":
                        AddBook();
                        break;
                    case "remove":
                        RemoveBook();
                        break;
                    case "list":
                        ListBooks();
                        break;
                    case "save":
                        SaveBooks();
                        break;
                    case "exit":
                        Console.WriteLine("Exiting the program.");
                        return;
                    default:
                        Console.WriteLine("Invalid command. Please enter 'add', 'remove', 'list', 'save' or 'exit'.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.WriteLine("Enter book details:");
            Console.WriteLine("Title:");
            string title = Console.ReadLine();
            Console.WriteLine("Author:");
            string author = Console.ReadLine();
            Console.WriteLine("Year:");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Genre:");
            string genre = Console.ReadLine();
            Console.WriteLine("ISBN:");
            string isbn = Console.ReadLine();

            Book newBook = new Book(title, author, year, genre, isbn);
            bookCollection.Add(newBook);
            Console.WriteLine("Book added successfully.");
        }

        static void RemoveBook()
        {
            Console.WriteLine("Enter the ISBN of the book to remove:");
            string isbn = Console.ReadLine();

            foreach (var book in bookCollection)
            {
                if (book.ISBN == isbn)
                {
                    bookCollection.Remove(book);
                    Console.WriteLine("Book removed successfully.");
                    return;
                }
            }
            Console.WriteLine("Book with the provided ISBN not found.");
        }

        static void ListBooks()
        {
            Console.WriteLine("Books in the collection:");
            foreach (var book in bookCollection)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.Year}, Genre: {book.Genre}, ISBN: {book.ISBN}");
            }
        }

        static void SaveBooks()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(bookCollection, options);
            File.WriteAllText("books.json", jsonString);
            Console.WriteLine("Books saved successfully.");
        }
    }
}