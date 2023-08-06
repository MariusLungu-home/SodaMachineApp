namespace SodaMachineLibrary.Models
{
    public class User
    {
        private string name;
        public string Id { get; set; }
        public string Name { get => name; set => name = value; }
        public double Balance { get; set; }
        public double TotalSpent { get; set; }
        public int TotalPurchases { get; set; }
        public bool IsAdmin { get; set; }
    }
}
