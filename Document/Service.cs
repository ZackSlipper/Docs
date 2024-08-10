namespace Docs.Document;

public class Service
{
	public string Name { get; set; }
	public Date Date { get; set; } = new();
	public decimal Price { get; set; }

	public Service() { }

	public Service(string name, Date date, decimal price)
	{
		Name = name;
		Date = date;
		Price = price;
	}
}
