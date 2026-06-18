using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates how to extract barcodes from each page of a PDF using Aspose.Pdf and Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads a PDF, converts each page to an image,
    /// and reads any barcodes present on the page.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file. Change as needed or place a sample PDF named "sample.pdf" beside the executable.
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document into Aspose.Pdf.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize a converter that will render PDF pages to images.
            var pdfConverter = new PdfConverter(pdfDocument);
            pdfConverter.RenderingOptions.BarcodeOptimization = true; // Enable barcode optimization for better detection.

            // Process each page in the PDF.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Configure the converter to work on a single page.
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Retrieve the rendered page as an image stream.
                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Create a barcode reader that scans the image for all supported barcode types.
                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        Console.WriteLine($"Page {pageNumber}:");

                        // Iterate through all detected barcodes on the current page.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}