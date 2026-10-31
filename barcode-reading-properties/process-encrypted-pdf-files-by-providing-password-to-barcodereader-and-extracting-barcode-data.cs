// Title: Extract Barcodes from Encrypted PDF Using Aspose.BarCode and Aspose.Pdf
// Description: Demonstrates how to open a password‑protected PDF, render its pages to images, and read barcodes such as PDF417, QR, DataMatrix, and Aztec using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, illustrating the use of Aspose.Pdf to open encrypted documents and Aspose.BarCode's BarCodeReader to detect multiple symbologies. Developers working with secure PDFs often need to extract embedded barcode data for inventory, tracking, or verification purposes. The sample showcases key classes like Document, PdfConverter, Resolution, and BarCodeReader, covering typical scenarios of password handling, page rendering, and barcode decoding.
// Prompt: Process encrypted PDF files by providing password to BarCodeReader and extracting barcode data.
// Tags: pdf, encryption, barcode, pdf417, qr, datamatrix, aztec, aspose.barcode, aspose.pdf, barcodereader, password

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Sample program that opens an encrypted PDF, converts each page to an image,
/// and extracts barcode information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads an encrypted PDF, renders pages, and prints detected barcodes.
    /// </summary>
    static void Main()
    {
        // Define the path to the encrypted PDF and its password
        string pdfPath = "encrypted_sample.pdf";
        string password = "myPassword";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF document using the supplied password
        using (var pdfDoc = new Document(pdfPath, password))
        {
            // Initialize a PDF converter to render pages as images
            var pdfConverter = new PdfConverter(pdfDoc);
            pdfConverter.RenderingOptions.BarcodeOptimization = true; // Optimize for barcode rendering
            pdfConverter.Resolution = new Resolution(300); // Set image resolution (DPI)
            pdfConverter.StartPage = 1; // Start from the first page
            pdfConverter.EndPage = pdfDoc.Pages.Count; // Process all pages
            pdfConverter.DoConvert(); // Begin conversion

            // Iterate through each rendered page image
            while (pdfConverter.HasNextImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    // Render the current page into the memory stream (PNG format by default)
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Create a BarCodeReader to detect multiple barcode types in the image
                    using (var reader = new BarCodeReader(imageStream, DecodeType.Pdf417, DecodeType.QR, DecodeType.DataMatrix, DecodeType.Aztec))
                    {
                        // Read and output each detected barcode
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
                        }
                    }
                }
            }

            // Release resources used by the converter
            pdfConverter.Close();
        }
    }
}