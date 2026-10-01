// Title: Extract MaxiCode Mode and Postal Code from PDF
// Description: Demonstrates how to load a PDF, render its pages to images, and read MaxiCode symbols to obtain the mode and postal code data.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showing how to use Aspose.Pdf to convert PDF pages to images and Aspose.BarCode.BarCodeRecognition to detect and decode MaxiCode symbols. Typical use cases include processing shipping documents or invoices that embed MaxiCode barcodes, where developers need to extract mode information and postal codes for logistics workflows. The key API classes used are Document, PdfConverter, BarCodeReader, DecodeType, ComplexCodetextReader, and MaxiCodeCodetextMode* classes.
// Prompt: Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols.
// Tags: maxicode,barcode recognition,pdf processing,aspnet,aspose.barcode,aspose.pdf,decode,postal code,mode extraction

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Sample program that extracts MaxiCode mode and postal code information from a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads a PDF, converts each page to an image,
    /// reads all supported barcodes, and outputs MaxiCode mode and postal code when found.
    /// </summary>
    static void Main()
    {
        // Path to the PDF that contains MaxiCode symbols.
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization to improve detection speed.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Limit processing to the first four pages to respect evaluation mode restrictions.
                int totalPages = Math.Min(pdfDocument.Pages.Count, 4);

                // Iterate through each page to be processed.
                for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Store the rendered page image in a memory stream.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a BarCodeReader to detect all supported barcode types in the image.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Enumerate each detected barcode result.
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                // Check whether the barcode includes MaxiCode extended parameters.
                                if (result.Extended?.MaxiCode != null)
                                {
                                    // Retrieve and display the MaxiCode mode.
                                    var mode = result.Extended.MaxiCode.Mode;
                                    Console.WriteLine($"Page {pageNumber}: Detected MaxiCode with Mode = {mode}");

                                    // Decode the complex codetext based on the detected mode.
                                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                                    if (decoded is MaxiCodeCodetextMode2 mode2)
                                    {
                                        Console.WriteLine($"  Postal Code: {mode2.PostalCode}");
                                    }
                                    else if (decoded is MaxiCodeCodetextMode3 mode3)
                                    {
                                        Console.WriteLine($"  Postal Code: {mode3.PostalCode}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("  Unable to extract postal code (unexpected codetext type).");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}