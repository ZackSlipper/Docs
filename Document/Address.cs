namespace Docs.Document;

public class Address
{
	public string City { get; set; }
	public string Street { get; set; }
	public string Building { get; set; }

	public bool CityFirst { get; set; }

	public Address() { }

	public Address(string city, string street, string building, bool cityFirst = true)
	{
		City = city;
		Street = street;
		Building = building;
		CityFirst = cityFirst;
	}

	public override string ToString() =>
		$"{Street} g. {Building}, {City}";
}
