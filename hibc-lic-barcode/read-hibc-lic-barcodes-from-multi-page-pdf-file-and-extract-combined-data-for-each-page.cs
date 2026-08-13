// Title: Read HIBC LIC Barcodes from Multi‑Page PDF and Combine Page Data
// Description: Demonstrates how to load a multi‑page PDF, render each page to an image, and use Aspose.BarCode to read HIBC LIC Code128 barcodes, then concatenate the results per page.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF barcode extraction category. It shows how to combine Aspose.Pdf (Document, PdfConverter) with Aspose.BarCode (BarCodeReader, DecodeType) to recognize HIBC LIC barcodes on each page of a PDF. Typical use cases include processing shipping documents, medical labels, or inventory forms where each page may contain one or more HIBC LIC barcodes that need to be aggregated. Developers often need to render PDF pages to images, configure barcode optimization, and collect decoded text for further processing.
// Prompt: Read HIBC LIC barcodes from a multi‑page PDF file and extract combined data for each page.
// Tags: barcode, hibc, lic, pdf, aspnet, aspnet-core, aspose.barcode, aspose.pdf, barcode-recognition, code128, multi-page

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates reading HIBC LIC Code128 barcodes from each page of a multi‑page PDF and outputting combined data per page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads the PDF, renders pages, reads barcodes, and prints combined results.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page PDF containing HIBC LIC barcodes.
        string pdfPath = "input.pdf";

        // Verify that the PDF file exists before attempting to process it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document into Aspose.Pdf.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize the PDF converter which will render PDF pages to images.
            var pdfConverter = new PdfConverter(pdfDocument);
            // Enable barcode optimization to improve recognition speed and accuracy.
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Iterate through each page in the PDF document.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Configure the converter to process only the current page.
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Render the current page to an in‑memory image stream.
                using (var pageImageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(pageImageStream);
                    pageImageStream.Position = 0; // Reset stream position for reading.

                    // Create a barcode reader for HIBC LIC Code128 barcodes using the rendered image.
                    using (var reader = new BarCodeReader(pageImageStream, DecodeType.HIBCCode128LIC))
                    {
                        var barcodesOnPage = new List<string>();

                        // Read all barcodes found on the page and collect their decoded text.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            barcodesOnPage.Add(result.CodeText);
                        }

                        // Combine the decoded barcode texts for the current page.
                        string combinedData = string.Join("; ", barcodesOnPage);
                        Console.WriteLine($"Page {pageNumber}: {combinedData}");
                    }
                }
            }

            // Release resources used by the PDF converter.
            pdfConverter.Dispose();
        }
    }
}