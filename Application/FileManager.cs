using System;
using System.Collections.Generic;
using System.IO;
using Docs.Invoice;
using Godot;
using Newtonsoft.Json;
using Environment = System.Environment;

namespace Docs.Application;

public class FileManager
{
	private static string StorageDirectory { get; } = 
		$"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Docs";

	private static string InvoiceDataPath { get; } = $"{StorageDirectory}/InvoiceData.json";

	static FileManager() => ValidateDirectories();

	private static bool ValidateDirectories()
	{
		try
		{
			if (!Directory.Exists(StorageDirectory))
			{
				Directory.CreateDirectory(StorageDirectory);
				GD.Print($"Created missing storage directory at: {StorageDirectory}");
			}
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Failed to create directories: {ex.Message}");
			return false;
		}
		return true;
	}

	public static void SaveInvoiceData(List<InvoiceData> invoices)
	{
		if (!ValidateDirectories())
			throw new("Failed to validate directories");

		try
		{
			string json = JsonConvert.SerializeObject(invoices, Formatting.Indented);
			File.WriteAllText(InvoiceDataPath, json);
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Failed to save invoice data: {ex.Message}");
		}
	}

	public static List<InvoiceData> LoadInvoiceData()
	{
		if (!ValidateDirectories())
			throw new("Failed to validate directories");

		if (!File.Exists(InvoiceDataPath))
			return new();

		try
		{
			string json = File.ReadAllText(InvoiceDataPath);
			return JsonConvert.DeserializeObject<List<InvoiceData>>(json);
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Failed to load settings: {ex.Message}");
			return new();
		}
	}
}
