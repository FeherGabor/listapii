using System.Text.Json;
using ListManagerApi.Models;

namespace ListManagerApi.Services;

public class FileStorageService
{
    private readonly string filePath = "Data/items.json";

    public List<Item> LoadItems()
    {
        if (!File.Exists(filePath))
            return new List<Item>();

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
    }

    public void SaveItems(List<Item> items)
    {
        var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}
