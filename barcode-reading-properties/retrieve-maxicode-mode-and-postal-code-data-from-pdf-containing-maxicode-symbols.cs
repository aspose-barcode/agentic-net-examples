// Title: Retrieve MaxiCode mode and postal code from PDF
// Description: Demonstrates how to extract MaxiCode mode and postal code data from each page of a PDF containing MaxiCode symbols using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showing how to convert PDF pages to images, read MaxiCode barcodes, and decode extended MaxiCode data. It uses PdfConverter, BarCodeReader, DecodeType.MaxiCode, and ComplexCodetextReader to obtain mode and postal code information. Developers working with document automation and barcode extraction can use this pattern to process PDFs containing complex barcodes.
// Prompt: Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols.
// Tags: maxicode, barcode recognition, pdf processing, aspose.barcode, aspose.pdf, decode, postalcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that extracts MaxiCode mode and postal code information from a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional PDF file path argument, converts each page to an image,
    /// reads MaxiCode barcodes, and prints the detected mode and postal code.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF path: use first argument if supplied, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize PdfConverter to render PDF pages as images.
        using (PdfConverter pdfConverter = new PdfConverter())
        {
            // Bind the PDF document to the converter.
            pdfConverter.BindPdf(pdfPath);

            // Enable barcode optimization to improve image quality for barcode reading.
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Get total number of pages in the PDF.
            int pageCount = pdfConverter.Document.Pages.Count;

            // Process each page individually.
            for (int page = 1; page <= pageCount; page++)
            {
                // Configure converter to render only the current page.
                pdfConverter.StartPage = page;
                pdfConverter.EndPage = page;

                // Perform the conversion for the selected page.
                pdfConverter.DoConvert();

                // Retrieve the rendered image into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    pdfConverter.GetNextImage(ms);
                    ms.Position = 0; // Reset stream position for reading.

                    // Create a BarCodeReader configured for MaxiCode decoding.
                    using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                    {
                        // Iterate over all detected barcodes on the page.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Extract the MaxiCode mode from the extended result data.
                            var mode = result.Extended.MaxiCode.Mode;

                            // Decode the complex MaxiCode text to obtain structured information.
                            MaxiCodeCodetext complex = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                            if (complex == null)
                                continue; // Skip if decoding failed.

                            // Retrieve postal code based on the specific MaxiCode mode.
                            string postalCode = null;
                            if (complex is MaxiCodeCodetextMode2 mode2)
                                postalCode = mode2.PostalCode;
                            else if (complex is MaxiCodeCodetextMode3 mode3)
                                postalCode = mode3.PostalCode;

                            // Output the extracted information.
                            Console.WriteLine($"Page {page}: Mode={mode}, PostalCode={postalCode}");
                        }
                    }
                }
            }
        }
    }
}