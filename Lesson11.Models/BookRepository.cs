using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lesson11.Models
{
    public class BookRepository
    {
        FileWriterServiceStorage Writer = new FileWriterServiceStorage();
        FileWriteReadServiceReader ReaderWriter = new FileWriteReadServiceReader();
        public List<string> ListBooks { get; set; }

        public BookRepository()
        {
            ListBooks = new List<string>();
        }

        public void WriteBookToFile(Book book, List<Book> listBooks) //List<Book>
        {
            bool isNotList = true;
            for (int i = 0; i < listBooks.Count; i++)
            {
                Book tempBook = listBooks[i];
                if (tempBook.Name == book.Name)
                {
                    listBooks[i].BookAmount ++;
                    isNotList = false;
                }
            }
            if (isNotList)
            {
                string saveBook = $"{book.Name},{book.Author},{book.Year},{book.BookAmount}";
                Writer.AppendText(saveBook);
                listBooks.Add(book);
            }
           // return listBooks;
        }

        public void WriteAllBookToFile(List<Book> book)
        {
            List<string> templist = new List<string>();
            foreach (Book record in book)
            {
                templist.Add($"{record.Name},{record.Author},{record.Year},{record.BookAmount}");
            }
            Writer.WriteAllText(templist.ToArray());

        }

        public List<Book> ReadAllBooksFromFile()
        {
            ListBooks.Clear();
            var listBooks = new List<Book>();
            List<string> alllinesFromfile = Writer.GetAllLines();
            for (int i = 0; i < alllinesFromfile.Count; i++)
            {
                var book = new Book();
                string[] tepm = alllinesFromfile[i].Split(",");
                book.Name = tepm[0];
                book.Author = tepm[1];
                book.Year = Convert.ToInt32(tepm[2]);
                book.BookAmount = Convert.ToInt32(tepm[3]);
                listBooks.Add(book);
            } 
                
            return listBooks;
        }
        public void BorrowBookFromStorage(List<Book> listBooks, List<Reader> borrowedListBooks)
        {
            int bookNo = 0;
            string borrowerName = "";
            int counter = 0;
            Console.WriteLine("Enter your Name");
            borrowerName = Console.ReadLine();
            Console.WriteLine("----------Book List------------");
            foreach (var book in listBooks)
            {
                Console.WriteLine($"{counter}No. Book Name: {book.Name}; Book author : {book.Author}; Book Year: {book.Year}; Amount books in storage : {book.BookAmount}");
                counter++;
            }
            Console.WriteLine("----------List end-----------");
            Console.WriteLine("Which book you want to borrow enter the number ");
            bookNo = Convert.ToInt32(Console.ReadLine());
            if (bookNo >= 0 && bookNo <= listBooks.Count)
            {
                if (listBooks[bookNo].BookAmount == 0)
                {
                    Console.WriteLine("We do not have this book at this moment ,sorry");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadKey();
                }
                else
                {
                    Reader borrower = new Reader(listBooks[bookNo].Name, listBooks[bookNo].Author, listBooks[bookNo].Year, 1, borrowerName);

                    borrowedListBooks.Add(borrower);
                    listBooks[bookNo].BookAmount--;

                }


            }
            else
            {
                Console.WriteLine("No such book");
                Console.WriteLine("Press Enter to continue");
                Console.ReadKey();
            }
            List<string> tempRederlist = new List<string>();
            foreach (Reader record in borrowedListBooks)
            {
                tempRederlist.Add($"{record.Name},{record.Author},{record.Year},{record.BookAmount},{record.ReaderName}");
            }
            ReaderWriter.WriteAllText(tempRederlist.ToArray());
        }
        
        public void ReturnBookToStorage(List<Book> listBooks, List<Reader> borrowedListBooks)
        {
            
            bool noSuchReader = true;
            Console.WriteLine("Enter your Name");
            string borrowerName = Console.ReadLine();
            List<Reader> redersBooks = new List<Reader>();
            foreach (var book in borrowedListBooks)
            {
                if (book.ReaderName == borrowerName)
                {
                    redersBooks.Add(book);
                    noSuchReader = false;
                }
            }
            if(noSuchReader)
            {
                Console.WriteLine("We do not have reader with this name in list, sorry");
                Console.WriteLine("Press Enter to continue");
                Console.ReadKey();
            } 
            else
            {
                int counter = 0;
                int bookIndexToDelete = 0;
                Console.WriteLine("----------Book List ------------");
                foreach (var book in redersBooks)
                {
                    Console.WriteLine($"{counter}No. Book Name: {book.Name}; Book author : {book.Author}; Book Year: {book.Year}; Amount books in storage : {book.BookAmount}");
                    counter++;
                }
                Console.WriteLine("----------List end-----------");
                Console.WriteLine("Which book you want to return enter number ");
                int bookNo = Convert.ToInt32(Console.ReadLine());
                string bookName = "";
                if (bookNo >= 0 && bookNo <= redersBooks.Count)
                {
                    int bookcCounter = 0;
                    foreach (var book in borrowedListBooks)
                    {

                        if (book.ReaderName == borrowerName && redersBooks[bookNo].Name == book.Name)
                        {
                            bookName = book.Name;
                            bookIndexToDelete = bookcCounter;
                        }
                        bookcCounter++;
                    }
                    borrowedListBooks.RemoveAt(bookIndexToDelete);
                    foreach (var book in listBooks)
                    {
                        if (book.Name == bookName)
                        {
                            book.BookAmount++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No such book");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadKey();
                }
                List<string> tempRederlist = new List<string>();
                foreach (Reader record in borrowedListBooks)
                {
                    tempRederlist.Add($"{record.Name},{record.Author},{record.Year},{record.BookAmount},{record.ReaderName}");
                }
                ReaderWriter.WriteAllText(tempRederlist.ToArray());
            }
            

        }
        public List<Reader> ReadAllBorrowerFromFile()
        {
            var listReaders = new List<Reader>();
            List<string> alllinesFromfile = ReaderWriter.GetAllLines();
            for (int i = 0; i < alllinesFromfile.Count; i++)
            {
                Reader book = new Reader();
                string[] tepm = alllinesFromfile[i].Split(",");
                book.Name = tepm[0];
                book.Author = tepm[1];
                book.Year = Convert.ToInt32(tepm[2]);
                book.BookAmount = Convert.ToInt32(tepm[3]);
                book.ReaderName = tepm[4];
                listReaders.Add(book);
            }

            return listReaders;
        }

    }
}
