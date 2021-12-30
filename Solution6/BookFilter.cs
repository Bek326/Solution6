namespace Solution6
{
    public class BookFilter
    {
        public BookFilter(string genre, int? yearFrom, int? yearTo, string author, string nameBook, int yearBook)
        {
            Genre = genre;
            YearTo = yearTo;
            YearFrom = yearFrom;
            Author = author;
            NameBook = nameBook;
            YearBook = yearBook;
        }

        public int? YearFrom {get;}

        public int? YearTo { get; }
        
        public int? YearBook { get; }

        public string Genre { get; }
        
        public string Author { get; }
        
        public string NameBook { get; }
    }
}