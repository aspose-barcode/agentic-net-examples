// Title: Embed HIBC LIC barcode into Word document using Aspose
// Description: Demonstrates generating a HIBC LIC barcode with Aspose.BarCode and embedding it into a Word document via Aspose.Words. The resulting DOCX can be used for product labeling.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Words integration category, showing how to create complex HIBC barcodes (using ComplexBarcodeGenerator) and insert the generated image into a Word file. Developers working on document automation, labeling, or reporting often need to combine barcode generation with document creation, using classes like ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, Document, DocumentBuilder, and image insertion methods. It serves as a reference for typical use cases such as generating product labels, compliance documents, and automated reports that require embedded barcodes.
/// Prompt: Embed a generated HIBC LIC barcode into an existing Word document using Aspose.Words for .NET.
/// Tags: hibc lic barcode generation, word document, aspose.words, aspose.barcode, c#, .net

using System;
using System.IO;
using Aspose.Words;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates embedding a generated HIBC LIC barcode into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, inserts it into a new Word document, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated Word document.
        string outputDocPath = Path.Combine(Path.GetTempPath(), "HIBCLIC_Word.docx");

        // Create a new empty Word document and a builder to add content.
        Document doc = new Document();
        DocumentBuilder builder = new DocumentBuilder(doc);
        builder.Writeln("Document with HIBC LIC barcode:");

        // Prepare primary data required for the HIBC LIC barcode.
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Wrap the primary data in a codetext object specifying the barcode type.
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };

        // Generate the barcode image using the complex barcode generator.
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set image resolution to 300 dpi for high-quality output.
            generator.Parameters.Resolution = 300;

            // Create a bitmap of the barcode.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert bitmap pixel dimensions to points (1 inch = 72 points) for Word insertion.
                    float widthPoints = (bitmap.Width * 72f) / generator.Parameters.Resolution;
                    float heightPoints = (bitmap.Height * 72f) / generator.Parameters.Resolution;

                    // Insert the barcode image into the Word document at the current cursor position.
                    builder.InsertImage(imageBytes, widthPoints, heightPoints);
                }
            }
        }

        // Save the populated Word document to the specified path.
        doc.Save(outputDocPath, Aspose.Words.SaveFormat.Docx);
        Console.WriteLine($"Word document saved to: {outputDocPath}");
    }
}