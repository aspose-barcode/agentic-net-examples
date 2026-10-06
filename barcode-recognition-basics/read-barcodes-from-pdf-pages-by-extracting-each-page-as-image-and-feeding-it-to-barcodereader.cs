// Title: Read barcodes from PDF pages by converting each page to an image
// Description: Demonstrates generating a Code128 barcode, embedding it in a PDF, extracting each page as a PNG image, and decoding any barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing collection, illustrating how to combine Aspose.Pdf and Aspose.BarCode APIs to read barcodes from PDF documents. It showcases creating a PDF, rendering pages to images with PngDevice, and using BarCodeReader to detect all supported symbologies. Developers often need this pattern for invoice scanning, document verification, or batch processing of scanned PDFs.
// Prompt: Read barcodes from PDF pages by extracting each page as an image and feeding it to BarCodeReader.
// Tags: code128, barcode reading, pdf, image conversion, png, aspose.pdf, aspose.barcode, decode

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, PDF embedding, page image extraction, and barcode recognition using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary PDF with a barcode, extracts each page as an image, and reads any barcodes found.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePdfDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string pdfPath = Path.Combine(tempFolder, "sample.pdf");

        // Generate a Code128 barcode image and embed it into a new PDF document
        using (var barcodeStream = new MemoryStream())
        {
            // Create the barcode and save it as PNG into the memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading
            }

            // Build a PDF containing the barcode image
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();
                var image = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream // Assign the barcode image stream to the PDF image
                };
                page.Paragraphs.Add(image);
                pdfDoc.Save(pdfPath); // Save the PDF to the temporary location
            }

            // Open the saved PDF and process each page
            using (var pdfDoc = new Document(pdfPath))
            {
                // Set up a PNG device to render PDF pages at 300 DPI
                var pngDevice = new PngDevice(new Resolution(300));

                // Iterate through all pages in the PDF
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    // Render the current page to a memory stream as PNG
                    using (var pageStream = new MemoryStream())
                    {
                        pngDevice.Process(pdfDoc.Pages[i], pageStream);
                        pageStream.Position = 0; // Reset for reading by BarCodeReader

                        // Use BarCodeReader to detect any barcodes on the rendered page image
                        using (var reader = new BarCodeReader(pageStream, DecodeType.AllSupportedTypes))
                        {
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {i}: Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }
                    }
                }
            }
        }

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup (e.g., file locks)
        }
    }
}