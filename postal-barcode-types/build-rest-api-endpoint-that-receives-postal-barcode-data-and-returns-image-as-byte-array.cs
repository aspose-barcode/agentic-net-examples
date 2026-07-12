// Title: Generate Postnet barcode image and return as byte array via simulated REST endpoint
// Description: Demonstrates creating a Postnet postal barcode from input data and returning the PNG image as a byte array, suitable for a REST API response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.Postnet, configure image parameters, and obtain a bitmap image. Developers building web services that need to produce barcode images for mailing or shipping can reference this pattern for creating PNG byte arrays to embed in JSON responses or files.
// Prompt: Build a REST API endpoint that receives postal barcode data and returns the image as a byte array.
// Tags: postnet, barcode generation, image output, png, byte array, aspose.barcode, rest api

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simulated REST API endpoint that generates a Postnet barcode image from postal data.
/// </summary>
class Program
{
    /// <summary>
    /// Simulated REST endpoint: receives postal barcode data and returns image bytes.
    /// </summary>
    /// <param name="postalData">The postal data to encode (e.g., ZIP code).</param>
    /// <returns>PNG image bytes representing the generated barcode.</returns>
    static byte[] GeneratePostalBarcode(string postalData)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(postalData))
            throw new ArgumentException("Postal data must be provided.", nameof(postalData));

        // Create a barcode generator for Postnet (postal) symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postalData))
        {
            // Optional: adjust image size or resolution if needed
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 96;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format and return the byte array
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Entry point for the console demonstration. Accepts postal data via command‑line argument,
    /// generates the barcode, and writes the Base64 representation to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is optional postal data.</param>
    static void Main(string[] args)
    {
        // In a real REST scenario the postal data would come from the request body.
        // Here we use a sample value or a command‑line argument for demonstration.
        string postalData = args.Length > 0 ? args[0] : "12345";

        try
        {
            // Generate barcode image bytes
            byte[] imageBytes = GeneratePostalBarcode(postalData);

            // Output the result as a Base64 string (simulating a JSON response body)
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine(base64);
        }
        catch (Exception ex)
        {
            // Write error details to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}