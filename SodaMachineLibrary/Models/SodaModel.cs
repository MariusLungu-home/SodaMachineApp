namespace SodaMachineLibrary.Models;

public class SodaModel
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SlotOccupied { get; set; }
}
