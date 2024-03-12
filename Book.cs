namespace Lab1
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }

        public Book(string title, string author, int year, string genre, string isbn)
        {
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
            ISBN = isbn;
        }
    }
}