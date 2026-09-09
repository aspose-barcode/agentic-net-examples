// Title: Generate Codabar Barcode and Embed in Word Document
// Description: This example creates a Codabar barcode with start symbol C and stop symbol D, renders it as a PNG image, and inserts it into a Word document.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes, CodabarSymbol) combined with Aspose.Words document creation (Document, DocumentBuilder). Typical use cases include adding barcodes to reports, invoices, or labels generated programmatically. Developers often need to customize barcode parameters, export images, and embed them into Office documents.
// Prompt: Generate a Codabar barcode with start symbol C, stop symbol D, and embed the image in a Word document.
// Tags: codabar, barcode generation, word document, aspose.barcode, aspose.words, image embedding, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Codabar barcode with specific start/stop symbols
/// and embedding the resulting image into a Word document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the barcode,
    /// converts it to PNG, inserts it into a Word file, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string wordPath = Path.Combine(outputDir, "CodabarDocument.docx");

        // Initialize barcode generator for Codabar with data "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Set image resolution and X-dimension (pixel width of narrow bar)
            generator.Parameters.Resolution = 300;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure start and stop symbols for Codabar
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Generate a bitmap to obtain the image dimensions
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int imgWidth = bitmap.Width;
                int imgHeight = bitmap.Height;

                // Save the barcode image to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    byte[] imageBytes = imageStream.ToArray();

                    // Create a new Word document and a builder to insert content
                    var doc = new Document();
                    var builder = new DocumentBuilder(doc);

                    // Convert pixel dimensions to points (1 point = 1/72 inch)
                    float widthPoints = (float)(imgWidth * 72.0 / generator.Parameters.Resolution);
                    float heightPoints = (float)(imgHeight * 72.0 / generator.Parameters.Resolution);

                    // Insert the barcode image into the document with calculated size
                    builder.InsertImage(imageBytes, widthPoints, heightPoints);

                    // Save the Word document to the specified path
                    doc.Save(wordPath);
                }
            }
        }

        // Inform the user where the document was saved
        Console.WriteLine("Word document with Codabar barcode created at:");
        Console.WriteLine(wordPath);
    }
}