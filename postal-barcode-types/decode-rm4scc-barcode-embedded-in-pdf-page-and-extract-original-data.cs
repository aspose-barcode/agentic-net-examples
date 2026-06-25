using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates how to extract RM4SCC barcodes from each page of a PDF using Aspose.Pdf and Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument specifying the PDF file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the PDF file path: use the first argument if supplied, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the specified PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document within a using block to ensure proper disposal.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Create a PdfConverter to render PDF pages as images.
            var pdfConverter = new PdfConverter(pdfDocument);
            // Enable barcode optimization to improve barcode detection performance.
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Loop through each page in the PDF.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Configure the converter to process only the current page.
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Store the rendered page image in a memory stream.
                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    // Reset stream position to the beginning for reading.
                    imageStream.Position = 0;

                    // Initialize a barcode reader for RM4SCC barcodes using the image stream.
                    using (var reader = new BarCodeReader(imageStream, DecodeType.RM4SCC))
                    {
                        // Read all barcodes found on the page.
                        var results = reader.ReadBarCodes();
                        // Output each barcode's type and decoded text.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}