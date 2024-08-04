using System.Collections.Generic;
using Docs.Document;

namespace Docs.Invoice;

public class GeneratedInvoiceData
{
	public int CurrentYear { get; set; }
	public int CurrentMonth { get; set; }
	public int CurrentSeries { get; set; }

	public List<Service> SelectedServices { get; set; } = new();
}
