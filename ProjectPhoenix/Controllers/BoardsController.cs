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
        private readonly IHttpContextAccessor _accessor;
        private ApplicationDbContext _context;
        public BoardsController (ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
            _context = context;
        }

        [HttpGet("seed/{quantity}")]

        public ActionResult Seed(int quantity)
        {
            var someu = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;

            var user = _accessor.HttpContext.User.Identity.Name;
            var appUser = _context.Users.First<ApplicationUser>(u => u.Id == someu);
            var result = BoardsDbInitializer.Seed(_context, quantity, appUser);
            return Ok(result);
        }
        [HttpGet("nuke")]
        public ActionResult DeleteAllBoardsByUser()
        {
            var user = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            var allBoardsByUser = _context.Boards.Where(b => b.user.Id == user);
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
            // List<String> boardNames = boards.Select<String>(b => b.name);
            return result;
        }

        public class BoardViewModel : BaseModel, IBoard
        {
            public BoardViewModel(Board board)
            {
                this.name = board.name;
                this.username = board.user.UserName;
                this.id = board.id;
                this.updateDate = board.updateDate;
                this.createDate = board.createDate;
                this.modifyDate = board.modifyDate;
            }
            public String name { get; set; }
            public string username { get; set; }
        }

        // GET api/<BoardsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BoardsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _context.Add(value);
            _context.SaveChangesAsync();
        }

        // PUT api/<BoardsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BoardsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
