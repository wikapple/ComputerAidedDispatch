using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;

namespace ComputerAidedDispatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallCommentsController : ControllerBase
    {
        private readonly ComputerAidedDispatchContext _context;

        public CallCommentsController(ComputerAidedDispatchContext context)
        {
            _context = context;
        }

        // GET: api/CallComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallComment>>> GetCallComments()
        {
            return await _context.CallComments.ToListAsync();
        }

        // GET: api/CallComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CallComment>> GetCallComment(int id)
        {
            var callComment = await _context.CallComments.FindAsync(id);

            if (callComment == null)
            {
                return NotFound();
            }

            return callComment;
        }

        // PUT: api/CallComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCallComment(int id, CallComment callComment)
        {
            if (id != callComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(callComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallCommentExists(id))
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

        // POST: api/CallComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CallComment>> PostCallComment(CallComment callComment)
        {
            _context.CallComments.Add(callComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCallComment", new { id = callComment.Id }, callComment);
        }

        // DELETE: api/CallComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCallComment(int id)
        {
            var callComment = await _context.CallComments.FindAsync(id);
            if (callComment == null)
            {
                return NotFound();
            }

            _context.CallComments.Remove(callComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CallCommentExists(int id)
        {
            return _context.CallComments.Any(e => e.Id == id);
        }
    }
}
