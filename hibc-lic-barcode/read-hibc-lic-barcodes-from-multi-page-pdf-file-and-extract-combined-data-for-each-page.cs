// Title: Read HIBC LIC barcodes from a multi‑page PDF and extract combined data per page
// Description: Demonstrates how to load a PDF, convert each page to an image, and decode HIBC LIC barcodes, outputting the combined primary and secondary data for each page.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showing how to use Aspose.Pdf to render PDF pages to images and Aspose.BarCode.BarCodeRecognition to read HIBC LIC (Health Industry Bar Code) complex barcodes. Typical use cases include processing medical or pharmaceutical documents where each page may contain a HIBC LIC label, and developers often need to extract product, lot, expiry, and other data programmatically. The sample uses Document, PdfConverter, BarCodeReader, DecodeType, ComplexCodetextReader, and HIBCLICCombinedCodetext classes.
// Prompt: Read HIBC LIC barcodes from a multi‑page PDF file and extract combined data for each page.
// Tags: hibc, lic, barcode, pdf, recognition, aspnet, aspnetcore, aspose.barcode, aspose.pdf, complexcodetext, decode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates reading HIBC LIC barcodes from each page of a PDF and printing combined data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional PDF path argument, processes each page, and writes barcode data to console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if provided, otherwise default to "sample.pdf"
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf
        using (var pdfDocument = new Aspose.Pdf.Document(pdfPath))
        {
            // Initialize a PdfConverter to render pages as images
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization for better image quality
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                int pageCount = pdfDocument.Pages.Count;

                // Iterate through each page in the PDF
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to process a single page
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Store the rendered page image in a memory stream
                    using (var pageImageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageImageStream);
                        pageImageStream.Position = 0; // Reset stream position for reading

                        // Create a BarCodeReader for HIBC QR LIC type using the page image
                        using (var reader = new BarCodeReader(pageImageStream, DecodeType.HIBCQRLIC))
                        {
                            // Read all barcodes found on the page
                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Attempt to decode the complex HIBCLIC codetext
                                var complexCodetext = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                                if (complexCodetext is HIBCLICCombinedCodetext combined)
                                {
                                    // Output primary data fields
                                    Console.WriteLine($"Page {pageNumber}:");
                                    Console.WriteLine($"  Product or catalog number: {combined.PrimaryData.ProductOrCatalogNumber}");
                                    Console.WriteLine($"  Labeler identification code: {combined.PrimaryData.LabelerIdentificationCode}");
                                    Console.WriteLine($"  Unit of measure ID: {combined.PrimaryData.UnitOfMeasureID}");

                                    // Output secondary and additional data fields
                                    var secondary = combined.SecondaryAndAdditionalData;
                                    Console.WriteLine($"  Expiry date: {secondary.ExpiryDate}");
                                    Console.WriteLine($"  Quantity: {secondary.Quantity}");
                                    Console.WriteLine($"  Lot number: {secondary.LotNumber}");
                                    Console.WriteLine($"  Serial number: {secondary.SerialNumber}");
                                    Console.WriteLine($"  Date of manufacture: {secondary.DateOfManufacture}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}