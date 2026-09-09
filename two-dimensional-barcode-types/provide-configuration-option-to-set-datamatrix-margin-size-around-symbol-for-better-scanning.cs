// Title: Generate DataMatrix barcode with custom margin (padding)
// Description: Demonstrates how to configure the margin around a DataMatrix barcode using Aspose.BarCode to improve scanning reliability.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as Padding and XDimension. Typical scenarios include creating high‑quality barcodes for packaging, inventory, and document automation where precise control over symbol margins is required. Developers often need to adjust these settings to meet scanner specifications and ensure optimal readability.
// Prompt: Provide configuration option to set DataMatrix margin size around the symbol for better scanning.
// Tags: datamatrix, barcode, margin, padding, generation, aspnet, aspose.barcode, png, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with configurable margins (padding) to enhance scanability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary directory, configures barcode margins, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system's temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "DataMatrixWithMargin.png");

        // Initialize the barcode generator for DataMatrix with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello Aspose"))
        {
            // Configure uniform margin (padding) of 20 points on all sides of the symbol
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Optional: set the module (dot) size to 2 points for better visual clarity
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}