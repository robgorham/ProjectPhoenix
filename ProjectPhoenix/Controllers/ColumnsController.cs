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

    public class ColumnsController : ControllerBase
    {
        public class ColumnPutDTOModel : BaseModel
        {
            private IEnumerable<ItemCard> _cards = null;
            public ColumnPutDTOModel() { }
            public ColumnPutDTOModel(Column column)
            {
                this.name = column.name;
                this.id = column.id;
                this.createDate = column.createDate;
                this.modifyDate = column.modifyDate;
                this.itemCards = column?.ItemCards;
            }
            public string name { get; set; }
            public int order { get; set; }
            public Guid boardid { get; set; }

            public virtual IEnumerable<ItemCard> itemCards
            {
                get
                {
                    return this._cards;
                }
                set
                {
                    this._cards = value;
                }
            }
        }

        private ApplicationDbContext _context;
        private ApplicationUser _user;
        public ColumnsController(ApplicationDbContext context)
        {
            _context = context;
        }


        private void initUser()
        {
            var someu = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            _user = _context.Users.First<ApplicationUser>(u => u.Id == someu);
        }





        // GET api/<ColumnsController>/5
        [HttpGet("{id}")]
        public ActionResult<Column> Get(Guid id)
        {
            initUser();
            Column result = (Column)_context.Columns
                            .Include(column => column.ItemCards.OrderBy(c => c.Order))
                            .Where(column => column.id == id && column.user.Id == _user.Id)
                            .FirstOrDefault();
            return Ok(result);
        }

       // PUT api/<ColumnsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] ColumnPutDTOModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            initUser();
            var result = _context.Columns
                            .Include(column => column.ItemCards)
                            .Include(column => column.user)
                            .FirstOrDefault<Column>(column => column.id == id && column.user.Id == _user.Id);

            if (result is not null)
            {
                result.name = data.name;
                result.ItemCards = data.itemCards.ToList();
                result.modifyDate = DateTime.Now;
                var success = _context.SaveChanges();
                return Ok(success);

            }
            return NotFound(id);
        }

        // DELETE api/<ColumnsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var _user_id = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            Column result = _context.Columns
                                .Where(column => column.id == id && column.user.Id == _user_id)
                                .FirstOrDefault();
            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                var success = _context.SaveChanges();
                return Ok(success);

            }
            return NotFound(id);
        }

        [HttpPost("{id}")]
        public ActionResult AddColumnToBoardById(Guid id, [FromBody] ColumnPutDTOModel data)
        {
            var value = data.name;
            initUser();
            Board candidateBoard = _context.Boards
                           .Include(board => board.Columns)
                           .Where(board => board.id == id && board.user.Id == _user.Id)
                           .FirstOrDefault();
            if (candidateBoard is not null)
            {
                if (candidateBoard.Columns is null)
                {
                    candidateBoard.Columns = new List<Column>();
                }
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
                _context.Columns.Add(added);
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
