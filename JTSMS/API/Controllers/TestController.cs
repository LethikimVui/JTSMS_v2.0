using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext context;

        public TestController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet()]
        public List<Requestdetail> Index()
        {
            var Requestdetail = context.Requestdetail.AsNoTracking().ToList();
            var singleRequest = context.Requestdetail.Single(c => c.ReqId == 54);
            var selectWithContain = context.Requestdetail.AsEnumerable()
                .Where(c => Combine(c.AssemblyNumber, c.AssemblyRevision).Contains("P1095284-01 P"))
                .ToList();
            var t = context.Requestdetail.Include(c => c.IsActive).ToList();
            return Requestdetail;
        }
        static string Combine(string firstName, string lastName)
        {
            return (firstName + " " + lastName);
        }
    }
}
