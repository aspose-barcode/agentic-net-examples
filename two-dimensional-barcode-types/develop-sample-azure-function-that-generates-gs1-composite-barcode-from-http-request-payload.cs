// Title: Generate GS1 Composite barcode and decode its components
// Description: Demonstrates creating a GS1 Composite barcode, saving it as PNG, and extracting linear and 2‑D component data using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes for producing GS1 Composite symbols, a common requirement in supply‑chain labeling. Developers often need to encode product identifiers (GTIN) together with additional AI data, then verify the barcode by decoding its components. The snippet illustrates typical configuration, image output, and post‑generation validation steps, making it searchable for Azure Function or console‑based implementations.
// Prompt: Develop a sample Azure Function that generates GS1 Composite barcode from HTTP request payload.
// Tags: gs1 composite, barcode generation, barcode recognition, png output, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a GS1 Composite barcode, saves it to a file,
/// and then reads it back to display the linear and 2‑D component information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the composite payload, creates the barcode image,
    /// writes it to a temporary file, and decodes it to show component details.
    /// </summary>
    static void Main(string[] args)
    {
        // Define the linear (GTIN‑14) and 2‑D (AI 10) parts of the GS1 Composite payload.
        string linearComponent = "(01)01234567890128";
        string twoDComponent = "(10)ABC123";
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Determine a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "gs1composite.png");

        // -------------------- Barcode Generation --------------------
        // Create a BarcodeGenerator for GS1 Composite symbology with the prepared payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set visual and encoding parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2;                     // Module width in pixels.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide human‑readable text.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }

        // -------------------- Barcode Decoding --------------------
        // Open the saved image with BarCodeReader configured for GS1 Composite decoding.
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            // Iterate through all detected barcodes (normally one) and output component details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== Decoded GS1 Composite Barcode ===");
                Console.WriteLine($"Linear component type : {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"Linear component text : {result.Extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"2D component type    : {result.Extended.GS1CompositeBar.TwoDType}");
                Console.WriteLine($"2D component text    : {result.Extended.GS1CompositeBar.TwoDCodeText}");
            }
        }
    }
}