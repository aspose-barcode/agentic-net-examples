// Title: Save PDF417 barcode as EMF and embed in Word document
// Description: Demonstrates generating a PDF417 barcode, exporting it as an EMF vector image, and inserting it into a Word document.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Words integration category, showing how to use BarcodeGenerator to create PDF417 barcodes, export them in vector formats (EMF) and embed the resulting image into a Word document using DocumentBuilder. Typical use cases include generating printable barcodes for reports, invoices, or forms where high‑resolution vector graphics are required. Developers often need to combine barcode generation with document automation to produce dynamic documents.
// Prompt: Save a PDF417 barcode as an EMF vector file and embed it into a Word document.
// Tags: pdf417, barcode, emf, vector, word, aspose.barcode, aspose.words, document generation, image insertion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a PDF417 barcode, saves it as an EMF vector image,
/// and embeds the image into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Barcode generation settings
        const int resolution = 300;               // DPI for the generated barcode image
        const int leftBarcodePosition = 10;       // Horizontal position (points) in the Word document
        const int topBarcodePosition = 20;        // Vertical position (points) in the Word document
        const string codeText = "Aspose.BarCode Pdf417 Example";

        // Prepare output directory and document path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string docPath = Path.Combine(outputDir, "Pdf417Barcode.docx");

        // Initialize the barcode generator for PDF417 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the image resolution (affects size conversion later)
            generator.Parameters.Resolution = resolution;

            // Generate the barcode as a bitmap (required for size calculations)
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Export the barcode to an EMF stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    try
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Emf);
                    }
                    catch (Exception ex)
                    {
                        // EMF export is only available with a licensed version
                        if (ex.Message.Contains("evaluation"))
                        {
                            Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                            return;
                        }
                        throw;
                    }

                    // Retrieve the EMF byte array
                    byte[] emfBytes = imageStream.ToArray();

                    // Create a new Word document and a builder for content insertion
                    var doc = new Document();
                    var builder = new DocumentBuilder(doc);

                    // Write introductory text
                    builder.Write("First Sentence.");

                    // Insert the EMF barcode image at the specified position
                    builder.InsertImage(
                        emfBytes,
                        RelativeHorizontalPosition.Page,
                        leftBarcodePosition,
                        RelativeVerticalPosition.Page,
                        topBarcodePosition,
                        (image.Width * 72.0) / resolution,   // Convert width from pixels to points
                        (image.Height * 72.0) / resolution, // Convert height from pixels to points
                        WrapType.Square);

                    // Write trailing text
                    builder.Write("Second Sentence.");

                    // Save the document in DOCX format
                    doc.Save(docPath, SaveFormat.Docx);
                    Console.WriteLine($"Document saved to {docPath}");
                }
            }
        }
    }
}