// Title: Validate UseMinimalXDimension with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode and reading it while both UseMinimalXDimension and AllowIncorrectBarcodes settings are enabled, ensuring no conflicts arise.
// Prompt: Validate that setting both UseMinimalXDimension and AllowIncorrectBarcodes together does not cause conflicts.
// Tags: barcode, code128, generation, recognition, minimalxdimension, allowincorrectbarcodes, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and reading with specific quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point: generates a Code128 barcode, saves it, and reads it using minimal X dimension and allowing incorrect barcodes.
    /// </summary>
    static void Main()
    {
        const string imagePath = "code128.png";

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the barcode with both UseMinimalXDimension and AllowIncorrectBarcodes enabled
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure recognition quality settings
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f; // minimal element size in pixels
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            try
            {
                // Perform barcode reading
                var results = reader.ReadBarCodes();

                // Process and display results
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Type: {result.CodeType}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                    }
                }

                Console.WriteLine("Reading completed without conflicts.");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during reading
                Console.WriteLine($"An error occurred during barcode reading: {ex.Message}");
            }
        }
    }
}