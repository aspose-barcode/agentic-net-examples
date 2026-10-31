// Title: Configure PDF417 column count for GS1 Composite barcode (CC_C)
// Description: Demonstrates how to set the CC_C 2D component to use 30 columns when generating a GS1 Composite barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite symbology. It shows usage of BarcodeGenerator, EncodeTypes, and PDF417 parameters to customize composite barcodes. Developers often need to adjust 2D component settings such as column count, X‑dimension, and human‑readable text for specific scanning requirements.
// Prompt: Configure column count for CC_C 2D component to 30 columns when generating a GS1 Composite barcode.
// Tags: gs1 composite, column count, pdf417, png, barcodegenerator, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Composite barcode with a CC_C 2D component configured to 30 PDF417 columns and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite_CC_C_30Columns.png");

        // Build the composite code text:
        //   - Linear part (GS1 Code128) containing a valid GTIN‑14
        //   - 2D part containing arbitrary data
        string linearPart = "(01)01234567890128"; // valid GTIN‑14
        string twoDPart = "Aspose.Barcode";
        string codeText = $"{linearPart}|{twoDPart}";

        // Initialize the barcode generator for GS1 Composite symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the 2D component type to CC_C (PDF417 based)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Set the linear component type to GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Allow non‑GS1 data in the 2D component (optional)
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Configure PDF417 to use 30 columns for the CC_C component
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Optional: increase X‑dimension for better visual size
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide human‑readable text to keep the image clean
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}