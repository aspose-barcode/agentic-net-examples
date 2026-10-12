// Title: Detect barcodes in password‑protected PDF files
// Description: Demonstrates opening a secured PDF with a password, converting each page to an image, and reading any barcodes present.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category, illustrating how to work with protected documents using Aspose.Pdf and Aspose.BarCode. It shows the use of Document, PdfConverter, and BarCodeReader classes to extract barcodes from PDFs in secure processing pipelines, a common requirement for automated invoice or ticket processing systems.
// Prompt: Handle password‑protected image files by supplying credentials before barcode detection in secure pipelines.
// Tags: pdf, password, barcode detection, aspnet, aspose.barcode, aspose.pdf, decode, image conversion

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that opens a password‑protected PDF, converts each page to an image,
/// and reads any barcodes found on those pages using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the password‑protected PDF file
        string pdfPath = "protected.pdf";

        // Password required to open the PDF
        string password = "secret";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document using the supplied password
        using (var pdfDocument = new Document(pdfPath, password))
        {
            // Initialize a PDF‑to‑image converter with barcode‑optimization enabled
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through each page of the PDF
                int pageCount = pdfDocument.Pages.Count;
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to process a single page
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page as an image stream
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Create a barcode reader for the image stream, supporting all barcode types
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            bool anyFound = false;

                            // Enumerate all detected barcodes on the current page
                            foreach (var result in reader.ReadBarCodes())
                            {
                                anyFound = true;
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }

                            // Inform the user if no barcodes were detected on this page
                            if (!anyFound)
                            {
                                Console.WriteLine($"Page {pageNumber}: No barcodes detected.");
                            }
                        }
                    }
                }
            }
        }
    }
}