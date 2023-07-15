namespace SodaMachineLibrary.Models
{
    public class Coin
    {
        private string _name;
        public int Id { get; set; }
        public double Value { get; set; }
        public string Name { get => _name; set => _name = value; }
        public int Quantity { get; set; }
    }
}
