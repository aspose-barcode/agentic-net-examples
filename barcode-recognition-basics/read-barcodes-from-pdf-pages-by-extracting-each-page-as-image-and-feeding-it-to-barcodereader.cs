// Title: Read barcodes from PDF pages using Aspose.BarCode and Aspose.Pdf
// Description: Demonstrates creating a PDF with QR code images, extracting each page as an image, and recognizing barcodes with BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showcasing how to generate barcodes, embed them in PDF documents, and subsequently recognize them. It uses BarcodeGenerator, Document, PdfConverter, and BarCodeReader classes—common tools for developers who need to automate barcode handling within PDF workflows, such as invoice processing or document archiving.
// Prompt: Read barcodes from PDF pages by extracting each page as an image and feeding it to BarCodeReader.
// Tags: barcode, qr, pdf, extraction, reading, aspose.barcode, aspose.pdf, generation, recognition, image

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates generating a PDF with QR codes, extracting each page as an image,
/// and reading the barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary PDF, extracts pages as images,
    /// reads barcodes from each image, and cleans up resources.
/// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare a temporary file path for the PDF that will hold barcode images
        // -----------------------------------------------------------------
        string pdfPath = Path.Combine(Path.GetTempPath(), "SampleBarcodes.pdf");

        // -----------------------------------------------------------------
        // Generate a PDF document containing two pages, each with a QR code image
        // -----------------------------------------------------------------
        var imageStreams = new List<MemoryStream>();
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < 2; i++)
            {
                // Create a QR code for the current page
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"Page{i + 1}"))
                {
                    // Render the QR code to a bitmap
                    using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        // Store the bitmap in a memory stream (PNG format)
                        var ms = new MemoryStream();
                        bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                        ms.Position = 0;
                        imageStreams.Add(ms);

                        // Add a new page to the PDF and place the barcode image on it
                        var page = pdfDoc.Pages.Add();
                        page.Paragraphs.Add(new Aspose.Pdf.Image { ImageStream = ms });
                    }
                }
            }

            // Save the constructed PDF to the temporary location
            pdfDoc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // Release the image streams now that the PDF has been saved
        // -----------------------------------------------------------------
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }
        imageStreams.Clear();

        // -----------------------------------------------------------------
        // Verify that the PDF file exists before attempting to read it
        // -----------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("PDF file not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Convert each PDF page to an image and feed it to BarCodeReader
        // -----------------------------------------------------------------
        using (var pdfDoc = new Document(pdfPath))
        {
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Optimize conversion for barcode detection
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                pdfConverter.Resolution = new Resolution(300);
                pdfConverter.StartPage = 1;
                pdfConverter.EndPage = pdfConverter.Document.Pages.Count;
                pdfConverter.DoConvert();

                // Process each rendered page image
                while (pdfConverter.HasNextImage())
                {
                    var pageStream = new MemoryStream();
                    pdfConverter.GetNextImage(pageStream);
                    pageStream.Position = 0;

                    // Read all supported barcodes from the page image
                    using (var reader = new BarCodeReader(pageStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
                        }
                    }

                    pageStream.Dispose();
                }
            }
        }

        // -----------------------------------------------------------------
        // Clean up the generated PDF file
        // -----------------------------------------------------------------
        try
        {
            File.Delete(pdfPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}