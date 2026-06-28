using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

/// <summary>
/// Generates QR code images from a list of URLs and compiles them into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads URLs from a CSV file (if present) or uses sample data,
    /// creates QR codes for up to four items, and saves them in a PDF document.
    /// </summary>
    static void Main()
    {
        // Path to optional input CSV file containing URLs (one per line).
        string csvPath = "input.csv";

        // Collection to hold the URL records.
        List<string> records = new List<string>();

        // Attempt to read URLs from the CSV file if it exists.
        if (File.Exists(csvPath))
        {
            try
            {
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    // Skip empty or whitespace-only lines.
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        records.Add(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while reading the file and exit.
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                return;
            }
        }
        else
        {
            // Use sample data when the CSV file is not found.
            // Limit to four items to stay within evaluation constraints.
            records.Add("https://example.com/item1");
            records.Add("https://example.com/item2");
            records.Add("https://example.com/item3");
            records.Add("https://example.com/item4");
        }

        // Determine how many items to process (maximum of four).
        int maxItems = Math.Min(4, records.Count);
        if (maxItems == 0)
        {
            Console.WriteLine("No data to process.");
            return;
        }

        // Create a new PDF document that will hold the QR code images.
        var pdfDoc = new Document();

        // List to keep memory streams alive until after the PDF is saved.
        List<MemoryStream> streams = new List<MemoryStream>();

        // Process each URL, generate a QR code, and add it to a new PDF page.
        for (int i = 0; i < maxItems; i++)
        {
            string text = records[i];

            // Generate QR code image in memory using Aspose.BarCode.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set QR code error correction level (optional).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Store the generated PNG image in a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Add a new page to the PDF document.
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image object from the memory stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms,
                    // Set image dimensions (points; 1 point = 1/72 inch).
                    FixWidth = 200.0,
                    FixHeight = 200.0
                };

                // Insert the image into the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);

                // Keep the stream for later disposal.
                streams.Add(ms);
            }
        }

        // Define the output PDF file path.
        string pdfPath = "BarcodesReport.pdf";

        // Attempt to save the PDF document to disk.
        try
        {
            pdfDoc.Save(pdfPath);
            Console.WriteLine($"PDF report generated: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving PDF: {ex.Message}");
        }

        // Dispose all memory streams to release resources.
        foreach (var s in streams)
        {
            s.Dispose();
        }
    }
}