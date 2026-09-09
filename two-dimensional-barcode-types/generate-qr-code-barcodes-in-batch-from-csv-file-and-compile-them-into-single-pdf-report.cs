// Title: Batch QR Code Generation from CSV and PDF Report Creation
// Description: Demonstrates reading a CSV file, generating QR code images for each record, and compiling up to four codes into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to create QR codes from data sources, store them in memory streams, and then embed the images into a PDF using Aspose.Pdf. Typical scenarios include generating barcode reports, inventory labels, or QR‑code based documentation where multiple codes need to be aggregated into a single file.
// Prompt: Generate QR Code barcodes in batch from CSV file and compile them into a single PDF report.
// Tags: qr code, batch generation, csv, pdf, aspose.barcode, aspose.pdf, barcode generation, image export

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that reads a CSV file, creates QR code images for each entry,
/// and assembles a PDF report containing up to four QR codes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder for all generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Prepare a sample CSV file with an Id column and a Value column.
        string csvPath = Path.Combine(workFolder, "data.csv");
        File.WriteAllText(csvPath,
            "Id,Value\n" +
            "1,Hello World\n" +
            "2,https://example.com\n" +
            "3,1234567890\n" +
            "4,Sample QR Code");

        // Read the CSV file line by line, generate a QR code for each Value, and store the images in memory streams.
        var barcodeStreams = new List<MemoryStream>();
        using (var reader = new StreamReader(csvPath))
        {
            // Skip the header row.
            string line = reader.ReadLine();

            // Process each subsequent line.
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length < 2) continue; // Ensure there is a Value column.

                string codeText = parts[1]; // Text to encode in the QR code.

                var ms = new MemoryStream();
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Configure QR code appearance.
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Save the generated QR code as PNG into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
                ms.Position = 0; // Reset stream position for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // Limit the number of QR codes added to the PDF to a maximum of four, as per example guidelines.
        int maxItems = Math.Min(4, barcodeStreams.Count);

        // Create a PDF document and add each QR code image to its own page.
        string pdfPath = Path.Combine(workFolder, "QrReport.pdf");
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < maxItems; i++)
            {
                var page = pdfDoc.Pages.Add();

                // Configure the image object with size, alignment, and margin.
                var img = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStreams[i],
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF to disk.
            pdfDoc.Save(pdfPath);
        }

        // Release all memory streams now that the PDF has been saved.
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF report was generated.
        Console.WriteLine("PDF report generated at:");
        Console.WriteLine(pdfPath);
    }
}