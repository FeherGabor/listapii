using Microsoft.AspNetCore.Mvc;
using ListManagerApi.Models;
using ListManagerApi.Services;

namespace ListManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly FileStorageService _storage;

    public ItemsController(FileStorageService storage)
    {
        _storage = storage;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var items = _storage.LoadItems();
        return Ok(items);
    }

    [HttpPost]
    public IActionResult Post(Item item)
    {
        var items = _storage.LoadItems();
        item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
        items.Add(item);
        _storage.SaveItems(items);
        return Ok(item);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Item updatedItem)
    {
        var items = _storage.LoadItems();
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();
        item.Name = updatedItem.Name;
        _storage.SaveItems(items);
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var items = _storage.LoadItems();
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();
        items.Remove(item);
        _storage.SaveItems(items);
        return NoContent();
    }
}
