// Title: Validate GS1 Composite barcode against GS1 specification using Aspose.BarCode
// Description: Demonstrates generating a GS1 Composite barcode, saving it as an image, and validating its components with the Aspose.BarCode validation API.
// Category-Description: This example belongs to the Aspose.BarCode generation, recognition, and validation category. It showcases the use of BarcodeGenerator to create a GS1 Composite symbol, BarCodeReader to decode it, and the extended GS1CompositeBar properties to verify compliance with the GS1 specification. Typical use cases include retail product labeling, supply‑chain tracking, and any scenario where combined linear‑and‑2D data must be validated. Developers often need to generate, read, and programmatically validate GS1 barcodes using these core API classes.
// Prompt: Validate generated GS1 Composite barcode against GS1 specification using the library's validation API.
// Tags: gs1 composite, barcode generation, barcode validation, image output, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creation and validation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a GS1 Composite barcode, saves it to a temporary file,
    /// reads it back, and validates that the linear and 2D components match the original data.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // --------------------------------------------------------------------
        // Define the GS1 Composite code text (linear part + 2D part separated by '|')
        // --------------------------------------------------------------------
        string linearPart = "(01)03212345678906"; // valid 14‑digit GTIN with correct check digit
        string twoDPart = "(21)A1B2C3D4E5F6G7H8";   // serial number AI
        string codeText = $"{linearPart}|{twoDPart}";

        // --------------------------------------------------------------------
        // Generate the barcode image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component to GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Set the 2D component to CC-A (MicroPDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

            // Do not throw on minor code‑text issues (e.g., optional AI formatting)
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode to the temporary file
            generator.Save(barcodePath);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read and validate the barcode using BarCodeReader
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            // Enable checksum validation for the linear (1D) component
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool validationPassed = false;

            // Iterate through all detected barcodes (should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // Access GS1 Composite specific extended parameters
                var ext = result.Extended.GS1CompositeBar;
                if (ext != null && !ext.IsEmpty)
                {
                    Console.WriteLine($"Linear component (1D) CodeText: {ext.OneDCodeText}");
                    Console.WriteLine($"2D component CodeText: {ext.TwoDCodeText}");

                    // Simple validation: compare with the original components
                    if (ext.OneDCodeText == linearPart && ext.TwoDCodeText == twoDPart)
                    {
                        Console.WriteLine("Validation succeeded: components match original data.");
                        validationPassed = true;
                    }
                    else
                    {
                        Console.WriteLine("Validation failed: component data does not match.");
                    }
                }
                else
                {
                    Console.WriteLine("Extended GS1 Composite data not available.");
                }
            }

            if (!validationPassed)
            {
                Console.WriteLine("Overall validation failed.");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional, best‑effort)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }
}