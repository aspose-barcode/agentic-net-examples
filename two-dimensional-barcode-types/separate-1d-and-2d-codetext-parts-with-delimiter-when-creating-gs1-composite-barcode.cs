// Title: GS1 Composite Barcode Generation with Separate 1D and 2D CodeText Parts
// Description: Demonstrates how to create a GS1 Composite barcode by separating the linear (1D) and 2D components using the ‘|’ delimiter in the CodeText, and how to read back each component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator with EncodeTypes.GS1CompositeBar, configuration of linear and 2D component types, and BarCodeReader with DecodeType.GS1CompositeBar to extract individual component data. Developers working with retail, logistics, or healthcare often need to encode product identifiers in a composite format and later parse each part separately.
// Prompt: Separate 1D and 2D CodeText parts with ‘|’ delimiter when creating a GS1 Composite barcode.
// Tags: gs1 composite barcode, 1d 2d components, code text delimiter, aspose.barcode, barcode generation, barcode recognition, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Composite barcode with distinct 1D and 2D parts,
/// saves it as an image, and then reads back the individual components.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output folder where the barcode image will be saved.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo");
        Directory.CreateDirectory(outputFolder);
        string barcodePath = Path.Combine(outputFolder, "gs1composite.png");

        // --------------------------------------------------------------------
        // Define the CodeText using the '|' delimiter:
        //   - Left side contains the linear (1D) component.
        //   - Right side contains the 2D component.
        // --------------------------------------------------------------------
        string codeText = "(01)12345678901231|(10)ABCD0123";

        // --------------------------------------------------------------------
        // Generate the GS1 Composite barcode with specific component settings.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the module size (X-dimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text because we are focusing on component data.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Specify the linear (1D) component type.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Specify the 2D component type (Composite Component A).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Allow non‑GS1 encoding for demonstration purposes.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // --------------------------------------------------------------------
        // Read the saved barcode and display the separated component information.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Full CodeText: {result.CodeText}");
                Console.WriteLine($"1D Component Type: {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"1D CodeText: {result.Extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"2D Component Type: {result.Extended.GS1CompositeBar.TwoDType}");
                Console.WriteLine($"2D CodeText: {result.Extended.GS1CompositeBar.TwoDCodeText}");
            }
        }
    }
}