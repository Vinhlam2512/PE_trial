using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PE_trial2.Models;

using static PE_trial2.DTOs;

namespace PE_trial2.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase {

        private readonly PRN_Sum22_B1Context db;
        private readonly IMapper mapper;

        public OrderController(PRN_Sum22_B1Context _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAllOrder() { 
            List<Order> orders = db.Orders.Include(x => x.Customer).Include(x => x.Employee).ToList();
            List<OrderDTO> ordersDTO = mapper.Map<List<OrderDTO>>(orders);
            return Ok(ordersDTO);
        }

        [HttpGet("{from}/{to}")]
        public IActionResult GetAllOrderByDate(DateTime from, DateTime to)
        {
            List<Order> orders = db.Orders.Include(x => x.Customer).Include(x => x.Employee).Where(x => x.OrderDate >= from && x.OrderDate <= to).ToList();
            List<OrderDTO> ordersDTO = mapper.Map<List<OrderDTO>>(orders);
            return Ok(ordersDTO);
        }
    }
}
