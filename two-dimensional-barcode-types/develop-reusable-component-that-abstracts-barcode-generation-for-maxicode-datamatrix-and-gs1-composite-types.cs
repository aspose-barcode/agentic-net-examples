// Title: Reusable Barcode Generation Component for MaxiCode, DataMatrix, and GS1 Composite
// Description: Demonstrates a simple factory that creates barcode images for MaxiCode, DataMatrix, and GS1 Composite symbologies and saves them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce various 1D and 2D barcodes. Typical use cases include inventory labeling, shipping, and product tracking where different symbologies are required. Developers often need a reusable component that abstracts the setup and saving of barcode images across multiple formats.
// Prompt: Develop a reusable component that abstracts barcode generation for MaxiCode, DataMatrix, and GS1 Composite types.
// Tags: barcode symbology, generation, maxicode, datamatrix, gs1 composite, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides static methods to generate barcode images for specific symbologies.
/// </summary>
class BarcodeFactory
{
    /// <summary>
    /// Generates a MaxiCode barcode image and returns the file path.
    /// </summary>
    /// <param name="codeText">The text to encode in the MaxiCode.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> for convenience.</returns>
    public static string GenerateMaxiCode(string codeText, string outputPath)
    {
        // Initialise the generator with MaxiCode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set module size (X dimension) and bar color.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
        return outputPath;
    }

    /// <summary>
    /// Generates a DataMatrix barcode image and returns the file path.
    /// </summary>
    /// <param name="codeText">The text to encode in the DataMatrix.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> for convenience.</returns>
    public static string GenerateDataMatrix(string codeText, string outputPath)
    {
        // Initialise the generator with DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set module size (X dimension).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Choose a specific DataMatrix version (size) that exists.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Set error correction level.
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
        return outputPath;
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image and returns the file path.
    /// </summary>
    /// <param name="linearCodeText">The linear component text (including AI parentheses).</param>
    /// <param name="twoDCodeText">The 2D component text (including AI parentheses).</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> for convenience.</returns>
    public static string GenerateGS1Composite(string linearCodeText, string twoDCodeText, string outputPath)
    {
        // Combine linear and 2D parts with the required separator '|'.
        string combinedCodeText = $"{linearCodeText}|{twoDCodeText}";

        // Initialise the generator with GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Define component types: linear part as GS1-128 and 2D part as CC-A (DataMatrix in this example).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set common parameters for both components.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f; // Height for the linear component.

            // Example: adjust 2D component aspect ratio via its specific parameters (Pdf417 used in example).
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
        return outputPath;
    }
}

/// <summary>
/// Demonstrates usage of the <see cref="BarcodeFactory"/> to create and save barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo application.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodesDemo");
        Directory.CreateDirectory(outputDir);

        // MaxiCode example.
        string maxiPath = Path.Combine(outputDir, "maxicode.png");
        BarcodeFactory.GenerateMaxiCode("Sample MaxiCode", maxiPath);
        Console.WriteLine($"MaxiCode saved to: {maxiPath}");

        // DataMatrix example.
        string dmPath = Path.Combine(outputDir, "datamatrix.png");
        BarcodeFactory.GenerateDataMatrix("DM1234567890", dmPath);
        Console.WriteLine($"DataMatrix saved to: {dmPath}");

        // GS1 Composite example.
        string gs1Path = Path.Combine(outputDir, "gs1composite.png");
        // Linear part must contain AI parentheses, e.g., (01)01234567890123
        string linear = "(01)01234567890123";
        // 2D part also uses AI parentheses, e.g., (21)ABC123
        string twoD = "(21)ABC123";
        BarcodeFactory.GenerateGS1Composite(linear, twoD, gs1Path);
        Console.WriteLine($"GS1 Composite saved to: {gs1Path}");
    }
}