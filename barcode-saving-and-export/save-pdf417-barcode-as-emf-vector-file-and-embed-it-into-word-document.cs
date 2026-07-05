// Title: Save PDF417 barcode as EMF and embed in Word
// Description: Demonstrates generating a PDF417 barcode, exporting it as an EMF vector image, and inserting the image into a Word document.
// Prompt: Save a PDF417 barcode as an EMF vector file and embed it into a Word document.
// Tags: pdf417, barcode, emf, word, aspose.barcode, aspose.words

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

/// <summary>
/// Example program that creates a PDF417 barcode, saves it as an EMF file,
/// and embeds the EMF image into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define output file paths
        string emfPath = "pdf417.emf";
        string docPath = "BarcodeDocument.docx";

        // Text to encode in the PDF417 barcode
        string codeText = "Sample PDF417 Barcode Text";

        // Generate the PDF417 barcode and save it as an EMF vector image
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
            {
                // Export the barcode to EMF format
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            // Handle evaluation version limitation for EMF export
            if (ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required for EMF export of this barcode type.");
                return;
            }

            // Re‑throw any other unexpected exceptions
            throw;
        }

        // Ensure the EMF file was created successfully
        if (!File.Exists(emfPath))
        {
            Console.WriteLine($"Failed to create EMF file at '{emfPath}'.");
            return;
        }

        // Create a new Word document and insert the EMF image
        var doc = new Document();
        var builder = new DocumentBuilder(doc);
        builder.InsertImage(emfPath);
        doc.Save(docPath);

        // Output the locations of the generated files
        Console.WriteLine($"PDF417 barcode saved as EMF: {Path.GetFullPath(emfPath)}");
        Console.WriteLine($"Word document with embedded barcode saved as: {Path.GetFullPath(docPath)}");
    }
}