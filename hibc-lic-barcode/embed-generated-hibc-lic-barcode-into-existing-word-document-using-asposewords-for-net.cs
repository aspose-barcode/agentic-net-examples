// Title: Embed HIBC LIC Barcode into a Word Document using Aspose.Words
// Description: This example shows how to generate a HIBC LIC barcode with Aspose.BarCode and embed the resulting image into a Word document using Aspose.Words for .NET.
// Category-Description: The sample belongs to the Aspose.BarCode generation and integration category, illustrating the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and DocumentBuilder to create barcode images and insert them into Office documents. Developers often need to combine barcode creation with document automation for labeling, packaging, and compliance reporting. The example demonstrates typical workflows for generating high‑resolution barcodes and embedding them in DOCX files.
// Prompt: Embed a generated HIBC LIC barcode into an existing Word document using Aspose.Words for .NET.
// Tags: hibc, lic, barcode, generation, docx, aspose.barcode, aspose.words

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Words;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a HIBC LIC barcode and inserting it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, creates a DOCX file, and embeds the barcode image.
    /// </summary>
    static void Main()
    {
        const int resolution = 300; // DPI for barcode image generation

        // Prepare primary data for HIBC LIC barcode
        var primaryData = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(primaryData))
        {
            // Set image resolution (dots per inch)
            generator.Parameters.Resolution = resolution;

            // Create bitmap to obtain dimensions of the generated barcode
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save barcode image to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    byte[] imageBytes = imageStream.ToArray();

                    // Create a new Word document and a builder for content insertion
                    var wordDoc = new Document();
                    var builder = new DocumentBuilder(wordDoc);

                    // Insert introductory text before the barcode
                    builder.Writeln("Document with HIBC LIC barcode:");

                    // Convert bitmap dimensions from pixels to points (1 inch = 72 points)
                    double widthPoints = (bitmap.Width * 72.0) / resolution;
                    double heightPoints = (bitmap.Height * 72.0) / resolution;

                    // Insert the barcode image with calculated size
                    builder.InsertImage(imageBytes, widthPoints, heightPoints);

                    // Insert concluding text after the barcode
                    builder.Writeln("\nEnd of document.");

                    // Save the Word document to the current directory
                    string outputPath = Path.Combine(Environment.CurrentDirectory, "HIBCLIC_Barcode.docx");
                    wordDoc.Save(outputPath, SaveFormat.Docx);

                    Console.WriteLine($"Word document saved to: {outputPath}");
                }
            }
        }
    }
}