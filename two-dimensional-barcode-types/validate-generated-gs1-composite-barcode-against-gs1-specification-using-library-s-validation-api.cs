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
    /// Entry point of the application.
    /// Generates a GS1 Composite barcode, saves it as an image, and then validates it by reading the image.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite barcode data.
        // Linear part: (01)03212345678906  (GTIN)
        // 2D part: (21)A1B2C3D4E5F6G7H8 (Serial)
        // Parts are separated by '|'
        string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Output image path for the generated barcode.
        string imagePath = "gs1composite.png";

        // -------------------------------------------------
        // Generate the GS1 Composite barcode and save it.
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure the linear component to use GS1-128 encoding.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use CC-A (Composite Component A) type.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Enforce GS1 encoding rules for the 2D component.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = true;

            // Set size parameters: X-dimension and bar height in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3;
            generator.Parameters.Barcode.BarHeight.Pixels = 100;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify that the barcode image was successfully created.
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // -------------------------------------------------
        // Recognize and validate the generated barcode.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            // Enable checksum validation (required for GS1 where applicable).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Disallow recognition of barcodes that are identified as incorrect.
            reader.QualitySettings.AllowIncorrectBarcodes = false;

            // Read all barcodes from the image.
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, report and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode result.
            foreach (var result in results)
            {
                // Check for GS1 Composite specific extended parameters.
                if (result.Extended?.GS1CompositeBar != null)
                {
                    Console.WriteLine("GS1 Composite barcode validation succeeded.");
                    Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                }
                else
                {
                    Console.WriteLine("GS1 Composite barcode validation failed: extended GS1 data not available.");
                }
            }
        }
    }
}