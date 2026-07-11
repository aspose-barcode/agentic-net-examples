// Title: Decode RM4SCC barcode from PDF page
// Description: Demonstrates how to extract an RM4SCC barcode embedded in a PDF document by converting each page to an image and using Aspose.BarCode to decode it.
// Category-Description: This example belongs to the Aspose.BarCode PDF barcode recognition category, showcasing the use of PdfConverter (Aspose.Pdf.Facades) together with BarCodeReader (Aspose.BarCode.BarCodeRecognition) to locate and decode RM4SCC symbology. Typical scenarios include processing shipping labels or inventory documents where RM4SCC codes are printed inside PDFs. Developers often need to render PDF pages to images, enable barcode optimization, and apply high‑quality settings for reliable extraction.
// Prompt: Decode an RM4SCC barcode embedded in a PDF page and extract the original data.
// Tags: rm4scc, barcode, decode, pdf, aspose.barcode, aspose.pdf, image conversion

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that decodes RM4SCC barcodes embedded in PDF pages using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Renders each PDF page to an image, then reads RM4SCC barcodes from the image.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file containing the RM4SCC barcode.
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PDF converter which will render pages to images.
        using (var pdfConverter = new PdfConverter())
        {
            pdfConverter.BindPdf(pdfPath);
            // Enable barcode optimization to improve detection accuracy.
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Determine the total number of pages in the document.
            int totalPages = pdfConverter.Document.Pages.Count;

            // Iterate through each page (example processes all pages).
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Set the range to a single page for conversion.
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Render the current page to an in‑memory image stream.
                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Create a barcode reader configured for RM4SCC symbology.
                    using (var reader = new BarCodeReader(imageStream, DecodeType.RM4SCC))
                    {
                        // Use high‑quality settings to improve recognition reliability.
                        reader.QualitySettings = QualitySettings.HighQuality;

                        // Attempt to read all barcodes on the image.
                        var results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine($"No RM4SCC barcode detected on page {pageNumber}.");
                        }
                        else
                        {
                            // Output each decoded barcode value.
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Page {pageNumber} - Decoded RM4SCC: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}