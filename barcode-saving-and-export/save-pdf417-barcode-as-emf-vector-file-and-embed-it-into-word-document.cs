// Title: Save PDF417 barcode as EMF and embed into Word document
// Description: Demonstrates generating a PDF417 barcode, exporting it as an EMF vector image, and inserting that image into a Word document.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Words document manipulation category. It showcases the use of BarcodeGenerator (Aspose.BarCode.Generation) to create a PDF417 symbology, the BarCodeImageFormat.Emf format for vector graphics, and Document/DocumentBuilder (Aspose.Words) for embedding images into Word files. Developers often need to create high‑resolution, scalable barcodes for print or digital documents and then programmatically insert them into Office documents.
// Prompt: Save a PDF417 barcode as an EMF vector file and embed it into a Word document.
// Tags: pdf417, barcode, emf, word, aspose.barcode, aspose.words, image-embedding, vector-format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

/// <summary>
/// Generates a PDF417 barcode, saves it as an EMF vector file, and embeds the image into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, EMF export, and Word insertion steps.
    /// </summary>
    static void Main()
    {
        // Define output file paths for the EMF image and the Word document.
        string emfPath = "Pdf417Barcode.emf";
        string docPath = "Pdf417Barcode.docx";

        // Initialize a BarcodeGenerator for PDF417 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // -----------------------------------------------------------------
            // Save the generated barcode as an EMF file (vector format) on disk.
            // -----------------------------------------------------------------
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"EMF file saved to: {Path.GetFullPath(emfPath)}");
            }
            catch (Exception ex)
            {
                // EMF export requires a licensed version of Aspose.BarCode.
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                    return;
                }
                throw;
            }

            // ---------------------------------------------------------------
            // Save the barcode to a memory stream for embedding into a Word doc.
            // ---------------------------------------------------------------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Emf);
                ms.Position = 0; // Reset stream position before reading.

                // Create a new Word document and insert the EMF image.
                var doc = new Document();
                var builder = new DocumentBuilder(doc);
                builder.InsertImage(ms);

                // Save the Word document to the specified path.
                doc.Save(docPath);
                Console.WriteLine($"Word document saved to: {Path.GetFullPath(docPath)}");
            }
        }
    }
}