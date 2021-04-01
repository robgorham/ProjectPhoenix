using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ProjectPhoenix.Data;
using ProjectPhoenix.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectPhoenix.Data.Initializers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectPhoenix.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BoardsController : ControllerBase
    {
        public class BoardViewModel : BaseModel, IBoard
        {
            public BoardViewModel(Board board)
            {
                this.name = board.name;
                this.username = board.user.UserName;
                this.id = board.id;
                this.createDate = board.createDate;
                this.modifyDate = board.modifyDate;
            }
            public string name { get; set; }
            public string username { get; set; }
        }

        private ApplicationDbContext _context;
        private ApplicationUser _user;
        public BoardsController (ApplicationDbContext context)
        {
            _context = context;
            
        }

        private void initUser()
        {
            var someu = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            _user = _context.Users.First<ApplicationUser>(u => u.Id == someu);
        }

        [HttpGet("seed/{quantity}")]

        public ActionResult Seed(int quantity)
        {
            initUser();
            var result = BoardsDbInitializer.Seed(_context, quantity, _user);
            return Ok(result);
        }


        [HttpGet("nuke")]
        public ActionResult DeleteAllBoardsByUser()
        {
            initUser();
            var allBoardsByUser = _context.Boards.Where(b => b.user.Id == _user.Id);
            _context.Boards.RemoveRange(allBoardsByUser);
            var result = _context.SaveChanges();
            return Ok(result);
        }
        [HttpGet("nukenull")]
        public ActionResult DeleteAllBoardsByNull()
        {
            var allBoardsByUser = _context.Boards
                                    .Where(b => b.user.Id == null);
            _context.Boards.RemoveRange(allBoardsByUser);
            var result = _context.SaveChanges();
            return Ok(result);
        }

        // GET: api/<BoardsController>
        [HttpGet]   
        public IEnumerable<BoardViewModel> Get()
        {
            List<Board> boards = _context.Boards
                                    .Include(board => board.user)
                                    .ToList<Board>();
            List<BoardViewModel> result = new List<BoardViewModel>();
            foreach(Board board in boards)
            {
                result.Add(new BoardViewModel(board));
            }

            return result;
        }

       

        // GET api/<BoardsController>/5
        [HttpGet("{id}")]
        public BoardViewModel Get(Guid id)
        {
            Board result = (Board)_context.Boards
                            .Include(board => board.user)
                            .Where(board => board.id == id)
                            .FirstOrDefault();

            return new BoardViewModel(result);
        }

        // POST api/<BoardsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            initUser();
            var added = new Board { createDate = DateTime.Now, modifyDate = DateTime.Now, id = Guid.NewGuid(), name = value, user = _user };
            _context.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<BoardsController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
            initUser();
            var result = _context.Boards
                            .First(board => board.id == id && board.user.Id == _user.Id);
            result.name = value;
            result.modifyDate = DateTime.Now;
            _context.SaveChanges();
        }

        // DELETE api/<BoardsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
