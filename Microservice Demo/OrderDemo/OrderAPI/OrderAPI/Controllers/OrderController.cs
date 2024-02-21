using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> orders;
        public OrderController()
        {
            orders = new List<Order>
            {
                new Order()
                {
                    Id = 1,
                    Name = "Test 1",
                    Description = "Description Test 1"
                },

                new Order()
                {
                    Id = 2,
                    Name = "Test 2",
                    Description = "Description Test 2"
                },

                new Order()
                {
                    Id = 3,
                    Name = "Test 3",
                    Description = "Description Test 3"
                }
            };
        }
        [Route("get")]
        public List<Order> Get()
        {
            return orders;
        }

        [Route("get/{id}")]
        public Order? Get(int id)
        {
            return orders.Where(col=>col.Id == id).FirstOrDefault();
        }
    }
}
