// Title: Detect barcodes in password‑protected PDF using Aspose.BarCode
// Description: Demonstrates opening a password‑protected PDF, converting each page to an image, and reading all supported barcodes from the images.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category, illustrating how to work with secured PDF documents. It shows usage of Aspose.Pdf Document, PdfConverter, and Aspose.BarCode.BarCodeRecognition.BarCodeReader to extract barcodes from protected files. Developers often need to supply credentials to open encrypted PDFs before performing barcode detection in automated or secure processing pipelines.
// Prompt: Handle password‑protected image files by supplying credentials before barcode detection in secure pipelines.
// Tags: pdf, password, barcode detection, barcodereader, decode, aspnet, aspose.pdf, aspose.barcode, image conversion

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that opens a password‑protected PDF, converts each page to an image,
/// and reads all supported barcodes from those images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes the protected PDF and outputs detected barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the password‑protected PDF file and its password.
        string pdfPath = "protected.pdf";
        string password = "1234";

        // Verify that the PDF file exists before attempting to open it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the PDF document using the supplied password.
            using (var pdfDocument = new Document(pdfPath, password))
            {
                // Initialize a PdfConverter to render PDF pages as images.
                using (var pdfConverter = new PdfConverter(pdfDocument))
                {
                    int pageCount = pdfDocument.Pages.Count;

                    // Iterate through each page in the PDF.
                    for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                    {
                        // Configure the converter to process a single page.
                        pdfConverter.StartPage = pageNumber;
                        pdfConverter.EndPage = pageNumber;
                        pdfConverter.DoConvert();

                        // Retrieve the rendered page image into a memory stream.
                        using (var imageStream = new MemoryStream())
                        {
                            pdfConverter.GetNextImage(imageStream);
                            imageStream.Position = 0; // Reset stream position for reading.

                            // Create a BarCodeReader to detect all supported barcode types.
                            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                            {
                                // Enumerate and output each detected barcode.
                                foreach (var result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing.
            Console.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}