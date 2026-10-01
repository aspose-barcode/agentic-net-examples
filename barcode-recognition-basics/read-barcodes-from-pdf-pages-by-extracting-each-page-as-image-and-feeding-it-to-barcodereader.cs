// Title: Read barcodes from PDF pages by converting each page to an image
// Description: Demonstrates extracting each page of a PDF as an image and using Aspose.BarCode.BarCodeReader to detect barcodes.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing collection. It shows how to combine Aspose.Pdf (for PDF creation and page rendering) with Aspose.BarCode (for barcode generation and recognition). Typical scenarios include scanning invoices, tickets, or any PDF documents that embed barcodes, where developers need to programmatically extract and decode them.
// Prompt: Read barcodes from PDF pages by extracting each page as an image and feeding it to BarCodeReader.
// Tags: barcode, pdf, image, reading, generation, aspose.barcode, aspose.pdf, code128, decode, all-supported-types

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Sample program that creates a PDF with barcode images and then reads those barcodes
/// by converting each PDF page to an image and scanning it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Ensures a sample PDF exists and then reads barcodes from it.
    /// </summary>
    static void Main()
    {
        // Path for the sample PDF (placed in the system temporary folder)
        string pdfPath = Path.Combine(Path.GetTempPath(), "SampleBarcodes.pdf");

        // Create a sample PDF with barcode images if it does not already exist
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // Read barcodes from each page of the PDF
        ReadBarcodesFromPdf(pdfPath);
    }

    /// <summary>
    /// Generates a PDF containing three pages, each with a Code128 barcode image.
    /// The PDF is saved to the specified output path.
    /// </summary>
    /// <param name="outputPath">Full file path where the PDF will be saved.</param>
    static void CreateSamplePdf(string outputPath)
    {
        // Keep image streams alive until the PDF is saved to avoid premature disposal
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Limit to 3 pages (evaluation mode limit is 4)
            for (int i = 0; i < 3; i++)
            {
                // Generate a barcode image in memory
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i + 1}"))
                {
                    var ms = new MemoryStream();
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Add a new page and place the barcode image on it
                    var page = pdfDoc.Pages.Add();
                    page.Paragraphs.Add(new Aspose.Pdf.Image { ImageStream = ms });

                    // Store the stream so it remains open during PDF saving
                    imageStreams.Add(ms);
                }
            }

            // Save the constructed PDF to the provided path
            pdfDoc.Save(outputPath);
        }

        // Dispose the image streams after the PDF has been saved
        foreach (var stream in imageStreams)
        {
            stream.Dispose();
        }
    }

    /// <summary>
    /// Opens the specified PDF, renders each page to an image, and uses BarCodeReader
    /// to detect and output any barcodes found on the page.
    /// </summary>
    /// <param name="pdfPath">Full file path of the PDF to be processed.</param>
    static void ReadBarcodesFromPdf(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document for reading
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize the PDF converter which will render pages to images
            var pdfConverter = new PdfConverter(pdfDoc);
            pdfConverter.RenderingOptions.BarcodeOptimization = true; // Optimize rendering for barcode clarity

            int pageCount = pdfDoc.Pages.Count;

            // Process each page individually
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Configure the converter to render only the current page
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Capture the rendered page as an image stream
                using (var pageImageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(pageImageStream);
                    pageImageStream.Position = 0;

                    // Use BarCodeReader to decode any barcodes present in the image
                    using (var reader = new BarCodeReader(pageImageStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Page {pageNumber}: CodeText = {result.CodeText}, Symbology = {result.CodeTypeName}, Quality = {result.ReadingQuality}");
                        }
                    }
                }
            }
        }
    }
}