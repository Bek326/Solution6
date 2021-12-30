using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Solution6.Repository
{
    public class BookRepository
    {
        public BookRepository()
        {
            using (var db = new AppContext())
            {
                var book1 = new Book { NameOfBook = "BasketballOfFuture", YearOfBook = 1996, GenreOfBook = "SportsAndPsychology", AuthorBook = "J.West", UserId = 1};
                var book2 = new Book { NameOfBook = "WarOrPeace", YearOfBook = 1873, GenreOfBook = "Literature", AuthorBook = "L.N.Tolstoy", UserId = 2};
                var book3 = new Book { NameOfBook = "HistoryOfWallStreet", YearOfBook = 2001, GenreOfBook = "History", AuthorBook = "Charles Geisst", UserId = 3};
                var book4 = new Book { NameOfBook = "LiveInRussia", YearOfBook = 2013, GenreOfBook = "History", AuthorBook = "Alexander Zaborov", UserId = 4};
                
                db.Books.AddRange(book1, book2, book3, book4);

                db.SaveChanges();
            }
            
        }

        public async Task<Book[]> GetByFilter(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();
            
            var query = db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Genre))
            {
                query = query.Where(x => x.GenreOfBook == filter.Genre);
            }

            if (filter.YearFrom.HasValue)
            {
                query = query.Where(x => x.YearOfBook == filter.YearFrom.Value);
            }
                
            if (filter.YearTo.HasValue)
            {
                query = query.Where(x => x.YearOfBook == filter.YearTo.Value);
            }

            var books = await query.ToArrayAsync(cancellationToken);

            return books;
        }

        public async Task<int> CountBooks(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();
            
            if (!string.IsNullOrEmpty(filter.Author))
            {
                query = query.Where(x => x.AuthorBook == filter.Author);
            }

            var count = await query.CountAsync(cancellationToken);

            return count;
        }

        public async Task<int> AmountBooks(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Genre))
            {
                query = query.Where(x => x.GenreOfBook == filter.Genre);
            }

            var amount = await query.CountAsync(cancellationToken);

            return amount;
        }

        public async Task<bool> HasBook(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Author))
            {
                query = query.Where(x => x.AuthorBook == filter.Author);
            }

            if (!string.IsNullOrEmpty(filter.NameBook))
            {
                query = query.Where(x => x.NameOfBook == filter.NameBook);
            }

            var has = await query.AnyAsync(cancellationToken);

            return has;
        }

        public async Task<Book> LastBook(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();

            query = query.Where(x => x.YearOfBook == filter.YearBook);

            var last = await query.LastAsync(cancellationToken);

            return last;
        }

        public async Task<Book[]> SortName(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();

            var sorted = await query.OrderBy(x => x.NameOfBook == filter.NameBook).ToArrayAsync(cancellationToken);

            return sorted;
        }

        public async Task<Book[]> SortYear(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();
            
            var sort = await query.OrderByDescending(x => x.YearOfBook == filter.YearBook).ToArrayAsync(cancellationToken);

            return sort;
        }

        public async Task<Book> ChoiceBook(int id, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();

            var choice = await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return choice;
        }
        
        public async Task<Book[]> ChoiceAllBook(CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Books.AsQueryable();

            var choiceAll = await query.ToArrayAsync(cancellationToken);

            return choiceAll;
        }

        public async Task<Book> DeleteBook(int id, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var book = await ChoiceBook(id, cancellationToken);

            db.Books.Remove(book);

            await db.SaveChangesAsync(cancellationToken);

            return book;
        }

        public async Task<Book> UpdateBook(int id,int yearBook, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var book = await ChoiceBook(id, cancellationToken);

            book.YearOfBook = yearBook;

            db.Books.Update(book);

            await db.SaveChangesAsync(cancellationToken);

            return book;
        }

        public async Task<Book> AddBook(int id, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var book = await ChoiceBook(id, cancellationToken);

            db.Books.Add(book);

            await db.SaveChangesAsync(cancellationToken);

            return book;
        }
    }
}