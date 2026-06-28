using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a postal (Postnet) barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode type (Postnet) and the data to encode.
        BaseEncodeType encodeType = EncodeTypes.Postnet;
        string codeText = "12345678";

        // Example XDimension value (intentionally set to an invalid value to show error handling).
        float xDimension = -0.5f;

        try
        {
            // Validate the XDimension before using it.
            ValidateXDimension(xDimension);

            // Create a barcode generator with the specified type and text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the validated XDimension to the barcode parameters.
                generator.Parameters.Barcode.XDimension.Point = xDimension;

                // Define the output file path and save the generated barcode.
                string outputPath = "postal.png";
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        // Handle specific validation errors.
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Invalid XDimension: {ex.Message}");
        }
        // Handle any other unexpected errors.
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates that the XDimension value is greater than zero.
    /// </summary>
    /// <param name="value">The XDimension value to validate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than or equal to zero.</exception>
    static void ValidateXDimension(float value)
    {
        // Ensure XDimension is a positive number.
        if (value <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "XDimension must be greater than zero.");
        }
    }
}