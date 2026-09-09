// Title: Decode RM4SCC barcode from PDF pages
// Description: Demonstrates how to render each PDF page to an image and use Aspose.BarCode to detect and decode RM4SCC barcodes, outputting the extracted data.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category, illustrating the use of Document, PngDevice, Resolution, and BarCodeReader classes to process PDF content. Typical use cases include extracting barcode data from scanned documents, invoices, or shipping labels embedded in PDFs. Developers often need to convert PDF pages to raster images before applying barcode recognition, as shown here.
// Prompt: Decode an RM4SCC barcode embedded in a PDF page and extract the original data.
// Tags: rm4scc, barcode, decode, pdf, aspose.pdf, aspose.barcode, image conversion, barcode recognition

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that extracts RM4SCC barcode data from each page of a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the PDF, converts each page to PNG, scans for RM4SCC barcodes, and prints the decoded text.
    /// </summary>
    static void Main()
    {
        // Path to the source PDF file
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document for reading
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Define the resolution for rasterizing PDF pages (300 DPI)
            var resolution = new Resolution(300);

            // Iterate through all pages in the PDF
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Render the current page to a PNG image stored in memory
                using (MemoryStream imageStream = new MemoryStream())
                {
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Initialize the barcode reader to detect all supported barcode types
                    using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found on the rendered page
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Check if the detected barcode is of type RM4SCC (case‑insensitive)
                            if (string.Equals(result.CodeTypeName, "RM4SCC", StringComparison.OrdinalIgnoreCase))
                            {
                                // Output the page number and decoded barcode data
                                Console.WriteLine($"Page {pageNumber}: RM4SCC barcode data: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}