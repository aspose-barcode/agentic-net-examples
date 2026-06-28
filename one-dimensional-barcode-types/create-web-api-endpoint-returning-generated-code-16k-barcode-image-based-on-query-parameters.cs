using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode using Aspose.BarCode,
/// encoding it as PNG, and outputting the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// <list type="bullet">
    ///   <item><description>args[0] – Text to encode in the barcode (default: a long numeric string).</description></item>
    ///   <item><description>args[1] – Desired aspect ratio for the barcode (default: 2.0).</description></item>
    /// </list>
    /// </param>
    static void Main(string[] args)
    {
        // Default barcode content and aspect ratio
        string codeText = "1234567890123456789012345678901234567890";
        float aspectRatio = 2.0f;

        // Override defaults with command‑line arguments when provided
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
        {
            codeText = args[0];
        }

        if (args.Length >= 2 && float.TryParse(args[1], out float parsedRatio) && parsedRatio > 0f)
        {
            aspectRatio = parsedRatio;
        }

        try
        {
            // Use a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Create a barcode generator for Code 16K with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Apply the requested aspect ratio to the barcode parameters
                    generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

                    // Render the barcode as PNG and write it into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Convert the PNG bytes to a Base64 string (simulating an API response)
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}