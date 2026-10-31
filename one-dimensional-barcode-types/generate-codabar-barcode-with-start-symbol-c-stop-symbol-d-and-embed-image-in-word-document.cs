// Title: Generate Codabar barcode and embed in Word document
// Description: Demonstrates creating a Codabar barcode with specific start/stop symbols, rendering it as a PNG image, and inserting the image into a Word .docx file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters (resolution, start/stop symbols) using BarcodeGenerator, render the barcode to an image, and embed that image into a Word document via Aspose.Words. Typical use cases include generating printable barcodes for inventory, shipping labels, or reports where the barcode must appear inside a Word file. Developers often need to combine barcode creation with document automation, using classes like BarcodeGenerator, Bitmap, Document, and DocumentBuilder.
/// Prompt: Generate a Codabar barcode with start symbol C, stop symbol D, and embed the image in a Word document.
/// Tags: codabar, barcode generation, word document, aspose.barcode, aspose.words, image embedding, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Words;

/// <summary>
/// Example program that creates a Codabar barcode, saves it as a PNG image,
/// and inserts the image into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, embeds it in a Word document, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated Word document.
        string outputDocPath = Path.Combine(Path.GetTempPath(), "CodabarBarcode.docx");

        // Initialize the barcode generator for Codabar with the full code text,
        // including the start (C) and stop (D) symbols.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "C12345D"))
        {
            // Set the image resolution (dots per inch). Higher values produce larger images.
            generator.Parameters.Resolution = 300;

            // Explicitly set the start and stop symbols to C and D.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Generate a bitmap to obtain the barcode's pixel dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the barcode image to a memory stream in PNG format.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Create a new Word document and a builder to edit its content.
                    Document doc = new Document();
                    DocumentBuilder builder = new DocumentBuilder(doc);

                    // Convert bitmap dimensions from pixels to points (1 point = 1/72 inch).
                    double widthInPoints = (bitmap.Width * 72.0) / generator.Parameters.Resolution;
                    double heightInPoints = (bitmap.Height * 72.0) / generator.Parameters.Resolution;

                    // Insert the barcode image into the document with the calculated size.
                    builder.InsertImage(imageStream.ToArray(), widthInPoints, heightInPoints);

                    // Save the Word document to the specified path in DOCX format.
                    doc.Save(outputDocPath, Aspose.Words.SaveFormat.Docx);
                }
            }
        }

        // Inform the user where the document was saved.
        Console.WriteLine("Word document with Codabar barcode saved to:");
        Console.WriteLine(outputDocPath);
    }
}