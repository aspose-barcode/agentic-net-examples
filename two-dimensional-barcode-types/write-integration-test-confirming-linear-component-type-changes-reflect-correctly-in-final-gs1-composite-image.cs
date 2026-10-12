// Title: GS1 Composite barcode generation with variable linear component types
// Description: Demonstrates creating GS1 Composite barcodes using different linear symbologies and verifies the linear component type via barcode reading.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator, BarCodeReader, EncodeTypes, and related parameters to produce composite images and validate their components. Developers working with GS1 standards often need to switch linear symbologies (e.g., EAN13, GS1‑Code128) while keeping the 2‑D component constant, and this sample illustrates how to configure and test such scenarios.
// Prompt: Write integration test confirming linear component type changes reflect correctly in the final GS1 Composite image.
// Tags: gs1, composite, barcode, generation, verification, encode types, linear component, two-dimensional component, aspnet, barcodereader, barcodgenerator

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating GS1 Composite barcodes with different linear component types
/// and verifying the generated image using Aspose.BarCode reader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates temporary barcode images, reads them back,
    /// and outputs detected linear component information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define test cases: linear type name, linear code, and 2‑D component code
        var testCases = new (string LinearTypeName, string LinearCode, string TwoDCode)[]
        {
            ("EAN13", "2001234567893", "(10)ABCD0123"),
            ("GS1Code128", "(01)01234567890128", "(10)ABCD0123")
        };

        // Iterate over each test case
        foreach (var (typeName, linearCode, twoDCode) in testCases)
        {
            // Resolve the EncodeTypes field that matches the linear component name via reflection
            FieldInfo field = typeof(EncodeTypes).GetField(typeName);
            if (field == null)
            {
                Console.WriteLine($"Unknown linear component type: {typeName}");
                continue;
            }
            BaseEncodeType linearEncodeType = (BaseEncodeType)field.GetValue(null);

            // Build the composite code text (linear|2D)
            string codeText = $"{linearCode}|{twoDCode}";
            string filePath = Path.Combine(tempDir, $"{typeName}.png");

            // Generate the GS1 Composite barcode image
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncodeType;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the image file was created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode image: {filePath}");
                continue;
            }

            // Read the generated barcode and output detected linear component details
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"Detected Linear Type: {result.Extended.GS1CompositeBar.OneDType}");
                    Console.WriteLine($"Detected Linear CodeText: {result.Extended.GS1CompositeBar.OneDCodeText}");
                }
            }
        }

        // Attempt to clean up the temporary directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}