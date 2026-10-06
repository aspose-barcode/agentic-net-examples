// Title: Generate Code128 Barcode with Checksum and Return Base64 PNG
// Description: Creates a Code128 barcode, enables checksum control, and outputs the image as a Base64‑encoded PNG string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set checksum options, and export barcodes to PNG format. Developers building web services, REST APIs, or desktop applications often need to generate barcodes on‑the‑fly and return them as image data. The snippet shows typical usage of EncodeTypes, generator parameters, and image conversion for such scenarios.
// Prompt: Create a REST API endpoint that receives barcode data, applies checksum control, and returns a PNG image.
// Tags: code128, checksum, png, base64, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode, applying checksum control,
/// and returning the result as a Base64‑encoded PNG image.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point that generates a barcode for sample data and prints the Base64 PNG string.
    /// </summary>
    public static void Main()
    {
        // Sample barcode data to encode.
        string sampleData = "1234567890";

        // Generate the barcode image and obtain its Base64 representation.
        string base64Image = ProcessBarcode(sampleData);

        // Output the Base64 string to the console.
        Console.WriteLine("Base64 PNG Image:");
        Console.WriteLine(base64Image);
    }

    /// <summary>
    /// Generates a Code128 barcode with checksum enabled, saves it as PNG,
    /// and returns the image as a Base64‑encoded string.
    /// </summary>
    /// <param name="data">The data to encode in the barcode.</param>
    /// <returns>Base64 string representing the PNG image of the barcode.</returns>
    public static string ProcessBarcode(string data)
    {
        // Validate input data.
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentException("Barcode data must not be null or empty.", nameof(data));
        }

        // Create a barcode generator for Code128 with the provided data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            // Enable checksum calculation and ensure it is displayed.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string.
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}