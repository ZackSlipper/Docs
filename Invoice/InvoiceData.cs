using System.Collections.Generic;
using Docs.Document;
using Newtonsoft.Json;

namespace Docs.Invoice;

public class InvoiceData
{
	public string ShortName { get; set; }

	public Date StartDate { get; set; } = new();
	public int StartSeries { get; set; } = 1;

	public List<Service> Services { get; set; } = new();
	[JsonIgnore] public List<Service> SelectedServices { get; set; } = new();

	public DocData OtherData { get; set; } = new();
}
