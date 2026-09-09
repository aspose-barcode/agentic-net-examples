// Title: Validate GS1 Composite barcode generation and verification
// Description: Demonstrates generating a GS1 Composite barcode, saving it as PNG, and validating the encoded linear and 2D components using Aspose.BarCode's recognition and validation API.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite symbology. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and related parameter objects for configuring GS1 Composite barcodes. Developers commonly use these APIs to create compliant GS1 barcodes for product identification and to verify that generated codes meet GS1 specifications.
// Prompt: Validate generated GS1 Composite barcode against GS1 specification using the library's validation API.
// Tags: gs1, composite, barcode, generation, validation, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and validation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a GS1 Composite barcode, saves it, reads it back, and validates the components.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // Define linear and 2D components (both must contain a valid GS1 AI (01) with 14 digits)
        string linearComponent = "(01)12345678901231";
        string twoDComponent = "(01)00123456789012";
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Generate the GS1 Composite barcode with specific parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set X-dimension to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text (not needed for validation)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2D component type and the linear component symbology
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Allow non‑GS1 encoding for flexibility (set to false to enforce strict GS1)
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read and validate the generated barcode using the GS1 Composite decoder
        using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            bool validationPassed = false;

            // Iterate through all recognized barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Extract extended GS1 Composite information (linear and 2D components)
                var ext = result.Extended.GS1CompositeBar;
                string readLinear = ext.OneDCodeText;
                string readTwoD = ext.TwoDCodeText;

                // Simple validation: compare the read components with the original values
                if (readLinear == linearComponent && readTwoD == twoDComponent)
                {
                    validationPassed = true;
                    Console.WriteLine("Validation succeeded:");
                    Console.WriteLine($"Linear component: {readLinear}");
                    Console.WriteLine($"2D component: {readTwoD}");
                }
                else
                {
                    Console.WriteLine("Validation failed:");
                    Console.WriteLine($"Expected linear: {linearComponent}, read: {readLinear}");
                    Console.WriteLine($"Expected 2D: {twoDComponent}, read: {readTwoD}");
                }
            }

            if (!validationPassed)
            {
                Console.WriteLine("No valid GS1 Composite barcode was recognized.");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}