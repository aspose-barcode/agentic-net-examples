using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary PNG file,
/// reading it back, and displaying detection results with confidence information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, outputs details, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Create a temporary PNG file path for the barcode image.
        // ------------------------------------------------------------
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to the temporary file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set a moderate resolution (150 DPI) which may affect confidence.
            generator.Parameters.Resolution = 150f;

            // Save the generated barcode image to the specified path.
            generator.Save(tempImagePath);
        }

        // ------------------------------------------------------------
        // Verify that the image file was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Error: Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image using all supported decode types.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            bool anyResult = false;

            // Iterate through all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;

                // Output barcode type, decoded text, and confidence level.
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");

                // Warn if the confidence level is only moderate.
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Barcode confidence is moderate. Consider enhancing the image (e.g., increase resolution, improve contrast).");
                }
            }

            // If no barcodes were detected, inform the user.
            if (!anyResult)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}