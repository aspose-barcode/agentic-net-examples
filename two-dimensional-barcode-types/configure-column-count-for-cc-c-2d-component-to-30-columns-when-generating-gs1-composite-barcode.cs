// Title: Generate GS1 Composite barcode with CC_C component and 30 columns
// Description: Demonstrates how to create a GS1 Composite barcode where the 2‑D component is of type CC_C and its column count is set to 30. The resulting image is saved as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating configuration of composite barcodes. It shows usage of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters such as TwoDComponentType, LinearComponentType, and Pdf417 column settings. Developers working with GS1 standards and composite symbologies can refer to this snippet for setting up 2‑D component options and customizing output.
// Prompt: Configure column count for CC_C 2D component to 30 columns when generating a GS1 Composite barcode.
// Tags: gs1 composite barcode, cc_c, pdf417 columns, aspose.barcode, c#, barcode generation, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Composite barcode with a CC_C 2‑D component and 30 PDF417 columns.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "GS1Composite_CC_C_30cols.png");

        // Barcode data string containing GS1 Application Identifiers and a human‑readable part
        string codeText = "(01)98898765432106(3202)012345|HelloWorld";

        // Initialize the generator for a GS1 Composite barcode using the provided data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the module (X) dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the linear component's human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2‑D component to be of type CC_C
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Set the linear component to use GS1‑Code128 encoding
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the number of columns for the PDF417 (2‑D) component to 30
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Allow non‑GS1 encoding in the linear component if needed
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}