// Title: Save PDF417 barcode as EMF and embed into Word document
// Description: Demonstrates generating a PDF417 barcode, exporting it as an EMF vector image, and inserting it into a Word document.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Words document manipulation category. It shows how to use BarcodeGenerator (Aspose.BarCode) to create a PDF417 barcode, export the barcode to a vector EMF format, and embed the image into a Word file using Document and DocumentBuilder (Aspose.Words). Developers often need to generate barcodes for printing or digital documents and embed them in Office files while preserving high‑quality vector graphics.
// Prompt: Save a PDF417 barcode as an EMF vector file and embed it into a Word document.
// Tags: pdf417, barcode, emf, word, aspose.barcode, aspose.words, image-embedding, vector-format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode, saves it as an EMF file, and embeds it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, exports it, and builds the Word file.
    /// </summary>
    static void Main()
    {
        // Barcode generation settings
        const int resolution = 300;               // DPI for the generated image
        const int leftBarcodePosition = 10;       // Horizontal offset in points
        const int topBarcodePosition = 20;        // Vertical offset in points
        const string codeText = "Aspose.Barcode Pdf417 Example";

        // Prepare output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for EMF and Word document
        string emfPath = Path.Combine(outputDir, "Pdf417Barcode.emf");
        string docPath = Path.Combine(outputDir, "Pdf417Barcode.docx");

        // Create a barcode generator for PDF417
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set image resolution (affects size calculations later)
            generator.Parameters.Resolution = resolution;

            // Attempt to save the barcode directly as EMF (requires a license)
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                // Inform the user if the evaluation version blocks EMF export
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                    return;
                }
                throw;
            }

            // Generate a bitmap to obtain the barcode dimensions in pixels
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the EMF image to a memory stream for embedding into Word
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Emf);
                    ms.Position = 0; // Reset stream position before reading

                    // Create a new Word document and a builder for content insertion
                    var wordDoc = new Document();
                    var builder = new DocumentBuilder(wordDoc);

                    // Write introductory text
                    builder.Write("First Sentence.");

                    // Insert the EMF barcode image with calculated size (points) and positioning
                    builder.InsertImage(
                        ms.ToArray(),
                        RelativeHorizontalPosition.Page,
                        leftBarcodePosition,
                        RelativeVerticalPosition.Page,
                        topBarcodePosition,
                        (bitmap.Width * 72) / resolution,   // Convert width from pixels to points
                        (bitmap.Height * 72) / resolution, // Convert height from pixels to points
                        WrapType.Square);

                    // Write trailing text
                    builder.Write("Second Sentence.");

                    // Save the Word document to the specified path
                    wordDoc.Save(docPath, SaveFormat.Docx);
                }
            }
        }

        // Output the locations of the generated files
        Console.WriteLine($"EMF file saved to: {emfPath}");
        Console.WriteLine($"Word document saved to: {docPath}");
    }
}