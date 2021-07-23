using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPhoenix.Data;
using ProjectPhoenix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectPhoenix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCardsController : ControllerBase
    {
        private ApplicationDbContext _context;
        private string _user_id;

        public ItemCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void initUser()
        {
            _user_id = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
        }

        public class ItemCardGetDTOModel : BaseModel
        {
            public ItemCardGetDTOModel(ItemCard itemCard)
            {
                this.Name = itemCard.Name;
                this.id = itemCard.id;
                this.createDate = itemCard.createDate;
                this.modifyDate = itemCard.modifyDate;
                this.ColumnId = itemCard.Column.id;
                this.BoardId = itemCard.Board.id;
                this.Description = itemCard.Description;
                this.Order = itemCard.Order;
            }
            public string Name { get; set; }
            public Guid ColumnId { get; set; }
            public Guid BoardId { get; set; }
            public int Order { get; set; }
            public string Description { get; set; }

        }


        // GET api/<ItemCardsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            initUser();
            var card = _context.ItemCards
                .Include(card => card.User)
                .Include(card => card.Board)
                .Include(card => card.Column)
                .Where(card => card.User.Id == _user_id)
                .Where(card => card.id == id)
                .FirstOrDefault();
            return Ok(new ItemCardGetDTOModel(card));
        }

        [HttpGet("column/{id}")]
        public ActionResult GetByColumnId(Guid id)
        {
            initUser();

            var cards = _context.ItemCards
                        .Include(card => card.User)
                .Include(card => card.Board)
                .Include(card => card.Column)
                .Where(card => card.User.Id == _user_id)
                .Where(card => card.Column.id == id)
                .ToList<ItemCard>();
            var cardsDTOList = new List<ItemCardGetDTOModel>();
            foreach(ItemCard itemcard in cards)
            {
                cardsDTOList.Add(new ItemCardGetDTOModel(itemcard));
            }
            return Ok(cardsDTOList);
        }

        public class ItemCardPostDTOModel
        {
            public string Name { get; set; }
            public Guid ColumnId { get; set; }
            public Guid BoardId { get; set; }
        }

        // POST api/<ItemCardsController>
        [HttpPost]
        public ActionResult Post([FromBody] ItemCardPostDTOModel card)
        {
            initUser();
            var user = _context.Users.First<ApplicationUser>(u => u.Id == _user_id);
            Board board = (Board)_context.Boards
                            .Where(board => board.id == card.BoardId && board.user.Id == _user_id)
                            .FirstOrDefault();
            Column column = (Column)_context.Columns
                            .Include(column => column.user)
                            .Include(column => column.ItemCards)
                            .Where(column => column.id == card.ColumnId && column.user.Id == _user_id)
                            .FirstOrDefault();

            // #TODO: Must check to make sure the board and column are not null
            if(column.ItemCards is null)
            {
                column.ItemCards = new List<ItemCard>();
            }
            var addedCard = new ItemCard
            {
                Name = card.Name,
                Board = board,
                Column = column,
                User = user,
                createDate = DateTime.Now,
                modifyDate = DateTime.Now,
                Order = column.ItemCards.Count() + 1
            };
            column.ItemCards.Add(addedCard);
            column.modifyDate = DateTime.Now;
            board.modifyDate = DateTime.Now;
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

        // PUT api/<ItemCardsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemCardsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
