﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;

namespace _05.BookLibrary
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            String[] inputFile = File.ReadAllLines("input.txt");
            File.Delete("output.txt");

            int n = int.Parse(inputFile[0]);

            List<Book> books = new List<Book>();

            for (int i = 1; i <= n; i++)
            {
                books.Add(GetBook(inputFile[i]));
            }

            Library library = new Library { Name = "Library", Books = books };

            Dictionary<string, decimal> authors = new Dictionary<string, decimal>();

            foreach (Book book in library.Books)
            {
                if (authors.ContainsKey(book.Author))
                {
                    authors[book.Author] += book.Price;
                }
                else
                {
                    authors[book.Author] = book.Price;
                }
            }

            File.WriteAllText("output.txt", "");

            foreach (var pair in authors.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                string output = $"{pair.Key} -> {pair.Value}" + Environment.NewLine;
                File.AppendAllText("output.txt", output);
            }
        }

        private static Book GetBook(string input)
        {
            string[] inputArr = input.Split(' ');
            string title = inputArr[0];
            string author = inputArr[1];
            string publisher = inputArr[2];
            DateTime releaseDate = DateTime.ParseExact(inputArr[3], "d.M.yyyy", CultureInfo.InvariantCulture);
            string isbn = inputArr[4];
            decimal price = decimal.Parse(inputArr[5]);

            return new Book { Title = title, Author = author, Publisher = publisher, ReleaseDate = releaseDate, ISBN = isbn, Price = price };
        }
    }

    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
    }

    internal class Library
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}