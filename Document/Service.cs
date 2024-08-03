namespace Docs.Document;

public class Service
{
    public string Name { get; }
	public Date Date { get; }
	public decimal Price { get; }

	public Service(string name, Date date, decimal price)
	{
		Name = name;
		Date = date;
		Price = price;
	}
}
