using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;

class Program
{
    static void Main()
    {
        // Paths for the EMF file and the Word document
        string emfPath = "pdf417.emf";
        string docPath = "Pdf417Document.docx";

        // Create a PDF417 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Optional: set resolution or other parameters if needed
            generator.Parameters.Resolution = 300;

            // Save the barcode as EMF, handling evaluation mode restrictions
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                if (ex.Message != null && ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license. Please apply a license before using this feature.");
                    return;
                }
                throw;
            }
        }

        // Verify that the EMF file was created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("Failed to create the EMF file.");
            return;
        }

        // Read the EMF file into a byte array
        byte[] emfBytes = File.ReadAllBytes(emfPath);

        // Create a new Word document and insert the EMF image
        var doc = new Document();
        var builder = new DocumentBuilder(doc);
        // Insert the image with a reasonable size (width and height in points)
        builder.InsertImage(emfBytes, 300.0, 100.0);
        doc.Save(docPath);

        Console.WriteLine($"PDF417 barcode saved as EMF at '{emfPath}' and embedded into Word document '{docPath}'.");
    }
}