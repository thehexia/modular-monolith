namespace PayYourChart.Module.Item;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(long id) : base($"Could not find item with specified id {id}.") { }
    public ItemNotFoundException(string itemCode) : base($"Could not find item with specified code {itemCode}.") { }
}
