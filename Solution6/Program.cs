using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solution6.Repository;

namespace Solution6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await using var context = new AppContext();
            
            context.Create();

            UserRepository userRepository = new UserRepository();

            BookRepository bookRepository = new BookRepository();

            var result = await bookRepository.LastBook(new BookFilter(null,null, null, null, null, 2022), CancellationToken.None);
            
            //context.Delete();
        }
    }
}