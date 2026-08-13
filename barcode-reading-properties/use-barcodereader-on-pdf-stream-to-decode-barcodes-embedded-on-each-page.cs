// Title: Decode barcodes from each page of a PDF using BarCodeReader
// Description: Demonstrates how to read a PDF file as a stream and extract all barcodes on every page.
// Category-Description: This example belongs to the Aspose.BarCode PDF barcode recognition category. It shows how to use BarCodeReader with a PDF stream to detect any supported barcode symbology across multiple pages. Developers often need to batch‑process PDFs to retrieve embedded barcodes for inventory, shipping, or document automation tasks.
// Prompt: Use BarCodeReader on a PDF stream to decode barcodes embedded on each page.
// Tags: pdf, barcode, decoding, barcodereader, aspnet, aspnetcore, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode decoding from a PDF file using Aspose.BarCode's BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a PDF stream, scans each page for barcodes, and prints their type and text.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file containing barcodes
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the reader to detect all supported barcode types
            using (BarCodeReader reader = new BarCodeReader(pdfStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the PDF
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the barcode type and decoded text
                    Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Barcode Text: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }
}