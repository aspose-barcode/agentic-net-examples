// Title: Batch decode Swiss Post Parcel barcodes from PDF
// Description: Demonstrates how to extract Swiss Post Parcel additional service code barcodes from a PDF file and output their human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.SwissPostParcel and Aspose.Pdf conversion utilities. It shows typical batch processing of multi‑page PDFs to locate and decode specific barcode symbologies, a common requirement for logistics and postal automation solutions. Developers can adapt this pattern for other barcode types and document sources.
// Prompt: Perform batch decoding of Swiss Post Parcel additional service code barcodes from a PDF and extract human‑readable text.
// Tags: barcode, swisspostparcel, batch decoding, pdf, aspose.barcode, aspose.pdf, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that reads Swiss Post Parcel barcodes from a PDF document and prints their decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Loads a PDF, converts each page to an image, and decodes Swiss Post Parcel barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the PDF containing Swiss Post Parcel barcodes.
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.
        using (var pdfDocument = new Document(pdfPath))
        {
            int totalPages = pdfDocument.Pages.Count;

            // Limit processing to a maximum of 4 pages as per evaluation guidelines.
            int pagesToProcess = Math.Min(4, totalPages);

            // Iterate through each page to be processed.
            for (int pageNumber = 1; pageNumber <= pagesToProcess; pageNumber++)
            {
                // Initialize the PDF converter for the current page.
                using (var pdfConverter = new PdfConverter(pdfDocument))
                {
                    pdfConverter.RenderingOptions.BarcodeOptimization = true;
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Render the page to an in‑memory image stream.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a barcode reader configured for Swiss Post Parcel barcodes.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.SwissPostParcel))
                        {
                            // Attempt to read all barcodes on the page.
                            var results = reader.ReadBarCodes();

                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageNumber}: No Swiss Post Parcel barcode detected.");
                            }
                            else
                            {
                                // Output each detected barcode's type and decoded text.
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Barcode Type = {result.CodeTypeName}, CodeText = {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}