// Title: Batch QR Code Generation from CSV and PDF Report
// Description: Demonstrates reading a CSV file, generating QR code images for each entry, and compiling them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf composition category. It showcases how to use BarcodeGenerator (Aspose.BarCode.Generation) to create QR codes, configure their appearance, and embed the resulting images into a PDF (Aspose.Pdf) as separate pages with captions. Developers often need to automate barcode creation from data sources and produce printable reports, making this pattern common for inventory, marketing, or tracking solutions.
// Prompt: Generate QR Code barcodes in batch from CSV file and compile them into a single PDF report.
// Tags: qr code, batch generation, csv, pdf, aspose.barcode, aspose.pdf, barcode generation, image export

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch creation of QR code barcodes from a CSV file and assembling them into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads sample CSV data, generates QR codes, and creates a PDF document.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for intermediate files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Prepare a sample CSV file (Id,Text) in the working directory
        string csvPath = Path.Combine(workDir, "data.csv");
        var sampleData = new[]
        {
            "1,https://example.com/item1",
            "2,https://example.com/item2",
            "3,https://example.com/item3",
            "4,https://example.com/item4",
            "5,https://example.com/item5"
        };
        File.WriteAllLines(csvPath, sampleData);

        // Read CSV lines and parse them into (Id, Text) tuples
        var rows = new List<(string Id, string Text)>();
        foreach (var line in File.ReadAllLines(csvPath))
        {
            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length == 2)
            {
                rows.Add((parts[0].Trim(), parts[1].Trim()));
            }
        }

        // Limit to the first 4 rows for PDF evaluation mode
        var limitedRows = rows.Take(4).ToList();

        // Generate QR code images and store them in memory streams
        var imageStreams = new List<MemoryStream>();
        foreach (var row in limitedRows)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, row.Text))
            {
                // Configure QR code error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                // Set module (pixel) size
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Define foreground and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                imageStreams.Add(ms);
            }
        }

        // Create a PDF document and add each QR code image on a separate page
        string pdfPath = Path.Combine(workDir, "BarcodesReport.pdf");
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < imageStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                // Insert the QR code image
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = imageStreams[i],
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };
                page.Paragraphs.Add(pdfImage);

                // Add a caption below the image showing the item Id
                var caption = new TextFragment($"Item ID: {limitedRows[i].Id}")
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 10 }
                };
                page.Paragraphs.Add(caption);
            }

            // Save the assembled PDF report to disk
            pdfDoc.Save(pdfPath);
        }

        // Release memory streams after the PDF has been saved
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF report generated at: {pdfPath}");
    }
}