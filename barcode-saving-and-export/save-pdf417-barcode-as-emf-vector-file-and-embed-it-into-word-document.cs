// Title: Save PDF417 barcode as EMF and embed in Word document
// Description: Demonstrates generating a PDF417 barcode, exporting it as an EMF vector image, and inserting it into a Word document.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Words document manipulation category. It showcases the use of BarcodeGenerator (Aspose.BarCode.Generation) to create a PDF417 barcode, the BarCodeImageFormat enumeration to export the barcode as an EMF vector file, and the Document/DocumentBuilder classes (Aspose.Words) to embed the image into a Word document. Developers often need to generate high‑quality barcodes for print media and embed them directly into office documents, making this pattern a common requirement.
// Prompt: Save a PDF417 barcode as an EMF vector file and embed it into a Word document.
// Tags: pdf417, barcode, emf, word, aspose.barcode, aspose.words, generation, embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Words.Drawing;

/// <summary>
/// Generates a PDF417 barcode, saves it as an EMF file, and embeds the image into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, EMF export, and Word embedding steps.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the PDF417 barcode
        const string codeText = "Sample PDF417 Text";

        // Define output file paths for the EMF image and the Word document
        const string emfPath = "pdf417.emf";
        const string docPath = "Pdf417Document.docx";

        // ------------------------------------------------------------
        // Generate PDF417 barcode and save it as an EMF vector image
        // ------------------------------------------------------------
        try
        {
            // Initialize the barcode generator with PDF417 symbology and the sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
            {
                // Export the generated barcode to an EMF file (vector format)
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            // Handle evaluation version limitation for EMF export gracefully
            if (ex.Message != null && ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required for EMF export of this barcode type.");
                return;
            }

            // Re‑throw any other unexpected exceptions
            throw;
        }

        // Verify that the EMF file was successfully created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine($"Failed to create EMF file at '{emfPath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Create a new Word document and embed the EMF barcode image
        // ------------------------------------------------------------
        var doc = new Document();
        var builder = new DocumentBuilder(doc);

        // Insert the EMF image at the current cursor position
        builder.InsertImage(emfPath);

        // Save the Word document with the embedded barcode
        doc.Save(docPath);

        // Output the locations of the generated files
        Console.WriteLine($"PDF417 barcode saved as EMF: {Path.GetFullPath(emfPath)}");
        Console.WriteLine($"Word document created with embedded barcode: {Path.GetFullPath(docPath)}");
    }
}