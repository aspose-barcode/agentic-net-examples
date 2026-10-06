// Title: Read HIBC LIC barcodes from a multi‑page PDF and extract combined data per page
// Description: Demonstrates how to load a multi‑page PDF, render each page to an image, and use Aspose.BarCode to recognize HIBC LIC barcodes, then decode the combined data fields.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category, focusing on reading complex HIBC LIC (Health Industry Bar Code – License) symbology from raster images. It showcases the use of Document (Aspose.Pdf), PngDevice, BarCodeReader, DecodeType, and ComplexCodetextReader to extract detailed product information. Developers working with pharmaceutical or medical labeling often need to batch‑process PDFs and retrieve structured data from HIBC LIC barcodes.
// Prompt: Read HIBC LIC barcodes from a multi‑page PDF file and extract combined data for each page.
// Tags: hibc lic, barcode recognition, pdf processing, aspnet, aspose.pdf, aspose.barcode, c#, .net

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads HIBC LIC barcodes from each page of a PDF,
/// decodes the combined data, and writes the extracted fields to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the path to the multi‑page PDF file located in the current directory.
        string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "sample.pdf");

        // Verify that the PDF file exists before attempting to process it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.
        using (Document pdfDocument = new Document(pdfPath))
        {
            int pageCount = pdfDocument.Pages.Count;
            Console.WriteLine($"Processing {pageCount} page(s) from PDF.");

            // Iterate through each page in the PDF.
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                Console.WriteLine($"\n--- Page {pageNumber} ---");

                // Render the current PDF page to a PNG image stored in a memory stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Set rendering resolution to 300 DPI for good barcode readability.
                    Resolution resolution = new Resolution(300);
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Initialize the barcode reader to detect HIBC LIC QR codes in the image.
                    using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.HIBCQRLIC))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // If no barcodes are found, report and continue to the next page.
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No HIBC LIC barcode detected on this page.");
                            continue;
                        }

                        // Process each detected barcode.
                        foreach (BarCodeResult result in results)
                        {
                            // Attempt to decode the complex HIBC LIC codetext.
                            HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                            if (complex is HIBCLICCombinedCodetext combined)
                            {
                                // Output the combined data fields extracted from the barcode.
                                Console.WriteLine($"Product or catalog number: {combined.PrimaryData.ProductOrCatalogNumber}");
                                Console.WriteLine($"Labeler identification code: {combined.PrimaryData.LabelerIdentificationCode}");
                                Console.WriteLine($"Unit of measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                                Console.WriteLine($"Expiry date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                                Console.WriteLine($"Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                                Console.WriteLine($"Lot number: {combined.SecondaryAndAdditionalData.LotNumber}");
                                Console.WriteLine($"Serial number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                                Console.WriteLine($"Date of manufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                            }
                            else
                            {
                                // Barcode was recognized but does not contain combined HIBC LIC data.
                                Console.WriteLine("Barcode detected but not a combined HIBC LIC type.");
                            }
                        }
                    }
                }
            }
        }
    }
}