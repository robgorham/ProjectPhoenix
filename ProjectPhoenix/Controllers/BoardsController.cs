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
        public class BoardDTO : BaseModel
        {
            public BoardDTO(Board board)
            {
                this.name = board.name;
                this.id = board.id;
                this.createDate = board.createDate;
                this.modifyDate = board.modifyDate;
                this.columns = board?.Columns;
            }
            public string name { get; set; }
            public string username { get; set; }
            public IList<Column> columns { get; set; }
        }


        private ApplicationDbContext _context;
        private string _user_id;
        public BoardsController(ApplicationDbContext context)
        {
            _context = context;
        }


        private void initUser()
        {
            _user_id = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
        }


        [HttpGet("seed/{quantity}")]

        public ActionResult Seed(int quantity)
        {

            var _user = _context.Users.First<ApplicationUser>(u => u.Id == _user_id);
            var result = BoardsDbInitializer.Seed(_context, quantity, _user);
            return Ok(result);
        }


        [HttpGet("nuke")]
        public ActionResult DeleteAllBoardsByUser()
        {
            initUser();
            var allBoardsByUser = _context.Boards.Where(b => b.user.Id == _user_id);
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
            var uid = Guid.Parse(_user_id);
            List<Board> boards = _context.Boards
                                    .Include(board => board.Columns)
                                    .ThenInclude(column => column.ItemCards)
                                    .Where(board => board.user.Id == _user_id)
                                    .ToList();
            List<BoardDTO> result = new List<BoardDTO>();
            foreach (Board board in boards)
            {
                result.Add(new BoardDTO(board));
            }

            if (result.Count() == 0)
            {
                return Array.Empty<BoardDTO>();
            }
            return result;
        }



        // GET api/<BoardsController>/5
        [HttpGet("{id}")]
        public BoardDTO Get(Guid id)
        {
            _user_id = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            Board result = (Board)_context.Boards
                            .Include(board => board.Columns.OrderBy(c => c.order))
                            .ThenInclude(column => column.ItemCards.OrderBy(i => i.Order))
                            .Include(board => board.user)
                            .Where(board => board.id == id && board.user.Id == _user_id)
                            .FirstOrDefault();
            return new BoardDTO(result);
        }

        // POST api/<BoardsController>
        [HttpPost]
        public void Post([FromBody] PutModel data)
        {
            var value = data.name;
            initUser();
            var _user = _context.Users.First<ApplicationUser>(u => u.Id == _user_id);
            var added = new Board { createDate = DateTime.Now, modifyDate = DateTime.Now, id = Guid.NewGuid(), name = value, user = _user };
            _context.Add(added);
            _context.SaveChanges();
        }

        public class PutModel
        {
            public PutModel() { }
            public string name { get; set; }
            public Board board { get; set; } = null;
        }
        // PUT api/<BoardsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] PutModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            initUser();

            var currBoard = _context.Boards
                            .Include(board => board.Columns.OrderBy(c => c.order))
                            .FirstOrDefault<Board>(board => board.id == id && board.user.Id == _user_id);

            if (currBoard is not null)
            {
                try
                {
                    currBoard.name = data.board.name;
                    currBoard.modifyDate = DateTime.Now;
                    for (var i = 0; i < currBoard.Columns.Count(); i++)
                    {
                        var currCol = currBoard.Columns[i];
                        var inCol = data.board.Columns.Where(varcol => varcol.id == currCol.id).FirstOrDefault();
                        currCol.order = inCol.order;
                        currCol.modifyDate = DateTime.Now;
                    }

                    var success = _context.SaveChanges();
                    return Ok(success);
                }
                catch (Exception er)
                {
                    return Ok(er.Message);
                }

            }
            return NotFound(id);
        }

        // DELETE api/<BoardsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            initUser();
            var result = _context.Boards
                            .FirstOrDefault<Board>(board => board.id == id && board.user.Id == _user_id);
            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                var success = _context.SaveChanges();
                return Ok(success);

            }
            return NotFound(id);
        }

        [HttpPost("{id}/columns")]
        public ActionResult AddColumnToBoardById(Guid id, [FromBody] PutModel data)
        {
            var value = data.name;
            initUser();
            Board candidateBoard = _context.Boards
                           .Include(board => board.Columns)
                           .Where(board => board.id == id && board.user.Id == _user_id)
                           .FirstOrDefault();
            if (candidateBoard is not null)
            {
                if (candidateBoard.Columns is null)
                {
                    candidateBoard.Columns = new List<Column>();
                }
                var _user = _context.Users.First<ApplicationUser>(u => u.Id == _user_id);
                var added = new Column
                {
                    BoardId = candidateBoard.id,
                    order = candidateBoard.Columns.Count() + 1,
                    createDate = DateTime.Now,
                    modifyDate = DateTime.Now,
                    name = value,
                    user = _user
                };
                candidateBoard.Columns.Add(added);
                candidateBoard.modifyDate = DateTime.Now;
                _context.Entry<Board>(candidateBoard).State = EntityState.Modified;
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
            return NotFound(id);
        }


    }
}
