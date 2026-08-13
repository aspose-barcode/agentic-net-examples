// Title: Postal Barcode Generation with XDimension Validation
// Description: Demonstrates creating a Postnet postal barcode using Aspose.BarCode while validating the XDimension parameter to ensure it is positive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies such as Postnet. It shows how to configure barcode parameters (e.g., XDimension), handle invalid input, and save the image using BarcodeGenerator and related classes. Developers working with postal barcode creation, parameter validation, and image output can use this pattern as a reference.
/// Prompt: Implement error handling for invalid XDimension values when creating a postal barcode.
/// Tags: barcode, postal, postnet, xdimension, validation, aspnet, aspnetcore, aspose.barcode, image, png, error-handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Postnet postal barcodes and demonstrates validation of the XDimension parameter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a valid barcode and attempts to generate an invalid one to showcase error handling.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a valid postal barcode
        // ------------------------------------------------------------
        try
        {
            // Create a barcode with a positive XDimension value
            CreatePostalBarcode("12345", 2f, "postal_valid.png");
            Console.WriteLine("Valid barcode generated successfully.");
        }
        catch (Exception ex)
        {
            // Unexpected errors during valid barcode generation
            Console.WriteLine($"Error generating valid barcode: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Attempt to generate a barcode with an invalid XDimension
        // ------------------------------------------------------------
        try
        {
            // XDimension is negative, which should trigger validation logic
            CreatePostalBarcode("12345", -1f, "postal_invalid.png");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Expected validation exception for non‑positive XDimension
            Console.WriteLine($"Caught expected argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other unexpected exceptions
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a Postnet postal barcode with the specified XDimension.
    /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="xDimension"/> is not positive.
    /// </summary>
    /// <param name="codeText">The postal code to encode.</param>
    /// <param name="xDimension">Module size in points (must be &gt; 0).</param>
    /// <param name="outputPath">File path to save the generated barcode image.</param>
    static void CreatePostalBarcode(string codeText, float xDimension, string outputPath)
    {
        // Validate that the XDimension is a positive value
        if (xDimension <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(xDimension), "XDimension must be greater than zero.");
        }

        // Ensure the output directory exists before saving the image
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for the Postnet symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Apply the validated XDimension (module size) to the barcode parameters
            generator.Parameters.Barcode.XDimension.Point = xDimension;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}