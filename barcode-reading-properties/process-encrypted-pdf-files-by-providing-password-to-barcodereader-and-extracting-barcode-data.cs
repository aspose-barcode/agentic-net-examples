// Title: Extract barcodes from encrypted PDF using Aspose.BarCode
// Description: Demonstrates opening a password‑protected PDF, converting each page to an image, and reading all supported barcodes with Aspose.BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF processing collection. It shows how to work with encrypted PDF documents, use Aspose.Pdf to render pages to images, and employ BarCodeReader (DecodeType.AllSupportedTypes) to detect any barcode symbology. Developers often need to batch‑process secured PDFs to extract embedded barcodes for inventory, shipping, or document verification workflows.
// Prompt: Process encrypted PDF files by providing password to BarCodeReader and extracting barcode data.
// Tags: pdf, encryption, barcode, extraction, decode, all-supported-types, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Example program that reads an encrypted PDF, converts each page to an image,
/// and extracts any barcode data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line arguments for the PDF path and password.
    /// </summary>
    /// <param name="args">args[0] = PDF file path (default: sample_encrypted.pdf), args[1] = password (default: password)</param>
    static void Main(string[] args)
    {
        // Resolve PDF file path and password from command‑line or use defaults.
        string pdfPath = args.Length > 0 ? args[0] : "sample_encrypted.pdf";
        string password = args.Length > 1 ? args[1] : "password";

        // Verify that the specified PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF document using the supplied password.
        using (Document pdfDoc = new Document(pdfPath, password))
        {
            // Initialize a PDF converter to render pages as images.
            using (PdfConverter pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode‑specific optimizations and set image resolution.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                pdfConverter.Resolution = new Resolution(300);

                // Process each page individually.
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    // Configure the converter to work on a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page image into a memory stream.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(ms);
                        ms.Position = 0; // Reset stream position for reading.

                        // Use BarCodeReader to detect all supported barcode types in the image.
                        using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Data={result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}