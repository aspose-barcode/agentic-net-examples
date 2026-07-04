// Title: Retrieve MaxiCode mode and postal code from PDF
// Description: Demonstrates how to extract MaxiCode mode and postal code information from each page of a PDF containing MaxiCode symbols.
// Prompt: Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols.
// Tags: barcode, maxicode, pdf, extraction, aspose, codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that reads a PDF, converts each page to an image,
/// and extracts MaxiCode mode and postal code data from any detected MaxiCode symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the input PDF file containing MaxiCode symbols.
        const string pdfPath = "input.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize a PDF converter to render pages as images.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization for better recognition performance.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through each page in the PDF.
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Store the rendered page image in a memory stream.
                    using (var pageImageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageImageStream);
                        pageImageStream.Position = 0; // Reset stream position for reading.

                        // Create a barcode reader that attempts to decode all supported types.
                        using (var reader = new BarCodeReader(pageImageStream, DecodeType.AllSupportedTypes))
                        {
                            // Process each detected barcode on the page.
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                // Skip results that are not MaxiCode.
                                if (result.Extended?.MaxiCode == null)
                                    continue;

                                // Retrieve the MaxiCode mode.
                                var mode = result.Extended.MaxiCode.Mode;
                                Console.WriteLine($"Page {pageNumber}: Detected MaxiCode");
                                Console.WriteLine($"  Mode: {mode}");

                                // Attempt to decode the MaxiCode codetext into a strongly‑typed object.
                                MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                                if (decoded == null)
                                {
                                    Console.WriteLine("  Unable to decode MaxiCode codetext.");
                                    continue;
                                }

                                // Extract postal code based on the specific MaxiCode mode.
                                string postalCode = null;
                                if (decoded is MaxiCodeCodetextMode2 mode2)
                                    postalCode = mode2.PostalCode;
                                else if (decoded is MaxiCodeCodetextMode3 mode3)
                                    postalCode = mode3.PostalCode;

                                // Output the postal code if available.
                                if (!string.IsNullOrEmpty(postalCode))
                                    Console.WriteLine($"  Postal Code: {postalCode}");
                                else
                                    Console.WriteLine("  Postal Code: (not available for this mode)");
                            }
                        }
                    }
                }
            }
        }
    }
}