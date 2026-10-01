// Title: Extract Barcodes from Encrypted PDF Using Aspose.BarCodeReader
// Description: Demonstrates how to open a password‑protected PDF with Aspose.Pdf, render its pages to images, and read any barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, showing how to combine Aspose.Pdf and Aspose.BarCode to decode barcodes from encrypted PDF documents. It highlights key classes such as Document, PdfConverter, BarCodeReader, and DecodeType, which developers use to render protected PDFs to images and extract barcode data for inventory, document management, or scanning workflows.
// Prompt: Process encrypted PDF files by providing password to BarCodeReader and extracting barcode data.
// Tags: pdf, encryption, barcode, decoding, aspose.pdf, aspose.barcode, c#

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates processing of an encrypted PDF file to extract barcode data using Aspose.Pdf and Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that opens the encrypted PDF, renders each page to an image, and reads all supported barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the encrypted PDF and its password.
        string pdfPath = "sample_encrypted.pdf";
        string password = "myPassword";

        // Ensure the PDF file exists before attempting to open it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF document using the supplied password.
        using (var pdfDocument = new Document(pdfPath, password))
        {
            // Initialize a PDF converter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization to improve detection performance.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Restrict processing to a maximum of 4 pages (evaluation mode limitation).
                int maxPages = Math.Min(pdfDocument.Pages.Count, 4);

                // Iterate through each page to render and scan for barcodes.
                for (int pageNumber = 1; pageNumber <= maxPages; pageNumber++)
                {
                    // Configure the converter to process only the current page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;

                    // Perform the conversion for the selected page.
                    pdfConverter.DoConvert();

                    // Capture the rendered image into a memory stream.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Create a barcode reader that scans for all supported symbologies.
                        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                        using (var reader = new BarCodeReader(imageStream, decodeType))
                        {
                            // Read all barcodes detected on the current page.
                            BarCodeResult[] results = reader.ReadBarCodes();

                            // Output each detected barcode's text and type.
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Page {pageNumber}: CodeText = {result.CodeText}, CodeType = {result.CodeTypeName}");
                            }

                            // Inform the user if no barcodes were found on the page.
                            if (results.Length == 0)
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