namespace WindowsFormsAppLearn
{
    public class Item
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public byte[] Image { get; set; }

        public Item() { }

        public Item(string code, string name, string description, int quantity, double unitPrice, byte[] image)
        {
            Code = code;
            Name = name;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Image = image;
        }
    }
}
