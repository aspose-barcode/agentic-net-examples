// Title: Embed HIBC LIC Barcode into a Word Document using Aspose.Words
// Description: Demonstrates generating a HIBC LIC barcode with Aspose.BarCode and inserting it into an existing Word document via Aspose.Words.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Words integration category, showcasing how to create complex barcodes (HIBC LIC) using ComplexBarcodeGenerator and embed the resulting image into a Word file with DocumentBuilder. Typical scenarios include adding product identification barcodes to reports, invoices, or label templates. Developers often need to combine barcode generation with document automation, leveraging classes such as HIBCLICPrimaryDataCodetext, ComplexBarcodeGenerator, Document, and DocumentBuilder.
// Prompt: Embed a generated HIBC LIC barcode into an existing Word document using Aspose.Words for .NET.
// Tags: hibc, lic, barcode, generation, embedding, word, aspose.barcode, aspose.words, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Words;

/// <summary>
/// Sample program that creates a HIBC LIC barcode and embeds it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, inserts it into a Word file, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source and destination Word documents
        string inputDocPath = "input.docx";
        string outputDocPath = "output.docx";

        // If the input document does not exist, create a minimal Word file with placeholder text
        if (!File.Exists(inputDocPath))
        {
            var newDoc = new Document();
            var newBuilder = new DocumentBuilder(newDoc);
            newBuilder.Writeln("Sample document with embedded HIBC LIC barcode:");
            newDoc.Save(inputDocPath);
        }

        // Configure the primary data for the HIBC LIC barcode
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        using (var barcodeStream = new MemoryStream())
        {
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading

            // Load the existing Word document, insert the barcode image, and save the updated file
            var doc = new Document(inputDocPath);
            var builder = new DocumentBuilder(doc);
            builder.InsertImage(barcodeStream);
            doc.Save(outputDocPath);
        }

        Console.WriteLine($"Barcode embedded successfully. Output saved to '{outputDocPath}'.");
    }
}