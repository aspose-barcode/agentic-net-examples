// Title: Decode RM4SCC barcode from PDF using Aspose.BarCode
// Description: Demonstrates how to render a PDF page to an image and decode an RM4SCC barcode embedded in it, extracting the original data.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to combine PDF rendering (Aspose.Pdf) with barcode recognition (Aspose.BarCode). Typical use cases include extracting data from shipping labels, invoices, or any documents that embed RM4SCC barcodes. Developers often need to convert PDF pages to high‑resolution images before feeding them to the BarCodeReader for reliable detection.
// Prompt: Decode an RM4SCC barcode embedded in a PDF page and extract the original data.
// Tags: rm4scc, barcode, decode, pdf, aspose.barcode, aspose.pdf, image, recognition

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes RM4SCC barcodes embedded in PDF pages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads a PDF, renders each page to PNG, and reads RM4SCC barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file containing the RM4SCC barcode
        string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "SampleRM4SCC.pdf");

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Specify the decode type for RM4SCC barcodes
        BaseDecodeType decodeType = DecodeType.RM4SCC;

        // Open the PDF document for processing
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate through all pages (example processes each page)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Render the current page to a PNG image held in memory
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // 300 DPI provides sufficient detail for reliable barcode recognition
                    PngDevice pngDevice = new PngDevice(new Resolution(300));
                    pngDevice.Process(pdfDoc.Pages[pageIndex], imageStream);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Initialize the barcode reader with the rendered image and the desired decode type
                    using (BarCodeReader reader = new BarCodeReader(imageStream, decodeType))
                    {
                        bool found = false;

                        // Enumerate all detected barcodes on the page
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Page {pageIndex}: Detected barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
                            found = true;
                        }

                        // Inform the user if no RM4SCC barcode was found on the page
                        if (!found)
                        {
                            Console.WriteLine($"Page {pageIndex}: No RM4SCC barcode detected.");
                        }
                    }
                }
            }
        }
    }
}