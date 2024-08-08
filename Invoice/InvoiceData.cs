using System.Collections.Generic;
using Docs.Document;

namespace Docs.Invoice;

public class InvoiceData
{
	public string ShortName { get; set; }

	public Date StartDate { get; set; } = new();
	public int StartSeries { get; set; } = 1;

	public bool SelectableServices { get; set; }
	public List<Service> Services { get; set; } = new();

	public DocData OtherData { get; set; } = new();
}
