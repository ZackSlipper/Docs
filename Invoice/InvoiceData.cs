using System.Collections.Generic;
using Docs.Document;

namespace Docs.Invoice;

public class InvoiceData
{
	public string ShortName { get; set; }

	public Date StartDate { get; set; }
	public int StartSeries { get; set; }

	public Date LastDate { get; set; }

	public bool UsingDefaultServices { get; set; }
	public List<Service> DefaultServices { get; set; } = new();
	public List<Service> SelectableServices { get; set; } = new();

	public DocData OtherData { get; set; }
}
