using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PE_trial2.Models;

namespace PE_trial2.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly PRN_Sum22_B1Context db;
        //private readonly IMapper mapper;

        public CustomerController(PRN_Sum22_B1Context _db)
        {
            db = _db;
            //mapper = _mapper;
        }

        [HttpPost("{CustomerId}")]
        public IActionResult Delete(string CustomerId)
        {
            try
            {
                int countDeletedCustomer = 0;
                int countDeletedOrder = 0;
                int countDeletedOrderDetail = 0;

                Customer? c = db.Customers.FirstOrDefault(x => x.CustomerId == CustomerId);
                if (c == null)
                {
                    return StatusCode(404, null);
                }
                else
                {
                    List<Order> orders = db.Orders.Where(x => x.CustomerId == CustomerId).ToList();
                    foreach (Order order in orders)
                    {
                        List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                        db.RemoveRange(orderDetails);
                        countDeletedOrderDetail += orderDetails.Count;
                    }
                    db.RemoveRange(orders);
                    countDeletedOrder += orders.Count;
                    db.Remove(c);
                    countDeletedCustomer++;
                    db.SaveChanges();
                    return Ok(new { countDeletedCustomer = countDeletedCustomer, countDeletedOrder = countDeletedOrder, countDeletedOrderDetail = countDeletedOrderDetail });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(409, "loi");
            }
        }
    }
}
