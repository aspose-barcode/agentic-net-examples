using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of Code16K barcodes with aspect ratio validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes with valid and invalid aspect ratios.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the Code16K barcode
        string codeText = "1234567890123456789012345678901234567890";

        // Attempt generation with an invalid aspect ratio (below the minimum of 8)
        GenerateCode16K(5f, codeText, "code16k_invalid.png");

        // Attempt generation with a valid aspect ratio (8 or above)
        GenerateCode16K(10f, codeText, "code16k_valid.png");
    }

    /// <summary>
    /// Generates a Code16K barcode if the aspect ratio meets the minimum requirement.
    /// Logs descriptive messages for invalid ratios or any generation errors.
    /// </summary>
    /// <param name="aspectRatio">Desired aspect ratio for the barcode.</param>
    /// <param name="codeText">Text to encode.</param>
    /// <param name="outputPath">File path to save the generated image.</param>
    static void GenerateCode16K(float aspectRatio, string codeText, string outputPath)
    {
        // Minimum allowed aspect ratio for Code16K is 8
        if (aspectRatio < 8f)
        {
            // Inform the user that the provided ratio is insufficient and skip generation
            Console.WriteLine($"[Warning] Aspect ratio {aspectRatio} is below the minimum of 8 for Code16K. Generation skipped.");
            return;
        }

        try
        {
            // Initialize the barcode generator for Code16K with the provided text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Apply the validated aspect ratio to the generator's parameters
                generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

                // Ensure the output directory exists before saving the image
                string directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Save the generated barcode image (default format is PNG)
                generator.Save(outputPath);
                Console.WriteLine($"[Info] Code16K barcode generated successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that occur during barcode generation
            Console.WriteLine($"[Error] Failed to generate Code16K barcode (AspectRatio={aspectRatio}). Exception: {ex.Message}");
        }
    }
}