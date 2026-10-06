// Title: Generate Codabar barcode with custom start/stop symbols and embed in Word document
// Description: Demonstrates creating a Codabar barcode with start symbol C and stop symbol D, saving it as PNG, and inserting the image into a Word document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters (resolution, X‑dimension, start/stop symbols) using BarcodeGenerator and EncodeTypes.Codabar, then embed the generated image into a document using Aspose.Words. Developers often need to produce printable barcodes and combine them with office documents for reports, invoices, or labels.
// Prompt: Generate a Codabar barcode with start symbol C, stop symbol D, and embed the image in a Word document.
// Tags: codabar, barcode, generation, image, word, aspose.barcode, aspose.words, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Codabar barcode with specific start/stop symbols,
/// saves it as an image, and embeds the image into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Paths for the barcode image and the resulting Word document
        string barcodePath = Path.Combine(outputDir, "CodabarC_D.png");
        string wordPath = Path.Combine(outputDir, "CodabarDocument.docx");

        // Initialize the barcode generator for Codabar with the data "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Configure barcode appearance
            generator.Parameters.Resolution = 300f; // DPI for high‑quality image
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C; // Set start symbol
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;  // Set stop symbol
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Width of the smallest bar

            // Save the barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);

            // Generate the barcode image in memory
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    // Write the image to a memory stream (PNG format)
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Create a new Word document and insert the barcode image
                    var doc = new Document();
                    var builder = new DocumentBuilder(doc);
                    builder.Writeln("Codabar barcode with start C and stop D:");

                    // Convert pixel dimensions to points (1 point = 1/72 inch)
                    float widthPoints = (bitmap.Width * 72f) / generator.Parameters.Resolution;
                    float heightPoints = (bitmap.Height * 72f) / generator.Parameters.Resolution;

                    // Insert the image using the calculated size
                    builder.InsertImage(imageBytes, widthPoints, heightPoints);

                    // Save the Word document
                    doc.Save(wordPath, SaveFormat.Docx);
                }
            }
        }

        // Inform the user where the files were saved
        Console.WriteLine("Barcode image saved to: " + barcodePath);
        Console.WriteLine("Word document saved to: " + wordPath);
    }
}