using System.Collections.Generic;

namespace Solution6
{
    public class Book
    {
        public int Id { get; set; }
        public string NameOfBook { get; set; }
    
        public int YearOfBook { get; set; }
        
        public string GenreOfBook { get; set; }
        
        public string AuthorBook { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; }
    }
}