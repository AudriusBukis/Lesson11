using System;

namespace Lesson11.Models
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int BookAmount { get; set; }

        public Book(string name, string author, int year, int bookAmount)
        {
            Name = name;
            Author = author;
            Year = year;
            BookAmount = bookAmount;
        }

        public Book()
        {
        }
    }
}
