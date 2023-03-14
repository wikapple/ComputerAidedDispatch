using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallsForServiceController : ControllerBase
{
    private readonly ComputerAidedDispatchContext _CadDb;

    public CallsForServiceController(ComputerAidedDispatchContext context)
    {
        _CadDb = context;
    }

    // GET: api/CallsForService
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CallForService>>> GetCallsForService()
    {
        return await _CadDb.CallsForService.ToListAsync();
    }

    // GET: api/CallsForService/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CallForService>> GetCallForService(int id)
    {

        var callForService = await _CadDb.CallsForService.FindAsync(id);

        if (callForService == null)
        {
            return NotFound();
        }

        return callForService;
    }

    // PUT: api/CallsForService/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCallForService(int id, CallForService callForService)
    {
        if (id != callForService.Id)
        {
            return BadRequest();
        }

        _CadDb.Entry(callForService).State = EntityState.Modified;

        try
        {
            await _CadDb.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CallForServiceExists(id))
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

    // POST: api/CallsForService
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<CallForService>> PostCallForService(CallForService callForService)
    {
        _CadDb.CallsForService.Add(callForService);
        await _CadDb.SaveChangesAsync();

        return CreatedAtAction("GetCallForService", new { id = callForService.Id }, callForService);
    }

    // DELETE: api/CallsForService/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCallForService(int id)
    {
        var callForService = await _CadDb.CallsForService.FindAsync(id);
        if (callForService == null)
        {
            return NotFound();
        }

        _CadDb.CallsForService.Remove(callForService);
        await _CadDb.SaveChangesAsync();

        return NoContent();
    }

    private bool CallForServiceExists(int id)
    {
        return _CadDb.CallsForService.Any(e => e.Id == id);
    }
}
