// Title: Create and Decode a GS1 Composite Barcode with Separate 1D and 2D Parts
// Description: Demonstrates how to generate a GS1 Composite barcode by separating the linear (1D) and two‑dimensional (2D) components with a ‘|’ delimiter, then reads back the barcode to retrieve each part.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for GS1 Composite barcodes and BarCodeReader for decoding. Developers working with product identification, inventory, or logistics often need to create GS1 Composite symbols that combine a linear component (e.g., GS1‑128) with a 2D component (e.g., QR Code). The key API classes demonstrated are BarcodeGenerator, BarCodeReader, EncodeTypes, TwoDComponentType, and DecodeType, which are commonly used for creating, customizing, and interpreting composite barcodes.
// Prompt: Separate 1D and 2D CodeText parts with ‘|’ delimiter when creating a GS1 Composite barcode.
// Tags: gs1 composite, barcode generation, barcode recognition, c#, aspose.barcode, encode types, decode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a GS1 Composite barcode with distinct 1D and 2D components,
/// saves it as an image, and then reads back the barcode to display each component separately.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and decodes it.
    /// </summary>
    static void Main()
    {
        // Prepare the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite.png");

        // Define the linear (1D) component – GTIN‑14 with a valid check digit.
        string linearComponent = "(01)01234567890128";

        // Define the two‑dimensional (2D) component – includes a lot number and a custom data field.
        string twoDComponent = "(10)ABCD0123(240)0123456789";

        // Combine the components using the '|' delimiter required for GS1 Composite barcodes.
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Generate the GS1 Composite barcode using the specified encoding types.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set visual parameters: X‑dimension and hide the human‑readable text.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Specify the types for the linear and 2D components.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Allow non‑GS1 encoding if needed (set to false to enforce strict GS1 rules).
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Read back the saved barcode and output the separated 1D and 2D components.
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Decoded 1D Component: " + result.Extended.GS1CompositeBar.OneDCodeText);
                Console.WriteLine("Decoded 2D Component: " + result.Extended.GS1CompositeBar.TwoDCodeText);
                Console.WriteLine("1D Component Type: " + result.Extended.GS1CompositeBar.OneDType);
                Console.WriteLine("2D Component Type: " + result.Extended.GS1CompositeBar.TwoDType);
            }
        }
    }
}