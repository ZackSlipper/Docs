namespace Docs.Document;

public class Address
{
	public string City { get; set; }
	public string PostalCode { get; set; }
	public string Street { get; set; }
	public string Building { get; set; }

	public Address() { }

	public Address(string city, string street, string building)
	{
		City = city;
		Street = street;
		Building = building;
	}

	public override string ToString() =>
		$"{Street.Trim()} g. {Building.Trim()}, {(string.IsNullOrWhiteSpace(PostalCode) ? "" : $"{PostalCode} ")}{City.Trim()}";
}
