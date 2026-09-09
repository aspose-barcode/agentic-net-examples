// Title: Generate Postal Barcode with XDimension Validation
// Description: Demonstrates creating a postal barcode (Postnet) using Aspose.BarCode, including validation of the XDimension parameter to ensure it is positive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use EncodeTypes, BarcodeGenerator, and related parameter classes to produce barcode images. Typical use cases include generating postal barcodes for mailing applications, where developers need to control dimensions and handle invalid input gracefully. The snippet illustrates common patterns such as symbology resolution, parameter configuration, image saving, and exception handling.
// Prompt: Implement error handling for invalid XDimension values when creating a postal barcode.
// Tags: barcode, postal, xdimension, validation, aspose.barcode, generation, png, error-handling

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a postal barcode and validates the XDimension value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Demonstrates barcode generation with both valid and invalid XDimension values.
    /// </summary>
    static void Main()
    {
        // Sample data for barcode generation
        string symbology = "Postnet";
        string codeText = "1159628792";
        float validXDimension = 3f;
        float invalidXDimension = -1f;

        // Generate barcode using a valid XDimension
        Console.WriteLine("Generating barcode with valid XDimension...");
        CreatePostalBarcode(symbology, codeText, validXDimension);

        Console.WriteLine();

        // Attempt to generate barcode using an invalid XDimension
        Console.WriteLine("Generating barcode with invalid XDimension...");
        CreatePostalBarcode(symbology, codeText, invalidXDimension);
    }

    /// <summary>
    /// Creates a postal barcode image using the specified symbology, code text, and XDimension.
    /// Includes validation of the XDimension and comprehensive error handling.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Postnet").</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="xDimension">Desired XDimension (module width) in points; must be greater than zero.</param>
    static void CreatePostalBarcode(string symbologyName, string codeText, float xDimension)
    {
        // Resolve the symbology name to the corresponding EncodeTypes field via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the resolved field value to BaseEncodeType for use with BarcodeGenerator
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Validate that XDimension is a positive value
        if (xDimension <= 0f)
        {
            Console.WriteLine($"Invalid XDimension value: {xDimension}. Must be greater than zero.");
            return;
        }

        try
        {
            // Initialize the barcode generator with the selected symbology and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the XDimension setting
                generator.Parameters.Barcode.XDimension.Point = xDimension;

                // Generate the barcode image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Construct a unique file name based on parameters
                    string fileName = $"Postal_{symbologyName}_{xDimension}_px.png";
                    string fullPath = Path.Combine(Path.GetTempPath(), fileName);

                    // Save the image as PNG
                    bitmap.Save(fullPath, ImageFormat.Png);
                    Console.WriteLine($"Barcode saved to: {fullPath}");
                }
            }
        }
        // Handle specific barcode generation errors
        catch (BarCodeException ex)
        {
            Console.WriteLine($"BarCodeException: {ex.Message}");
        }
        // Handle any other unexpected errors
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}