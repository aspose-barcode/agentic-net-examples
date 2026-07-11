// Title: Postal barcode generation with XDimension validation
// Description: Demonstrates creating a Postnet barcode while validating the XDimension parameter to ensure it is positive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension, BarColor, and BackColor. Developers often need to generate valid postal barcodes for mailing applications and must validate dimensions to meet specification requirements.
// Prompt: Implement error handling for invalid XDimension values when creating a postal barcode.
// Tags: barcode, postal, xdimension, validation, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Postnet barcodes with validation for the XDimension parameter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over sample XDimension values,
    /// attempts to generate a barcode for each, and reports success or errors.
    /// </summary>
    static void Main()
    {
        // Sample XDimension values to demonstrate validation (valid, negative, zero)
        float[] xDimensions = { 2f, -1f, 0f };

        foreach (float xDim in xDimensions)
        {
            try
            {
                // Build a unique file name based on the current XDimension value
                string fileName = $"postal_{xDim}.png";

                // Attempt to create and save the barcode
                CreatePostalBarcode(xDim, fileName);

                // Inform the user of successful generation
                Console.WriteLine($"Barcode generated and saved to '{fileName}' with XDimension = {xDim}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Handle validation errors for XDimension
                Console.WriteLine($"Invalid XDimension ({xDim}): {ex.Message}");
            }
            catch (Aspose.BarCode.BarCodeException ex)
            {
                // Handle errors thrown by the Aspose.BarCode library
                Console.WriteLine($"Barcode generation error for XDimension ({xDim}): {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Creates a Postnet barcode using the specified XDimension and saves it to the given path.
    /// </summary>
    /// <param name="xDimension">The XDimension (module width) in points; must be greater than zero.</param>
    /// <param name="outputPath">The file path where the generated barcode image will be saved.</param>
    static void CreatePostalBarcode(float xDimension, string outputPath)
    {
        // Validate XDimension before applying it to the generator
        if (xDimension <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(xDimension), "XDimension must be greater than zero.");
        }

        // Initialize the barcode generator for the Postnet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345678"))
        {
            // Apply the validated XDimension to control module size
            generator.Parameters.Barcode.XDimension.Point = xDimension;

            // Optional visual settings: black bars on a white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }
    }
}