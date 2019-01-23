using System;
using System.Collections.Generic;
using APIFinances.Contexts;
using APIFinances.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIFinances.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly APIFinancesDbContext _context;
        public UserController(APIFinancesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<User> Get()
        {
            return _context.Users.Find();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            User user = new User();
            user.Username = "matheuspaz";
            user.Password = "teste";
            user.Token = "89j129dj89ja9s9d8jas";

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}