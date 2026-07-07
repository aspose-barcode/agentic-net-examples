// Title: Generate Codabar barcode and embed it in a Word document
// Description: Demonstrates creating a Codabar barcode with start symbol C and stop symbol D, saving it as a PNG image, and inserting the image into a Word document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator (Aspose.BarCode.Generation) together with Aspose.Words to produce printable documents. Typical use cases include generating inventory labels, shipping tags, or any printable media that requires barcode data embedded in Word files. Developers often need to configure symbology settings, export barcode images, and programmatically manipulate Word documents.
// Prompt: Generate a Codabar barcode with start symbol C, stop symbol D, and embed the image in a Word document.
// Tags: codabar, barcode generation, word document, aspose.barcode, aspose.words, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

/// <summary>
/// Example program that creates a Codabar barcode, saves it as an image,
/// and embeds the image into a Word document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the temporary barcode image and the final Word document.
        string imagePath = "codabar.png";
        string docPath = "Codabar.docx";

        // Remove any existing files to ensure a clean run.
        if (File.Exists(imagePath))
            File.Delete(imagePath);
        if (File.Exists(docPath))
            File.Delete(docPath);

        // Generate a Codabar barcode with start symbol C and stop symbol D.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the data to encode (excluding start/stop symbols).
            generator.CodeText = "123456";

            // Configure the start and stop symbols for Codabar.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath);
        }

        // Create a new Word document and insert the barcode image.
        var doc = new Document();
        var builder = new DocumentBuilder(doc);
        builder.InsertImage(imagePath);
        doc.Save(docPath);

        // Clean up the temporary barcode image file.
        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }
}