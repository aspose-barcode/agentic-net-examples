// Title: Encode AI 11 (Production Date) in GS1 Composite Barcode (2D Component)
// Description: Demonstrates how to encode the GS1 Application Identifier 11 (production date) in YYMMDD format within the 2D component of a GS1 Composite barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It shows how to configure the linear and 2‑dimensional components, set X‑dimension, and control code‑text placement using the BarcodeGenerator and its Parameters API. Developers creating GS1‑compliant labels often need to embed AI data such as GTIN and production dates in composite barcodes for retail and logistics.
// Prompt: Encode AI 11 (production date) in the 2D component of a GS1 Composite barcode using YYMMDD format.
// Tags: gs1 composite, encode, png, aspose.barcode, barcodegenerator, ai11

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Composite barcode that includes AI 11 (production date) in the 2D component
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1CompositeAI11.png");
        string directory = Path.GetDirectoryName(outputPath);

        // Ensure the target directory exists.
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Example GTIN (AI 01) and AI 11 (production date) in YYMMDD format.
        // The pipe character separates the linear and 2D components of the composite barcode.
        string codeText = "(01)01234567890123|(11)230915";

        // Initialize the barcode generator for GS1 Composite symbology with the prepared code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Hide the human‑readable text because the composite barcode already encodes the data.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2D component to be a CC‑A (Composite Component A) type.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set the linear component to use GS1‑Code128 encoding.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}