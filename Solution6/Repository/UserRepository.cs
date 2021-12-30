using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Solution6.Repository
{
    public class UserRepository
    {
        public UserRepository()
        {
            using (var db = new AppContext())
            {
                var user1 = new User { Name = "Arthur", Email = "arthur12@example.com", Role = "Admin"};
                var user2 = new User { Name = "Bob", Email = "bob56@gamil.com", Role = "Admin"};
                var user3 = new User { Name = "Clark", Email = "clarckkent83@example.com", Role = "User"};
                var user4 = new User { Name = "Dan", Email = "dan382@gmail.com", Role = "User"};
                
                db.Users.AddRange(user1, user2, user3, user4);

                db.SaveChanges();
            }
        }
        
        public async Task<bool> HasBook(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Users.AsQueryable();
            
            query = query.Where(x => x.Books!= null && x.Books.ToString() == filter.NameBook);
            
            var has = await query.AnyAsync(cancellationToken);

            return has;
        }
        
        public async Task<int> CountUser(BookFilter filter, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Users.AsQueryable();

            query = query.Where(x => x.Books.Count > 0);

            var count = await query.CountAsync(cancellationToken);

            return count;
        }

        public async Task<User> ChoiceUser(int id, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Users.AsQueryable();

            var choice = await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return choice;
        }

        public async Task<User[]> ChoiceAllUser(CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var query = db.Users.AsQueryable();

            var choiceAll = await query.ToArrayAsync(cancellationToken);

            return choiceAll;
        }

        public async Task<User> DeleteUser(int id, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var user = await ChoiceUser(id, cancellationToken);

            db.Users.Remove(user);

            await db.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> UpdateUser(int id, string userName, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var user = await ChoiceUser(id, cancellationToken);

            user.Name = userName;

            db.Users.Update(user);

            await db.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> AddUser(int id, CancellationToken cancellationToken)
        {
            await using var db = new AppContext();

            var user = await ChoiceUser(id, cancellationToken);

            db.Users.Add(user);

            await db.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}