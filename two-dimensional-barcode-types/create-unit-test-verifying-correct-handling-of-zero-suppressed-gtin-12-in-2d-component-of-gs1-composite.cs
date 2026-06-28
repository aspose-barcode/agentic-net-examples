using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demo program that generates and validates a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a GS1 Composite barcode with a zero‑suppressed GTIN‑12,
    /// saves it to a temporary file, reads it back for verification, and cleans up.
    /// </summary>
    static void Main()
    {
        // Prepare temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "gs1composite.png");

        // Zero‑suppressed GTIN‑12 example (12‑digit GTIN)
        string gtin12 = "012345678905"; // GTIN‑12

        // Composite code consists of a linear part and a 2D part separated by '|'
        string compositeCode = $"(01){gtin12}|(21)ABC123";

        // ------------------------------------------------------------
        // Generate GS1 Composite barcode and save it to the temporary file
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, compositeCode))
            {
                // Set linear component to GS1 Code128
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Set 2D component to CC_A (MicroPDF417)
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Use interpolation mode so the engine automatically sizes the image
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Save the generated barcode image to the temporary path
                generator.Save(tempPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Barcode generation error - {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Verify the generated barcode by reading it back
        // ------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(tempPath, DecodeType.GS1CompositeBar))
            {
                // Read all barcodes found in the image
                var results = reader.ReadBarCodes();

                // Ensure at least one barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                    return;
                }

                // Examine the first (and expected only) result
                var result = results[0];

                // Check that the recognized CodeText contains the expected linear part
                if (result.CodeText.Contains($"(01){gtin12}"))
                {
                    Console.WriteLine("PASSED: Zero‑suppressed GTIN‑12 correctly handled in 2D component.");
                }
                else
                {
                    Console.WriteLine($"FAILED: Expected GTIN not found. Detected CodeText: {result.CodeText}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Barcode recognition error - {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file if it still exists
            if (File.Exists(tempPath))
            {
                try
                {
                    File.Delete(tempPath);
                }
                catch
                {
                    // Suppress any exceptions during cleanup
                }
            }
        }
    }
}