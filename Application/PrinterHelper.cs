using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Docs.Application;

public static class PrinterHelper
{
	public static void PrintDocument(string documentPath, string printerName)
	{
		string pdfPath = ConvertDocxToPdf(documentPath);
		if (!string.IsNullOrEmpty(pdfPath))
			PrintPdf(pdfPath, printerName);

		File.Delete(pdfPath);
	}

	private static string ConvertDocxToPdf(string docxPath)
	{
		string newFileName =
			$"{Path.GetFileNameWithoutExtension(docxPath)}_{(ulong)Random.Shared.NextInt64()}";
		string documentPath = Path.Combine(Path.GetDirectoryName(docxPath), $"{newFileName}.docx");

		File.Copy(docxPath, documentPath, true);

		string pdfPath = Path.ChangeExtension(documentPath, ".pdf");
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "libreoffice",
			Arguments = $"--headless --convert-to pdf --outdir \"{Path.GetDirectoryName(documentPath)}\" \"{documentPath}\"",
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		using Process process = Process.Start(startInfo);
		process.WaitForExit();
		if (process.ExitCode == 0)
		{
			File.Delete(documentPath);
			return pdfPath;
		}
		throw new Exception($"Error converting DOCX to PDF. Exit code: {process.ExitCode}");
	}

	private static void PrintPdf(string pdfPath, string printerName)
	{
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "lp",
			Arguments = $"-d {printerName} \"{pdfPath}\"",
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		using Process process = Process.Start(startInfo);
		process.WaitForExit();
		if (process.ExitCode != 0)
			throw new Exception($"Error printing PDF. Exit code: {process.ExitCode}");
	}

	public static string[] ListPrinters()
	{
		try
		{
			ProcessStartInfo startInfo = new()
			{
				FileName = "lpstat",
				Arguments = "-p",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			using Process process = Process.Start(startInfo);
			using StreamReader reader = process.StandardOutput;
			return ParseAndPrintPrinters(reader.ReadToEnd());
		}
		catch (Exception ex)
		{
			Global.ViewController.ShowView("error", ex);
			return [];
		}
	}

	private static readonly char[] Separators = ['\n', '\r'];

	private static string[] ParseAndPrintPrinters(string lpstatOutput)
	{
		List<string> printers = [];
		string[] lines = lpstatOutput.Split(Separators,
			StringSplitOptions.RemoveEmptyEntries);

		foreach (string line in lines)
		{
			if (!line.StartsWith("printer"))
				continue;

			string[] parts = line.Split(' ');
			if (parts.Length > 1)
				printers.Add(parts[1]);
		}

		return [.. printers];
	}
}
