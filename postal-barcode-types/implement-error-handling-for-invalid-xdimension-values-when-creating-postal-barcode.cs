// Title: Postal Barcode Generation with XDimension Validation
// Description: Demonstrates creating a Postnet postal barcode, saving it as PNG, and handling invalid XDimension values.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies such as Postnet. It shows how to use BarcodeGenerator, set barcode parameters like XDimension, and handle argument errors. Developers working with postal barcodes often need to validate dimensions and capture exceptions during image creation.
// Prompt: Implement error handling for invalid XDimension values when creating a postal barcode.
// Tags: barcode, postal, xdimension, validation, aspnet, aspose.barcode, generation, png, exception handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating postal barcodes with XDimension validation and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates a valid and an invalid barcode,
    /// and saves the resulting images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string tempDir = Path.Combine(Path.GetTempPath(), "PostalBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // ----- Valid barcode example -----
        string validPath = Path.Combine(tempDir, "Postnet_Valid.png");
        CreatePostalBarcode("Postnet", "1159628792", 3f, validPath);

        // ----- Invalid XDimension example (should trigger error handling) -----
        string invalidPath = Path.Combine(tempDir, "Postnet_Invalid.png");
        CreatePostalBarcode("Postnet", "1159628792", 0f, invalidPath);
    }

    /// <summary>
    /// Generates a postal barcode of the specified symbology, validates the XDimension,
    /// and saves the image to the given path. Errors are caught and reported to the console.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Postnet").</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="xDimension">Desired XDimension in points; must be greater than zero.</param>
    /// <param name="outputPath">File path where the generated PNG image will be saved.</param>
    static void CreatePostalBarcode(string symbologyName, string codeText, float xDimension, string outputPath)
    {
        // Resolve the symbology name to the corresponding EncodeTypes enum value
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        try
        {
            // Initialize the barcode generator with the resolved type and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Validate XDimension before assigning to avoid runtime exceptions
                if (xDimension <= 0f)
                {
                    throw new ArgumentOutOfRangeException(nameof(xDimension), "XDimension must be greater than zero.");
                }

                // Apply the validated XDimension
                generator.Parameters.Barcode.XDimension.Point = xDimension;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved: {outputPath} (XDimension={xDimension})");
            }
        }
        catch (ArgumentException ex)
        {
            // Handles validation errors such as invalid XDimension
            Console.WriteLine($"Argument error while generating barcode: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors during barcode generation
            Console.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }
}