/**
 * Initializes the Database for Boards
 * example:
 * https://www.entityframeworktutorial.net/code-first/seed-database-in-code-first.aspx
 * 
 * 
**/
using ProjectPhoenix.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Data.Initializers
{
    public class BoardsDbInitializer
    {
        public static int Seed(ApplicationDbContext context, int quantity, ApplicationUser appUser)
        {
           
            IList<Board> boards = new List<Board>();
            var now = DateTime.Now;
            for(int x = 0; x < quantity; x++)
            {
                boards.Add(new Board { createDate = now, modifyDate = now, name = "Rinzler", id = Guid.Empty, user = appUser });

            }
            context.Boards.AddRange(boards);
            return context.SaveChanges();
            
        }
    }
}
