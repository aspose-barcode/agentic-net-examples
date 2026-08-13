// Title: Batch decode Swiss Post Parcel barcodes from a PDF
// Description: Demonstrates generating a PDF with a Swiss Post Parcel barcode and then decoding all such barcodes from each page of the PDF.
// Category-Description: This example belongs to the Aspose.BarCode for .NET suite, showcasing barcode generation (BarcodeGenerator) and recognition (BarCodeReader) together with Aspose.Pdf conversion (PdfConverter). Typical scenarios include batch processing of documents to extract Swiss Post Parcel service codes for logistics and tracking. Developers often need to render PDF pages to images, enable barcode optimization, and read multiple barcodes efficiently.
// Prompt: Perform batch decoding of Swiss Post Parcel additional service code barcodes from a PDF and extract human‑readable text.
// Tags: swisspostparcel, batch decoding, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates creating a sample PDF containing a Swiss Post Parcel barcode
/// and then batch‑decoding all such barcodes from each page of the PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Ensures a sample PDF exists and triggers barcode decoding.
    /// </summary>
    static void Main()
    {
        // Path to the PDF that will be processed
        const string pdfPath = "SwissPostParcelSample.pdf";

        // If the PDF does not exist, create a simple sample PDF containing a Swiss Post Parcel barcode
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // Decode Swiss Post Parcel barcodes from the PDF
        DecodeBarcodesFromPdf(pdfPath);
    }

    // Creates a PDF with a single page that contains a Swiss Post Parcel barcode
    private static void CreateSamplePdf(string path)
    {
        // Sample code text for Swiss Post Parcel (including an additional service code example)
        const string sampleCodeText = "1234567890AB";

        // Generate barcode image into a memory stream (PNG format)
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleCodeText))
            {
                // Save the barcode as PNG directly to the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;
            }

            // Create a new PDF document and add the barcode image to the first page
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();
                var image = new Aspose.Pdf.Image
                {
                    ImageStream = new MemoryStream(barcodeStream.ToArray())
                };
                page.Paragraphs.Add(image);
                pdfDoc.Save(path);
            }
        }

        Console.WriteLine($"Sample PDF created at '{path}'.");
    }

    // Decodes all Swiss Post Parcel barcodes from each page of the specified PDF
    private static void DecodeBarcodesFromPdf(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: PDF file '{pdfPath}' does not exist.");
            return;
        }

        // Open the PDF document
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize the PDF converter for rendering pages to images
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better extraction
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                int pageCount = pdfDoc.Pages.Count;

                // Iterate through each page in the PDF
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to process a single page
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Render the current page to an image stream
                    using (var pageImageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageImageStream);
                        pageImageStream.Position = 0;

                        // Read barcodes from the rendered image
                        using (var reader = new BarCodeReader(pageImageStream, DecodeType.SwissPostParcel))
                        {
                            var results = reader.ReadBarCodes();

                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageNumber}: No Swiss Post Parcel barcode detected.");
                            }
                            else
                            {
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Detected barcode");
                                    Console.WriteLine($"  Type    : {result.CodeTypeName}");
                                    Console.WriteLine($"  CodeText: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}