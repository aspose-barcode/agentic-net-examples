// Title: Retrieve MaxiCode mode and postal code from a PDF
// Description: Demonstrates extracting the MaxiCode mode and associated postal code data from a PDF file that contains MaxiCode symbols.
// Category-Description: This example belongs to the Aspose.BarCode PDF barcode extraction category. It uses Aspose.Pdf to render PDF pages to images and Aspose.BarCode.BarCodeRecognition to decode MaxiCode symbols. Typical use cases include processing shipping documents, invoices, or any PDF containing MaxiCode for logistics. Developers often need to read mode‑specific data such as postal codes, carrier IDs, or other structured information from MaxiCode barcodes.
// Prompt: Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols.
// Tags: maxicode, barcode, extraction, pdf, aspose.barcode, aspose.pdf, codetext, postalcode

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Program that extracts MaxiCode mode and postal code information from a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional PDF path argument, renders each page to an image,
    /// reads MaxiCode barcodes, and outputs the detected mode and postal code (if available).
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine the PDF file path: use the first argument if supplied, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document using Aspose.Pdf.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render pages as images with barcode optimization enabled.
            using (var converter = new PdfConverter(pdfDocument))
            {
                converter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through each page in the PDF.
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    converter.StartPage = pageNumber;
                    converter.EndPage = pageNumber;
                    converter.DoConvert();

                    // Render the current page to a memory stream (image format).
                    using (var imageStream = new MemoryStream())
                    {
                        converter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a BarCodeReader to decode MaxiCode symbols from the image.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
                        {
                            // Process each detected barcode on the page.
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                // Retrieve the MaxiCode mode from the extended result data.
                                var mode = result.Extended.MaxiCode.Mode;
                                Console.WriteLine($"Detected MaxiCode mode: {mode}");

                                // Decode the complex codetext using the identified mode.
                                var complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);

                                // Output the postal code if the mode supports it.
                                switch (complexCodetext)
                                {
                                    case MaxiCodeCodetextMode2 mode2:
                                        Console.WriteLine($"Postal Code: {mode2.PostalCode}");
                                        break;
                                    case MaxiCodeCodetextMode3 mode3:
                                        Console.WriteLine($"Postal Code: {mode3.PostalCode}");
                                        break;
                                    default:
                                        Console.WriteLine("Postal code not available for this mode.");
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}