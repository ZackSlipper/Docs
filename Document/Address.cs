namespace Docs.Document;

public class Address
{
	public string City { get; }
	public string Street { get; }
	public string Building { get; }

	public bool CityFirst { get; }

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
