public class CustomerComboItem
{
    public string Name { get; set; }
    public string Id { get; set; }
    public override string ToString() => $"{Id} - {Name}"; // Ensures only the name is shown
}
