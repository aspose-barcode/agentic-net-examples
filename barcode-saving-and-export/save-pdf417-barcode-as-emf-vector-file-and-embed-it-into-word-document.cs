using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

/// <summary>
/// Demonstrates generating a PDF417 barcode, saving it as EMF, and embedding it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate EMF image and the final Word document.
        string emfPath = "pdf417.emf";
        string wordPath = "Pdf417Barcode.docx";

        // Generate a PDF417 barcode and save it as an EMF image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // EMF saving may fail in evaluation mode, so wrap it in a try‑catch block.
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"Barcode saved as EMF to '{emfPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving EMF: {ex.Message}");
                return; // Abort if the image could not be saved.
            }
        }

        // Ensure the EMF file was created before attempting to embed it.
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("EMF file not found. Cannot embed into Word document.");
            return;
        }

        // Create a new Word document and obtain a builder for inserting content.
        var doc = new Document();
        var builder = new DocumentBuilder(doc);

        // Open the EMF file as a stream and insert it into the document.
        using (var emfStream = File.OpenRead(emfPath))
        {
            builder.InsertImage(emfStream);
        }

        // Save the populated Word document to disk.
        doc.Save(wordPath);
        Console.WriteLine($"Word document with embedded barcode saved to '{wordPath}'.");
    }
}