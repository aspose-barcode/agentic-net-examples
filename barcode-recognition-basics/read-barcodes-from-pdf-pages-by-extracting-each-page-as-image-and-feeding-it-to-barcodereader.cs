// Title: Read barcodes from PDF pages using Aspose
// Description: Demonstrates extracting each PDF page as an image and scanning it for barcodes with BarCodeReader.
// Prompt: Read barcodes from PDF pages by extracting each page as an image and feeding it to BarCodeReader.
// Tags: barcode, pdf, image extraction, aspose.pdf, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that reads barcodes from each page of a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads a PDF, converts each page to an image, and scans for barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file (adjust as needed)
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (var pdfDocument = new Document(pdfPath))
        {
            int pageCount = pdfDocument.Pages.Count;
            Console.WriteLine($"Processing {pageCount} page(s) from '{pdfPath}'.");

            // Iterate through each page
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                var page = pdfDocument.Pages[pageIndex];

                // Convert the page to a JPEG image stored in a memory stream
                using (var imageStream = new MemoryStream())
                {
                    var resolution = new Resolution(300);
                    var jpegDevice = new JpegDevice(resolution);
                    jpegDevice.Process(page, imageStream);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Load the image into an Aspose.Drawing.Bitmap
                    using (var bitmap = new Bitmap(imageStream))
                    {
                        // Initialize the barcode reader for all supported symbologies
                        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Use a standard quality preset
                            reader.QualitySettings = QualitySettings.NormalQuality;

                            // Perform barcode detection
                            var results = reader.ReadBarCodes();

                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageIndex}: No barcodes detected.");
                            }
                            else
                            {
                                Console.WriteLine($"Page {pageIndex}: Detected {results.Length} barcode(s).");
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}