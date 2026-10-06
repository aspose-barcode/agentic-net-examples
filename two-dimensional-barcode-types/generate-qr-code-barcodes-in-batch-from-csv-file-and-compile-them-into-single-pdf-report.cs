// Title: Generate QR Code barcodes from CSV and compile into PDF report
// Description: Demonstrates reading values from a CSV file, creating QR Code barcodes for each entry, and assembling them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce QR codes, and Aspose.Pdf to embed images into a PDF report. Typical use cases include generating barcode catalogs, inventory sheets, or QR code based marketing materials where multiple barcodes need to be compiled into one document. Developers often need to read data sources, generate barcodes in memory, and combine them into PDFs for distribution.
// Prompt: Generate QR Code barcodes in batch from CSV file and compile them into a single PDF report.
// Tags: qr code, batch generation, csv, pdf, aspose.barcode, aspose.pdf, barcode generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that reads a CSV file, generates QR Code barcodes for each row,
/// and creates a PDF report containing the barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point for the QR Code batch generation example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Prepare a sample CSV file
        string csvPath = Path.Combine(workFolder, "data.csv");
        string[] csvLines = new[]
        {
            "Id,Value",
            "1,Hello World",
            "2,https://example.com",
            "3,Sample QR Code",
            "4,Extra Item"
        };
        File.WriteAllLines(csvPath, csvLines);

        // Read values from CSV (skip header)
        List<string> values = new List<string>();
        foreach (var line in File.ReadAllLines(csvPath))
        {
            // Ignore empty lines and the header row
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("Id,"))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length >= 2)
                values.Add(parts[1]);
        }

        // Limit to maximum 4 items for evaluation mode
        int count = Math.Min(values.Count, 4);
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate QR code images in memory
        for (int i = 0; i < count; i++)
        {
            string text = values[i];
            var ms = new MemoryStream();

            // Use BarcodeGenerator to create a QR code for the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            ms.Position = 0; // Reset stream position for later reading
            barcodeStreams.Add(ms);
        }

        // Create PDF report and embed barcode images
        string pdfPath = Path.Combine(workFolder, "BarcodeReport.pdf");
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                // Add the barcode image to the page, centered with a margin
                var img = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStreams[i],
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF document
            pdfDoc.Save(pdfPath);
        }

        // Dispose barcode streams after PDF is saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Output file locations for verification
        Console.WriteLine("CSV file: " + csvPath);
        Console.WriteLine("Generated PDF report: " + pdfPath);
    }
}