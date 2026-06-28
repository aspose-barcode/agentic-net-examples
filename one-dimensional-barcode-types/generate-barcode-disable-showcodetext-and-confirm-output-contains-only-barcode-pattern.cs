using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode without human‑readable text,
/// saving it to a temporary file, and then reading it back to verify correctness.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and validates the saved image by decoding it.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and the output file path.
        string codeText = "1234567890";
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // ------------------------------------------------------------
        // Generate the barcode image with human‑readable text disabled.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Hide the code text (human‑readable) from the generated image.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image to the specified path.
            generator.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image file was created successfully.
        // ------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the barcode from the saved image to ensure it is valid.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the generated image.");
            }
            else
            {
                // Output each decoded barcode's text.
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }

                // Confirm successful generation without human‑readable text.
                Console.WriteLine("Barcode generated successfully with no human‑readable text.");
            }
        }
    }
}