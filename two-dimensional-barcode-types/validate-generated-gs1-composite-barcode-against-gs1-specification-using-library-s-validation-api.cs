// Title: Validate GS1 Composite Barcode Using Aspose.BarCode
// Description: Demonstrates generating a GS1 Composite barcode, saving it as an image, and validating its components against the GS1 specification using Aspose.BarCode's validation API.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category, showcasing how to work with GS1 Composite symbology. It uses BarcodeGenerator for creation, BarCodeReader for recognition, and the GS1CompositeBar extended parameters for detailed validation. Developers often need to generate GS1 barcodes for supply‑chain labeling and verify that both linear and 2D components conform to GS1 standards.
// Prompt: Validate generated GS1 Composite barcode against GS1 specification using the library's validation API.
// Tags: gs1 composite, barcode generation, barcode validation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a GS1 Composite barcode, saves it to a PNG file, and validates the
/// linear and 2D components against the original data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and validation.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Composite barcode text.
        // Linear part: (01)03212345678906
        // 2D part: (21)A1B2C3D4E5F6G7H8
        // Parts are separated by '|'
        string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Output file path for the generated barcode image.
        string outputPath = "gs1composite.png";

        // Generate the GS1 Composite barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set the linear component to GS1 Code 128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component to CC-A (Composite Component A).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust the aspect ratio for the 2D component.
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

            // Set X-dimension (module width) for both components.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Set the height of the linear component.
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Read and validate the generated barcode.
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            // Enable checksum validation (recommended for GS1 barcodes).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool validationPassed = false;

            // Iterate through all recognized barcodes (should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the raw CodeText returned by the reader.
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");

                // Access GS1 Composite specific extended parameters.
                var gs1Ext = result.Extended.GS1CompositeBar;
                if (gs1Ext != null && !gs1Ext.IsEmpty)
                {
                    // Display component types and their respective CodeTexts.
                    Console.WriteLine($"Linear Component Type: {gs1Ext.OneDType}");
                    Console.WriteLine($"Linear Component CodeText: {gs1Ext.OneDCodeText}");
                    Console.WriteLine($"2D Component Type: {gs1Ext.TwoDType}");
                    Console.WriteLine($"2D Component CodeText: {gs1Ext.TwoDCodeText}");

                    // Expected component texts for validation.
                    string expectedLinear = "(01)03212345678906";
                    string expectedTwoD = "(21)A1B2C3D4E5F6G7H8";

                    // Compare recognized component texts with expected values.
                    if (gs1Ext.OneDCodeText == expectedLinear && gs1Ext.TwoDCodeText == expectedTwoD)
                    {
                        validationPassed = true;
                        Console.WriteLine("GS1 Composite barcode validation succeeded.");
                    }
                    else
                    {
                        Console.WriteLine("GS1 Composite barcode validation failed: component texts do not match expected values.");
                    }
                }
                else
                {
                    Console.WriteLine("GS1 Composite extended parameters are missing or empty.");
                }
            }

            // Report overall validation result if no successful match was found.
            if (!validationPassed)
            {
                Console.WriteLine("GS1 Composite barcode validation did not pass.");
            }
        }
    }
}