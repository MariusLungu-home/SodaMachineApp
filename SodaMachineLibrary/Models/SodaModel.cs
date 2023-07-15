namespace SodaMachineLibrary.Models;

public class SodaModel
{
    private string? _name;

    public int Id { get; set; }
    public double Price { get; set; }
    public string Name { get => _name; set => _name = value; }
    public string? Description { get; set; }
    public string? SlotOccupied { get; set; }
}
