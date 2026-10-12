// Title: Reusable barcode generator for MaxiCode, DataMatrix, and GS1 Composite
// Description: Demonstrates a simple factory that creates PNG images for three common symbologies using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure and save barcodes with different symbologies. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for developers needing to produce barcode images programmatically for labeling, packaging, or inventory systems.
// Prompt: Develop a reusable component that abstracts barcode generation for MaxiCode, DataMatrix, and GS1 Composite types.
// Tags: barcode, generation, maxicode, datamatrix, gs1 composite, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a factory method to generate barcode images for supported symbologies.
/// </summary>
class BarcodeFactory
{
    /// <summary>
    /// Generates a barcode image of the specified symbology and saves it to the given path.
    /// </summary>
    /// <param name="symbology">The barcode type (e.g., "MaxiCode", "DataMatrix", "GS1Composite").</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    public static void Generate(string symbology, string codeText, string outputPath)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(symbology))
            throw new ArgumentException("Symbology must be provided.", nameof(symbology));
        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));

        // Map string identifier to Aspose.EncodeTypes value
        BaseEncodeType encodeType;
        switch (symbology.Trim().ToLowerInvariant())
        {
            case "maxicode":
                encodeType = EncodeTypes.MaxiCode;
                break;
            case "datamatrix":
                encodeType = EncodeTypes.DataMatrix;
                break;
            case "gs1composite":
                encodeType = EncodeTypes.GS1CompositeBar;
                break;
            default:
                throw new ArgumentException($"Unsupported symbology: {symbology}", nameof(symbology));
        }

        // Create generator with selected type and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply common visual settings
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Resolution = 300f;

            // Additional configuration for GS1 Composite barcodes
            if (encodeType == EncodeTypes.GS1CompositeBar)
            {
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                generator.Parameters.Barcode.Pdf417.Columns = 30;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            }

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}

class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes and saves them to the local file system.
    /// </summary>
    static void Main()
    {
        // Determine output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Generate a MaxiCode barcode
        string maxiCodePath = Path.Combine(outputDir, "maxicode.png");
        BarcodeFactory.Generate(
            symbology: "MaxiCode",
            codeText: "SampleMaxiCode123",
            outputPath: maxiCodePath);
        Console.WriteLine($"Generated MaxiCode: {maxiCodePath}");

        // Generate a DataMatrix barcode
        string dataMatrixPath = Path.Combine(outputDir, "datamatrix.png");
        BarcodeFactory.Generate(
            symbology: "DataMatrix",
            codeText: "DM-2023-ABC",
            outputPath: dataMatrixPath);
        Console.WriteLine($"Generated DataMatrix: {dataMatrixPath}");

        // Generate a GS1 Composite barcode
        // Linear part must contain a valid GTIN-14 AI (01) and 2D part can be any GS1 data.
        string linearPart = "(01)01234567890128"; // valid GTIN-14
        string twoDPart = "(21)A12345678";
        string gs1CompositeCode = $"{linearPart}|{twoDPart}";
        string gs1CompositePath = Path.Combine(outputDir, "gs1composite.png");
        BarcodeFactory.Generate(
            symbology: "GS1Composite",
            codeText: gs1CompositeCode,
            outputPath: gs1CompositePath);
        Console.WriteLine($"Generated GS1 Composite: {gs1CompositePath}");
    }
}