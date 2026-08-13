// Title: Generate Codabar barcode and embed in Word document
// Description: Creates a Codabar barcode with start symbol C and stop symbol D, renders it as a PNG image in memory, and inserts the image into a Word document.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate barcodes and Aspose.Words to embed images into Word files. It covers barcode generation (EncodeTypes, BarcodeGenerator), configuring barcode parameters (Codabar start/stop symbols), saving the barcode to a stream, and inserting the image into a document via DocumentBuilder. Ideal for developers needing to automate document creation with barcodes for inventory, shipping, or tracking.
// Prompt: Generate a Codabar barcode with start symbol C, stop symbol D, and embed the image in a Word document.
// Tags: codabar, barcode generation, image embedding, word document, aspose.barcode, aspose.words

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

/// <summary>
/// Demonstrates generating a Codabar barcode with custom start/stop symbols
/// and embedding the resulting image into a Word document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, inserts it into a Word file,
    /// and saves the document to disk.
    /// </summary>
    static void Main()
    {
        // Define the output Word document path
        string outputDocPath = "Codabar.docx";

        // Initialize a barcode generator for Codabar with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Configure start and stop symbols to C and D respectively
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Render the barcode to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading

                // Create a new Word document and insert the barcode image
                var doc = new Document();
                var builder = new DocumentBuilder(doc);
                builder.InsertImage(imageStream);

                // Save the Word document to the specified path
                doc.Save(outputDocPath);
            }
        }

        // Output the full path of the generated document
        Console.WriteLine($"Codabar barcode embedded in Word document: {Path.GetFullPath(outputDocPath)}");
    }
}