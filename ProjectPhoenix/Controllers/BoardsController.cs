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
        public class BoardDTO : BaseModel, IBoard
        {
            public BoardDTO(Board board)
            {
                this.name = board.name;
                this.username = board.user.UserName;
                this.id = board.id;
                this.createDate = board.createDate;
                this.modifyDate = board.modifyDate;
                this.Columns = board?.Columns;
            }
            public string name { get; set; }
            public string username { get; set; }
            public Column[] Columns { get; set; }
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
            var allBoardsByNull = _context.Boards
                                    .Where(b => b.user.Id == null);
            _context.Boards.RemoveRange(allBoardsByNull);
            var result = _context.SaveChanges();
            return Ok(result);
        }

        // GET: api/<BoardsController>
        [HttpGet]   
        public IEnumerable<BoardDTO> Get()
        {
            initUser();
            List<Board> boards = _context.Boards
                                    .Include(board => board.user)
                                    .Where(board => board.user.Id == _user.Id)
                                    .ToList<Board>();
            List<BoardDTO> result = new List<BoardDTO>();
            foreach(Board board in boards)
            {
                result.Add(new BoardDTO(board));
            }

            return result;
        }

       

        // GET api/<BoardsController>/5
        [HttpGet("{id}")]
        public BoardDTO Get(Guid id)
        {
            initUser();
            Board result = (Board)_context.Boards
                            .Include(board => board.user)
                            .Where(board => board.id == id && board.user.Id == _user.Id)
                            .FirstOrDefault();

            return new BoardDTO(result);
        }

        // POST api/<BoardsController>
        [HttpPost]
        public void Post([FromBody] PutModel data)
        {
            var value = data.name;
            initUser();
            var added = new Board { createDate = DateTime.Now, modifyDate = DateTime.Now, id = Guid.NewGuid(), name = value, user = _user };
            _context.Add(added);
            _context.SaveChanges();
        }

        public class PutModel {
            public PutModel() { }
            public string name { get; set; }
        }
        // PUT api/<BoardsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] PutModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            initUser();
            var result = _context.Boards
                            .FirstOrDefault<Board>(board => board.id == id && board.user.Id == _user.Id);
            
            if(result != null)
            {
                result.name = data.name;
                result.modifyDate = DateTime.Now;
                var success = _context.SaveChanges();
                return Ok(success);

            }
            return NotFound();
        }

        // DELETE api/<BoardsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            initUser();
            var result = _context.Boards
                            .FirstOrDefault<Board>(board => board.id == id && board.user.Id == _user.Id);
            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                var success = _context.SaveChanges();
                return Ok(success);

            }
            return NotFound();
        }

        [HttpPost("{id}/columns")]
        public ActionResult AddColumn(Guid id, [FromBody] PutModel data)
        {
            var value = data.name;
            initUser();
            Board result = (Board)_context.Boards
                           .Include(board => board.Columns)
                           .Where(board => board.id == id && board.user.Id == _user.Id)
                           .FirstOrDefault();
            if (result is not null)
            {

            }
            var added = new Column { board= result, order=result.Columns.Length + 1, createDate = DateTime.Now, modifyDate = DateTime.Now, id = Guid.NewGuid(), name = value, user = _user };
            result.Columns[result.Columns.Length] = added;
            _context.Columns.Add(added);
            _context.Entry<Board>(result).State = EntityState.Modified;
            try
            {
                var success = _context.SaveChanges();
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    
    }
}
