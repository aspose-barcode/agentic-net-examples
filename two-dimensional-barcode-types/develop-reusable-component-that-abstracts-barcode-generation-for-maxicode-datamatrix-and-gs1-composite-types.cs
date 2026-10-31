// Title: Reusable Barcode Generation Component for MaxiCode, DataMatrix, and GS1 Composite
// Description: Demonstrates a simple factory that creates PNG images for MaxiCode, DataMatrix, and GS1 Composite barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with different EncodeTypes. Typical use cases include creating product labels, shipping codes, and composite barcodes for retail. Developers often need to generate multiple symbologies programmatically, configure parameters such as version or component types, and save the output in common image formats.
// Prompt: Develop a reusable component that abstracts barcode generation for MaxiCode, DataMatrix, and GS1 Composite types.
// Tags: barcode, generation, maximicode, datamatrix, gs1 composite, aspose.barcode, png, factory pattern

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides static methods to generate different barcode types (MaxiCode, DataMatrix, GS1 Composite) as PNG files.
/// </summary>
class BarcodeFactory
{
    /// <summary>
    /// Generates a MaxiCode barcode image.
    /// </summary>
    /// <param name="codeText">The text to encode in the MaxiCode.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    public static void GenerateMaxiCode(string codeText, string outputPath)
    {
        // Create a generator for MaxiCode with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Optional: set mode or other parameters if needed.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Generates a DataMatrix barcode image.
    /// </summary>
    /// <param name="codeText">The text to encode in the DataMatrix.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    public static void GenerateDataMatrix(string codeText, string outputPath)
    {
        // Create a generator for DataMatrix with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Example version; adjust as needed.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image that combines a linear component and a 2‑D component.
    /// </summary>
    /// <param name="linearComponent">The linear component string (e.g., GS1 Code128).</param>
    /// <param name="twoDComponent">The 2‑D component string (e.g., PDF417).</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    public static void GenerateGS1Composite(string linearComponent, string twoDComponent, string outputPath)
    {
        // Combine linear and 2‑D components using the pipe separator required by Aspose.
        string combined = $"{linearComponent}|{twoDComponent}";
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combined))
        {
            // Specify the linear component type (GS1 Code128 in this example).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Specify the 2‑D component type (CC_C uses full PDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            // Allow non‑GS1 data in the 2‑D component if needed.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            // Configure PDF417 columns for the 2‑D part.
            generator.Parameters.Barcode.Pdf417.Columns = 30;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}

/// <summary>
/// Demonstrates usage of the <see cref="BarcodeFactory"/> to generate various barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example application.
    /// </summary>
    static void Main()
    {
        // Determine the base directory for output files.
        string baseDir = Directory.GetCurrentDirectory();

        // ---------- MaxiCode example ----------
        string maxiCodeText = "Sample MaxiCode Text";
        string maxiCodePath = Path.Combine(baseDir, "maxicode.png");
        BarcodeFactory.GenerateMaxiCode(maxiCodeText, maxiCodePath);
        Console.WriteLine($"MaxiCode saved to {maxiCodePath}");

        // ---------- DataMatrix example ----------
        string dataMatrixText = "DM1234567890";
        string dataMatrixPath = Path.Combine(baseDir, "datamatrix.png");
        BarcodeFactory.GenerateDataMatrix(dataMatrixText, dataMatrixPath);
        Console.WriteLine($"DataMatrix saved to {dataMatrixPath}");

        // ---------- GS1 Composite example ----------
        // Valid GTIN‑14 values (01) with correct check digit.
        string linear = "(01)01234567890128"; // linear component
        string twoD = "(01)00123456789012";   // 2D component
        string gs1CompositePath = Path.Combine(baseDir, "gs1composite.png");
        BarcodeFactory.GenerateGS1Composite(linear, twoD, gs1CompositePath);
        Console.WriteLine($"GS1 Composite saved to {gs1CompositePath}");
    }
}