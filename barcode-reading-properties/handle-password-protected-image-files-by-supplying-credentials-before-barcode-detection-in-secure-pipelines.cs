// Title: Detect barcodes in password‑protected PDF files
// Description: Demonstrates opening a secured PDF with a password, converting each page to an image, and reading any barcodes present.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showing how to combine PdfDocument and PdfConverter with BarCodeReader to process protected documents. Developers often need to extract barcodes from confidential PDFs in automated pipelines, requiring credential handling and page‑wise image conversion before detection.
// Prompt: Handle password‑protected image files by supplying credentials before barcode detection in secure pipelines.
// Tags: pdf, password, barcode detection, aspnet, aspose.barcode, aspose.pdf, image conversion, automation

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates barcode detection in a password‑protected PDF using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Opens the PDF with the supplied password, converts each page to an image, and reads all supported barcodes.
    /// </summary>
    static void Main()
    {
        // Define the path to the protected PDF and its password.
        string pdfPath = "protected.pdf";
        string password = "myPassword";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the PDF document using the provided password.
            using (var pdfDocument = new Aspose.Pdf.Document(pdfPath, password))
            {
                // Initialize a PDF converter to render pages as images.
                using (var converter = new PdfConverter(pdfDocument))
                {
                    // Enable barcode optimization for better detection performance.
                    converter.RenderingOptions.BarcodeOptimization = true;

                    int pageCount = pdfDocument.Pages.Count;

                    // Process each page individually.
                    for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                    {
                        // Set the range to a single page for conversion.
                        converter.StartPage = pageNumber;
                        converter.EndPage = pageNumber;
                        converter.DoConvert();

                        // Retrieve the rendered image into a memory stream.
                        using (var imageStream = new MemoryStream())
                        {
                            converter.GetNextImage(imageStream);
                            imageStream.Position = 0; // Reset stream position for reading.

                            try
                            {
                                // Create a barcode reader that supports all barcode types.
                                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                                {
                                    // Iterate through all detected barcodes on the current page.
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle any errors that occur during barcode reading.
                                Console.WriteLine($"Error reading barcodes on page {pageNumber}: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle errors related to opening or processing the PDF document.
            Console.WriteLine($"Failed to process PDF: {ex.Message}");
        }
    }
}