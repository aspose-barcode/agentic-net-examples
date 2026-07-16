// Title: Batch QR Code Generation from CSV to PDF
// Description: Demonstrates reading values from a CSV file, generating QR code barcodes for each entry, and compiling them into a single PDF report.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf batch processing category. It showcases how to use BarcodeGenerator (Aspose.BarCode.Generation) to create QR codes and Aspose.Pdf (Document, Image) to embed those images into a PDF. Typical use cases include generating product labels, inventory reports, or any scenario where multiple barcodes need to be rendered together. Developers often need to read data sources, create barcodes in memory, and produce consolidated documents for printing or distribution.
// Prompt: Generate QR Code barcodes in batch from CSV file and compile them into a single PDF report.
// Tags: qr code, batch generation, pdf, aspose.barcode, aspose.pdf, csv, barcode generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates QR code barcodes from a CSV file and compiles them into a single PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads CSV data, creates QR codes, and saves a PDF document.
    /// </summary>
    static void Main()
    {
        // Define file paths for input CSV and output PDF
        const string csvPath = "data.csv";
        const string pdfPath = "Report.pdf";

        // Ensure the CSV file exists; create a sample file if it does not
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("Id,Value");
                writer.WriteLine("1,HelloWorld");
                writer.WriteLine("2,1234567890");
                writer.WriteLine("3,https://example.com");
                writer.WriteLine("4,SampleQR");
                writer.WriteLine("5,AnotherValue");
            }
        }

        // Read CSV lines (skip header) and collect QR code texts, limiting to a safe batch size
        var records = new List<string>();
        using (var reader = new StreamReader(csvPath))
        {
            // Skip the header row
            if (!reader.EndOfStream) reader.ReadLine();

            // Read up to 5 records (or fewer if the file has less)
            while (!reader.EndOfStream && records.Count < 5)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                if (parts.Length < 2) continue;

                // Use the second column as the QR code text
                records.Add(parts[1].Trim());
            }
        }

        // Create a new PDF document to hold the QR code images
        var pdfDoc = new Document();

        // Limit the number of QR codes added to the PDF to 4, as per example guidelines
        int maxItems = Math.Min(4, records.Count);
        for (int i = 0; i < maxItems; i++)
        {
            string codeText = records[i];

            // Generate a QR code image in memory using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set error correction level (optional)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();

                    // Add a new page to the PDF for this barcode
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf image object from the PNG byte array
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = new MemoryStream(pngBytes),
                        // Define a reasonable size for the QR code on the page
                        FixWidth = 150f,
                        FixHeight = 150f,
                        // Center the image horizontally and vertically
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);
                }
            }
        }

        // Save the compiled PDF report to disk
        pdfDoc.Save(pdfPath);

        // Inform the user of the successful operation
        Console.WriteLine($"Generated PDF report with {maxItems} QR codes: {pdfPath}");
    }
}