// Title: Generate QR Code barcodes from CSV and compile into PDF report
// Description: Demonstrates reading a CSV file, creating QR Code barcodes for each entry, and assembling them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to use BarcodeGenerator (Aspose.BarCode.Generation) together with Aspose.Pdf to produce QR Code images and embed them in a PDF report. Typical use cases include generating product labels, inventory sheets, or any scenario where multiple barcodes need to be compiled into a printable document. Developers often need to read data sources, generate barcodes with specific settings, and combine them into a final output format.
// Prompt: Generate QR Code barcodes in batch from CSV file and compile them into a single PDF report.
// Tags: qr code, barcode generation, batch processing, csv, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that reads a CSV file, generates QR Code barcodes for each entry,
/// and creates a PDF report containing the barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, prepares sample CSV data,
    /// generates QR Code images, embeds them into a PDF, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this run
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a sample CSV file with a few rows (max 5, but PDF will include only first 4)
        string csvPath = Path.Combine(tempFolder, "data.csv");
        string[] sampleData = new string[] { "Item001", "Item002", "Item003", "Item004", "Item005" };
        File.WriteAllLines(csvPath, sampleData);

        // Validate CSV existence
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found: " + csvPath);
            return;
        }

        // Read all lines from CSV
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0)
        {
            Console.WriteLine("CSV file is empty.");
            return;
        }

        // Limit to 4 items for PDF (rule 22)
        int maxItems = Math.Min(lines.Length, 4);
        var barcodeStreams = new List<MemoryStream>();

        // Generate QR code for each line and store in memory streams
        for (int i = 0; i < maxItems; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, lines[i]))
            {
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                // Optional: set barcode colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // reset for reading
                barcodeStreams.Add(ms);
            }
        }

        // Create PDF and embed each barcode image
        string pdfPath = Path.Combine(tempFolder, "BarcodesReport.pdf");
        using (var pdfDoc = new Aspose.Pdf.Document())
        {
            foreach (var stream in barcodeStreams)
            {
                var page = pdfDoc.Pages.Add();
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = stream,
                    FixWidth = 150,
                    FixHeight = 150,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };
                page.Paragraphs.Add(pdfImage);
            }

            pdfDoc.Save(pdfPath);
        }

        // Dispose barcode streams after PDF is saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine("PDF report generated at:");
        Console.WriteLine(pdfPath);
    }
}