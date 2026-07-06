// Title: Read HIBC LIC Barcodes from Multi‑Page PDF and Extract Combined Data
// Description: Demonstrates how to read HIBC LIC barcodes from each page of a PDF file and output the combined primary and secondary data.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, focusing on extracting complex HIBC LIC symbology from PDF documents. It showcases the use of Aspose.Pdf for page rendering and Aspose.BarCode.BarCodeRecognition for barcode detection, along with ComplexCodetextReader for parsing combined HIBC data. Developers working with healthcare or logistics barcode standards can use this pattern to process multi‑page PDFs and retrieve detailed product information.
// Prompt: Read HIBC LIC barcodes from a multi‑page PDF file and extract combined data for each page.
// Tags: hibc, lic, barcode, pdf, read, extraction, aspose.barcode, aspose.pdf, complexcodetext

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads HIBC LIC barcodes from each page of a PDF file
/// and prints the extracted combined data (primary and secondary) to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional PDF file path argument; defaults to "sample.pdf" if none provided.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path from arguments or use default.
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the specified PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize a PDF converter to render pages as images.
            using (PdfConverter pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization for better detection.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                int pageCount = pdfDocument.Pages.Count;

                // Process each page individually.
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure converter to work on the current page only.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Retrieve the rendered page image into a memory stream.
                    using (MemoryStream pageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageStream);
                        pageStream.Position = 0;

                        // Initialize barcode reader for all supported types.
                        using (BarCodeReader reader = new BarCodeReader(pageStream, DecodeType.AllSupportedTypes))
                        {
                            List<string> hibcDataPerPage = new List<string>();

                            // Iterate through all detected barcodes on the page.
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                // Filter for HIBC symbology.
                                if (!string.IsNullOrEmpty(result.CodeTypeName) && result.CodeTypeName.Contains("HIBC"))
                                {
                                    // Attempt to decode the HIBC LIC complex codetext.
                                    var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                                    // Handle combined primary and secondary data.
                                    if (complex is HIBCLICCombinedCodetext combined)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append($"ProductOrCatalogNumber={combined.PrimaryData.ProductOrCatalogNumber};");
                                        sb.Append($"LabelerIdentificationCode={combined.PrimaryData.LabelerIdentificationCode};");
                                        sb.Append($"UnitOfMeasureID={combined.PrimaryData.UnitOfMeasureID};");

                                        if (combined.SecondaryAndAdditionalData != null)
                                        {
                                            var sec = combined.SecondaryAndAdditionalData;
                                            sb.Append($"LotNumber={sec.LotNumber};");
                                            sb.Append($"SerialNumber={sec.SerialNumber};");
                                            sb.Append($"Quantity={sec.Quantity};");
                                            sb.Append($"ExpiryDate={sec.ExpiryDate:yyyy-MM-dd};");
                                        }

                                        hibcDataPerPage.Add(sb.ToString());
                                    }
                                    // Handle primary data only (no secondary information).
                                    else if (complex is HIBCLICPrimaryDataCodetext primary)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append($"ProductOrCatalogNumber={primary.Data.ProductOrCatalogNumber};");
                                        sb.Append($"LabelerIdentificationCode={primary.Data.LabelerIdentificationCode};");
                                        sb.Append($"UnitOfMeasureID={primary.Data.UnitOfMeasureID};");
                                        hibcDataPerPage.Add(sb.ToString());
                                    }
                                }
                            }

                            // Output collected HIBC data for the current page, if any.
                            if (hibcDataPerPage.Count > 0)
                            {
                                Console.WriteLine($"Page {pageNumber}:");
                                foreach (string line in hibcDataPerPage)
                                {
                                    Console.WriteLine(line);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}