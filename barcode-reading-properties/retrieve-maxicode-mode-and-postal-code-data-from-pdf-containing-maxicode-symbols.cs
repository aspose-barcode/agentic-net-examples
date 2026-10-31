// Title: Retrieve MaxiCode mode and postal code from PDF pages
// Description: Demonstrates extracting the MaxiCode mode and associated postal code data from each page of a PDF that contains MaxiCode symbols using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to combine Aspose.Pdf's PDF conversion with Aspose.BarCode's BarCodeReader to decode complex barcode symbologies. It highlights key API classes such as PdfConverter, BarCodeReader, DecodeType, and ComplexCodetextReader, which are commonly used for extracting detailed information (e.g., mode, postal code) from MaxiCode symbols in documents. Developers working with document automation, logistics, or inventory systems often need to read such data programmatically.
// Prompt: Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols.
// Tags: maxicode, barcode recognition, pdf, aspose.barcode, aspose.pdf, decode, postalcode, complexcodetext

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that reads each page of a PDF, converts it to an image,
/// and extracts MaxiCode mode and postal code information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the input PDF file containing MaxiCode symbols.
        string pdfPath = "input.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.
        using (var pdfDoc = new Aspose.Pdf.Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better recognition performance.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through all pages in the PDF.
                int pageCount = pdfDoc.Pages.Count;
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Retrieve the rendered image for the current page.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a BarCodeReader to detect MaxiCode symbols in the image.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
                        {
                            // Process each detected barcode.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Retrieve the MaxiCode mode from extended parameters.
                                var mode = result.Extended.MaxiCode.Mode;
                                Console.WriteLine($"Page {pageNumber}: Detected MaxiCode mode = {mode}");

                                // Decode the complex codetext to access postal code information.
                                var complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                                if (complexCodetext is MaxiCodeCodetextMode2 mode2)
                                {
                                    Console.WriteLine($"  PostalCode: {mode2.PostalCode}");
                                }
                                else if (complexCodetext is MaxiCodeCodetextMode3 mode3)
                                {
                                    Console.WriteLine($"  PostalCode: {mode3.PostalCode}");
                                }
                                else if (complexCodetext is MaxiCodeStandardCodetext)
                                {
                                    Console.WriteLine("  Standard MaxiCode (no postal code field).");
                                }
                                else
                                {
                                    Console.WriteLine("  Unable to determine MaxiCode type.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}