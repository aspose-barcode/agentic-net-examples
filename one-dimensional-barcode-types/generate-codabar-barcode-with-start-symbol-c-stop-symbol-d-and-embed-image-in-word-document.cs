using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

/// <summary>
/// Demonstrates generating a Codabar barcode using Aspose.BarCode,
/// saving it as an image, and embedding it into a Word document using Aspose.Words.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, creates a Word document, inserts the barcode image, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the output directory inside the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);

        // Build full file paths for the barcode image and the resulting Word document
        string barcodePath = Path.Combine(outputDir, "codabar.png");
        string wordPath = Path.Combine(outputDir, "CodabarDocument.docx");

        // Generate a Codabar barcode with start symbol 'C' and stop symbol 'D'
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Configure start and stop symbols for Codabar
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a new Word document
        var doc = new Document();
        // Initialize a DocumentBuilder to modify the document
        var builder = new DocumentBuilder(doc);
        // Insert the previously saved barcode image into the document
        builder.InsertImage(barcodePath);
        // Save the document in DOCX format
        doc.Save(wordPath, SaveFormat.Docx);

        // Write the locations of the generated files to the console
        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Word document saved to: {wordPath}");
    }
}