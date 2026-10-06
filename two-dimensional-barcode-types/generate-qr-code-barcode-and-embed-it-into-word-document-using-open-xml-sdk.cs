// Title: Generate QR Code and embed it into a Word document using Aspose.BarCode and Aspose.Words
// Description: This example creates a QR Code barcode from a URL, converts it to a PNG image, and inserts the image into a Word document.
// Category-Description: Demonstrates how to generate a barcode image with Aspose.BarCode, manipulate its dimensions, and embed the image into a Word file using Aspose.Words (Open XML SDK). The sample showcases the BarcodeGenerator, EncodeTypes, BarCodeImageFormat, Document, and DocumentBuilder classes—common tools for developers who need to add barcodes to Office documents for reporting, labeling, or automated document generation.
// Prompt: Generate QR Code barcode and embed it into a Word document using Open XML SDK.
// Tags: qr code, barcode generation, image embedding, word document, aspose.barcode, aspose.words, openxml, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode and inserting it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, converts it to PNG, and embeds it in a DOCX file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Text to encode in the QR Code
        string qrText = "https://example.com";

        // Desired image resolution (dots per inch)
        int resolution = 300;

        // Initialize the barcode generator for QR code with the specified text
        var generator = new BarcodeGenerator(EncodeTypes.QR, qrText);
        generator.Parameters.Resolution = resolution;

        // Generate a bitmap to obtain the pixel dimensions of the QR Code
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Save the barcode image to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                byte[] imageBytes = imageStream.ToArray();

                // Convert pixel dimensions to points (1 inch = 72 points) for Word insertion
                double widthPoints = (bitmap.Width * 72.0) / resolution;
                double heightPoints = (bitmap.Height * 72.0) / resolution;

                // Create a new Word document and insert the barcode image
                var doc = new Document();
                var builder = new DocumentBuilder(doc);
                builder.Write("QR Code:");
                builder.InsertImage(imageBytes, widthPoints, heightPoints);
                builder.Writeln();

                // Define the output path and save the document as DOCX
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "QrBarcodeWord.docx");
                doc.Save(outputPath, SaveFormat.Docx);
                Console.WriteLine($"Document saved to: {outputPath}");
            }
        }
    }
}