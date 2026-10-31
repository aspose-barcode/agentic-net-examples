// Title: Read HIBC LIC barcodes from each page of a PDF
// Description: Demonstrates how to extract HIBC LIC barcode data from a multi‑page PDF by converting each page to an image and decoding the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showing how to work with complex HIBC LIC barcodes using BarCodeReader, ComplexCodetextReader, and related classes. Typical use cases include processing pharmaceutical or medical device labeling PDFs to retrieve product, lot, and expiry information. Developers often need to render PDF pages to images, decode barcodes, and parse combined codetext for downstream processing.
// Prompt: Read HIBC LIC barcodes from a multi‑page PDF file and extract combined data for each page.
// Tags: hibc, lic, barcode, pdf, recognition, complexcodetext, aspnet, aspnetcore, csharp

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading HIBC LIC barcodes from a multi‑page PDF and extracting combined data per page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Processes the PDF, decodes barcodes, and prints extracted fields.
    /// </summary>
    static void Main()
    {
        // Path to the source PDF file.
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document and create a converter to render pages as images.
        using (var pdfDocument = new Document(pdfPath))
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

                // Retrieve the rendered page image into a memory stream.
                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Initialize the barcode reader for HIBC QR LIC symbology.
                    using (var reader = new BarCodeReader(imageStream, DecodeType.HIBCQRLIC))
                    {
                        // Read all barcodes found on the current page.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Attempt to decode the complex HIBC LIC codetext.
                            var complexCodetext = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                            var combined = complexCodetext as HIBCLICCombinedCodetext;

                            // If the barcode is not a combined HIBC LIC type, report and continue.
                            if (combined == null)
                            {
                                Console.WriteLine($"Page {pageNumber}: No combined HIBC LIC barcode detected.");
                                continue;
                            }

                            // Output the extracted primary data fields.
                            Console.WriteLine($"Page {pageNumber} - Product or catalog number: {combined.PrimaryData.ProductOrCatalogNumber}");
                            Console.WriteLine($"Page {pageNumber} - Labeler identification code: {combined.PrimaryData.LabelerIdentificationCode}");
                            Console.WriteLine($"Page {pageNumber} - Unit of measure ID: {combined.PrimaryData.UnitOfMeasureID}");

                            // Output the extracted secondary and additional data fields.
                            Console.WriteLine($"Page {pageNumber} - Expiry date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                            Console.WriteLine($"Page {pageNumber} - Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                            Console.WriteLine($"Page {pageNumber} - Lot number: {combined.SecondaryAndAdditionalData.LotNumber}");
                            Console.WriteLine($"Page {pageNumber} - Serial number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                            Console.WriteLine($"Page {pageNumber} - Date of manufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                        }
                    }
                }
            }
        }
    }
}