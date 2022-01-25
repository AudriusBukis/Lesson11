using Lesson11.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace lesson11
{
    internal class Program
    {
        public static List<Book> BookStorage = new List<Book>();
        public static List<Reader> BorrowedBookList = new List<Reader>();
        static void Main(string[] args)
        {

            lesson11task1();


           

            static void lesson11task1()
            {
                /*1. Book entity
                2. Books repository
                3. Repository  saves a book in a file*/

                /* 1. Check if such a book already exists, if so add a counter to an existing one (For example a book is saved like this "Twilight 7"
                   2. Create a Reader entity which is capable of "borrowing" a book which means getting it from file(therefore removing 1 from counter) and putting it into a private list.
                   3. Create a method in a Reader class to give a book back therefore adding +1 in a file and removing from its private list.*/
                
                Console.WriteLine("Lesson 11 task 1");
                var rep = new BookRepository();
                bool isTrue = true;
                BorrowedBookList = rep.ReadAllBorrowerFromFile();
                BookStorage = rep.ReadAllBooksFromFile();
                while (isTrue)
                {
                    Console.Clear();
                    Console.WriteLine("Welkome To Book storage");
                    Console.WriteLine("What you wat to do ?");
                    Console.WriteLine(" 1 - Add the Book To Storage;");
                    Console.WriteLine(" 2 - Read all book storage: ");
                    Console.WriteLine(" 3 - Wriete all books To storage");
                    Console.WriteLine(" 4 - Borow book from storage");
                    Console.WriteLine(" 5 - Return book to storage");
                    Console.WriteLine(" 6 - View list off books which is borrowed");
                    
                    int inputNumber = Convert.ToInt32(Console.ReadLine());
                    switch (inputNumber)
                    {
                        case 1:
                            var book1 = new Book();
                            Console.WriteLine("Enter book name");
                            book1.Name = Console.ReadLine();
                            Console.WriteLine("Enter book author");
                            book1.Author = Console.ReadLine();
                            Console.WriteLine("Enter book Year");
                            book1.Year = Convert.ToInt32(Console.ReadLine());
                            book1.BookAmount = 1;

                            rep.WriteBookToFile(book1, BookStorage);
                            rep.WriteAllBookToFile(BookStorage);

                            break;
                        case 2:
                            BookStorage.Clear();
                            Console.WriteLine("Full list of books in storage");
                            BookStorage = rep.ReadAllBooksFromFile();
                            foreach (var book in BookStorage)
                            {
                                Console.WriteLine($"Book Name: {book.Name}; Book author : {book.Author}; Book Year: {book.Year}; Amount books in storage : {book.BookAmount}");
                            }
                            Console.WriteLine( "-------------------------------------------");
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadKey();
                            break;
                        case 3:
                            BookStorage.Clear(); 
                            Console.WriteLine("This list was writen in storage");
                            BookStorage = rep.ReadAllBooksFromFile();
                            foreach (var book in BookStorage)
                            {
                                Console.WriteLine($"Book Name: {book.Name}; Book author : {book.Author}; Book Year: {book.Year}; Amount books in storage : {book.BookAmount}");
                            }
                            rep.WriteAllBookToFile(BookStorage);
                            Console.WriteLine("-------------------------------------------");
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadKey();
                            break;
                         case 4:
                            Console.Clear();
                            rep.BorrowBookFromStorage(BookStorage, BorrowedBookList);
                            rep.WriteAllBookToFile(BookStorage);
                            break;
                         case 5:
                            Console.Clear();
                            rep.ReturnBookToStorage(BookStorage, BorrowedBookList);
                            rep.WriteAllBookToFile(BookStorage);
                            break;
                         case 6:
                            int conter = 1;
                            BorrowedBookList.Clear();   
                            BorrowedBookList = rep.ReadAllBorrowerFromFile();
                            foreach (var book in BorrowedBookList) 
                            {
                                Console.WriteLine($"Borrowers Name {book.ReaderName}");
                                Console.WriteLine($"Book Name: {book.Name}; Book author : {book.Author}; Book Year: {book.Year}; Amount books in storage : {book.BookAmount}");
                                conter++;
                            }
                            Console.WriteLine("-------------------------------------------");
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadKey();
                            break;
                         default:
                            Console.WriteLine("No such selection");
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadKey();
                            break;
                    
                    
                    }
                }
               
                
            }
        }
    }
}
