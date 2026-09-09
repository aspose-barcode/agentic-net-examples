// Title: Reusable barcode generator for MaxiCode, DataMatrix, and GS1 Composite
// Description: Demonstrates a simple factory that creates PNG images for three common barcode symbologies using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes such as MaxiCode, DataMatrix, and GS1CompositeBar. It highlights key API classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers frequently use to produce barcode images for labeling, packaging, and inventory systems.
// Prompt: Develop a reusable component that abstracts barcode generation for MaxiCode, DataMatrix, and GS1 Composite types.
// Tags: barcode, generation, maximcode, datamatrix, gs1 composite, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides static methods to generate different barcode types using Aspose.BarCode.
/// </summary>
class BarcodeFactory
{
    /// <summary>
    /// Generates a MaxiCode barcode image in PNG format.
    /// </summary>
    /// <param name="codeText">The text to encode in the MaxiCode.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    public static void GenerateMaxiCode(string codeText, string outputPath)
    {
        // Create a generator for MaxiCode with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Optional: configure mode or other parameters here if needed.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Generates a DataMatrix barcode image in PNG format.
    /// </summary>
    /// <param name="codeText">The text to encode in the DataMatrix.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    public static void GenerateDataMatrix(string codeText, string outputPath)
    {
        // Create a generator for DataMatrix with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optional: set version or error correction level here.
            // generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Generates a GS1 Composite barcode (linear + 2D component) image in PNG format.
    /// </summary>
    /// <param name="linearPart">The linear (1D) component text, typically GS1 Code128.</param>
    /// <param name="twoDPart">The 2D component text, typically PDF417 data.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    public static void GenerateGS1Composite(string linearPart, string twoDPart, string outputPath)
    {
        // Combine linear and 2D parts using the GS1 separator.
        string combinedText = $"{linearPart}|{twoDPart}";

        // Create a generator for GS1 Composite Bar with the combined text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedText))
        {
            // Configure the linear component to use GS1 Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use CC_C (PDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Set PDF417 column count for the 2D component.
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Allow non‑GS1 data in the 2D component if required.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}

/// <summary>
/// Demonstrates usage of the BarcodeFactory to create sample barcode images.
/// </summary>
class Program
{
    static void Main()
    {
        // Create a temporary directory for the generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodesDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define output file paths for each barcode type.
        string maxiPath = Path.Combine(tempDir, "maxicode.png");
        string dmPath = Path.Combine(tempDir, "datamatrix.png");
        string gs1Path = Path.Combine(tempDir, "gs1composite.png");

        // Generate MaxiCode and report its location.
        BarcodeFactory.GenerateMaxiCode("Sample MaxiCode", maxiPath);
        Console.WriteLine($"MaxiCode saved to: {maxiPath}");

        // Generate DataMatrix and report its location.
        BarcodeFactory.GenerateDataMatrix("Sample DataMatrix", dmPath);
        Console.WriteLine($"DataMatrix saved to: {dmPath}");

        // Prepare linear and 2D components for the GS1 Composite barcode.
        string linear = "(01)12345678901231"; // 1D component (GS1 Code128)
        string twoD = "(21)ABC123";          // 2D component (PDF417)

        // Generate GS1 Composite and report its location.
        BarcodeFactory.GenerateGS1Composite(linear, twoD, gs1Path);
        Console.WriteLine($"GS1 Composite saved to: {gs1Path}");
    }
}