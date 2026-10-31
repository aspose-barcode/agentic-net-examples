// Title: Save GS1 Composite barcode as JPEG with quality level 90
// Description: Demonstrates generating a GS1 Composite barcode and saving it as a JPEG image suitable for web display.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to create GS1 Composite (linear + 2‑D) barcodes using the BarcodeGenerator class. It shows setting basic visual parameters, configuring the composite components, and exporting the result to a JPEG file. Developers working with product identification, inventory, or retail systems often need to generate GS1‑128 and GS1‑DataMatrix composites for scanning and online presentation.
// Prompt: Save GS1 Composite barcode image as JPEG with quality level 90 for web display.
// Tags: gs1 composite barcode, jpeg output, image quality, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a GS1 Composite barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its components,
    /// and writes the image to a temporary JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite.jpg");

        // Ensure the target directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Prepare the GS1 Composite code text.
        // Linear part (GS1‑128) contains a GTIN‑14 (AI (01)).
        string linearPart = "(01)01234567890128";
        // 2D part (e.g., CC‑C) contains arbitrary data.
        string twoDPart = "HelloWorld";
        // Combine parts with the '|' separator required for composite barcodes.
        string codeText = $"{linearPart}|{twoDPart}";

        // Create a barcode generator for the GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set basic visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the composite components.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Note: Aspose.BarCode does not expose a JPEG quality setting.
            // The image will be saved with default JPEG compression.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"GS1 Composite barcode saved as JPEG to: {outputPath}");
    }
}