// Title: Extract barcodes from password-protected PDF using Aspose.BarCode
// Description: Demonstrates how to open an encrypted PDF with a password, render each page to an image, and read any barcodes present.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, illustrating the use of Aspose.Pdf.Document, PdfConverter, and Aspose.BarCode.BarCodeRecognition.BarCodeReader to decode barcodes from secured PDF files. Typical scenarios include scanning invoices, tickets, or forms that are password-protected, where developers need to extract barcode data without manual decryption.
// Prompt: Process encrypted PDF files by providing password to BarCodeReader and extracting barcode data.
// Tags: pdf, encryption, barcode, decoding, aspnet, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that opens an encrypted PDF, renders each page to an image,
/// and extracts any barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path to the encrypted PDF file (adjust as needed)
        string pdfPath = "encrypted.pdf";

        // Password for the encrypted PDF (adjust as needed)
        string password = "myPassword";

        // Verify that the PDF file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF document using the provided password
        using (var pdfDocument = new Document(pdfPath, password))
        {
            // Initialize the PDF converter which will render pages to images
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization to improve rendering speed for barcode detection
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Process each page in the PDF sequentially
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Configure the converter to render only the current page
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page image into a memory stream
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Create a barcode reader for the image stream, detecting all supported types
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Iterate through all detected barcodes on the current page
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}