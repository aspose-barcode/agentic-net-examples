// Title: Read barcodes from each PDF page by converting pages to PNG images
// Description: Demonstrates extracting each page of a PDF as a high‑resolution PNG image and using Aspose.BarCode's BarCodeReader to detect all supported barcode types.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category, illustrating how to combine Aspose.Pdf page rendering with BarCodeReader. Typical use cases include scanning invoices, shipping documents, or any PDF containing barcodes. Developers often need to render PDF pages to images before feeding them to the barcode engine, using Document, PngDevice, and BarCodeReader classes.
// Prompt: Read barcodes from PDF pages by extracting each page as an image and feeding it to BarCodeReader.
// Tags: pdf, barcode, recognition, image conversion, aspnet, aspose.pdf, aspose.barcode, decodeall

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from a PDF by converting each page to an image and scanning it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads a PDF, renders each page to PNG, and prints detected barcodes.
    /// </summary>
    static void Main()
    {
        // Build the full path to the sample PDF located in the current working directory.
        string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "sample.pdf");

        // Verify that the PDF file exists before attempting to process it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found at path: {pdfPath}");
            return;
        }

        // Open the PDF document using Aspose.Pdf.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create a PNG device with a resolution of 300 DPI for high‑quality image rendering.
            PngDevice pngDevice = new PngDevice(new Resolution(300));

            // Determine the number of pages in the PDF.
            int pageCount = pdfDoc.Pages.Count;
            if (pageCount == 0)
            {
                Console.WriteLine("PDF contains no pages.");
                return;
            }

            // Iterate through each page, render it to a memory stream, and scan for barcodes.
            for (int i = 1; i <= pageCount; i++)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Render the current PDF page to the memory stream as a PNG image.
                    pngDevice.Process(pdfDoc.Pages[i], ms);
                    ms.Position = 0; // Reset stream position for reading.

                    // Initialize the barcode reader to detect all supported barcode types.
                    using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        // Read and output each detected barcode on the current page.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Page {i}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}