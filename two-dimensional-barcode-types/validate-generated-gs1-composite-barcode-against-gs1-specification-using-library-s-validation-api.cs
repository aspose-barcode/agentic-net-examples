// Title: Validate GS1 Composite barcode generation and validation
// Description: Demonstrates creating a GS1 Composite barcode, saving it as PNG, reading it back, and verifying its components against the original data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite symbology. It shows how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2‑D component types, save the image, then employ BarCodeReader with DecodeType.GS1CompositeBar to extract and validate the OneD and TwoD parts. Developers working with GS1 standards often need to generate composite barcodes and ensure the encoded data complies with the specification, making this pattern useful for inventory, logistics, and retail applications.
// Prompt: Validate generated GS1 Composite barcode against GS1 specification using the library's validation API.
// Tags: gs1 composite barcode, generation, recognition, validation, aspnet.barcode, encode types, decode type, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation, saving, reading, and validation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a GS1 Composite barcode, reads it back, and validates its components.
    /// </summary>
    static void Main()
    {
        // Define temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "gs1Composite.png");

        // Prepare linear and 2‑D component data according to GS1 syntax
        string linearComponent = "(01)98898765432106";
        string twoDComponent = "(10)ABCD0123";

        // Combine components using the GS1 Composite separator '|'
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Generate the GS1 Composite barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure GS1 Composite specific settings
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode image to the temporary path
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Read the saved barcode image and decode the GS1 Composite components
        using (var reader = new BarCodeReader(tempPath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Read GS1 Composite barcode:");
                Console.WriteLine($"OneD Type: {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"OneD CodeText: {result.Extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"TwoD Type: {result.Extended.GS1CompositeBar.TwoDType}");
                Console.WriteLine($"TwoD CodeText: {result.Extended.GS1CompositeBar.TwoDCodeText}");

                // Validate that the decoded components match the original input
                bool isValid = result.Extended.GS1CompositeBar.OneDCodeText == linearComponent &&
                               result.Extended.GS1CompositeBar.TwoDCodeText == twoDComponent;

                Console.WriteLine($"Validation result: {(isValid ? "Valid" : "Invalid")}");
            }
        }

        // Clean up the temporary barcode image file
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }
    }
}