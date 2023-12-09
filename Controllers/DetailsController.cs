using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApp.Models;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public DetailsController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detail>>> GetDetails()
        {
          if (_context.Details == null)
          {
              return NotFound();
          }
            return await _context.Details.ToListAsync();
        }

        //GET: api/Details/getByDate/17-12-2003
        [HttpGet("getByInvoiceDate/{dateInvoice}")]
        public List<Detail> GetByInvoiceDate(DateTime dateInvoice)
        {


            var details = _context.Details
                .Include(x => x.Invoice)
                .Include(x => x.Product)
                .Where(x => x.Invoice.Date.Date == dateInvoice.Date)
                .OrderBy(x => x.Invoice.Date)
                .OrderBy(x => x.Product)
                .ToList();

            return details;

        }

        //GET: api/Details/getByInvoiceNumber/2
        [HttpGet("getByInvoiceNumber/{invoiceNumber}")]
        public List<Detail> GetByInvoiceNumber(string invoiceNumber)
        {


            var details = _context.Details
                .Include(x => x.Invoice)
                .Include(x => x.Invoice.Customer)
                .Where(x => x.Invoice.InvoiceNumber.Contains(invoiceNumber))
                .OrderByDescending(x => x.Invoice.Customer.FirstName)
                .OrderByDescending(x => x.Invoice.InvoiceNumber)
                .ToList();

            return details;

        }

        // GET: api/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detail>> GetDetail(int id)
        {
          if (_context.Details == null)
          {
              return NotFound();
          }
            var detail = await _context.Details.FindAsync(id);

            if (detail == null)
            {
                return NotFound();
            }

            return detail;
        }

        // PUT: api/Details/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetail(int id, Detail detail)
        {
            if (id != detail.DetailId)
            {
                return BadRequest();
            }

            _context.Entry(detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Details
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detail>> PostDetail(Detail detail)
        {
          if (_context.Details == null)
          {
              return Problem("Entity set 'InvoiceContext.Details'  is null.");
          }
            _context.Details.Add(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetail", new { id = detail.DetailId }, detail);
        }

        // DELETE: api/Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            if (_context.Details == null)
            {
                return NotFound();
            }
            var detail = await _context.Details.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetailExists(int id)
        {
            return (_context.Details?.Any(e => e.DetailId == id)).GetValueOrDefault();
        }
    }
}
