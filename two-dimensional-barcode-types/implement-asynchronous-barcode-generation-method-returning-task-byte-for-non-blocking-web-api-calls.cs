// Title: Asynchronous Barcode Generation Example
// Description: Demonstrates generating a barcode image asynchronously and returning it as a byte array, suitable for non‑blocking web API scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcode images on demand. Typical use cases include web services that need to produce barcodes without blocking threads, such as order processing, ticketing, or inventory systems. Developers often require asynchronous methods to keep APIs responsive and scalable, and this snippet illustrates a common pattern for achieving that with Aspose.BarCode.
/// Prompt: Implement asynchronous barcode generation method returning Task<byte[]> for non‑blocking web API calls.
/// Tags: barcode, symbology, asynchronous, generation, png, aspose.barcode, task, bytearray

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point and helper method for generating barcodes asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Demonstrates calling the asynchronous barcode generation method and outputs the result size.
    /// </summary>
    static void Main()
    {
        // Initiate asynchronous barcode generation for Code128 symbology with sample text
        Task<byte[]> task = GenerateBarcodeAsync("Code128", "12345678");

        // Synchronously wait for the task to complete and retrieve the byte array
        byte[] barcodeBytes = task.GetAwaiter().GetResult();

        // Display the length of the generated PNG byte array
        Console.WriteLine($"Generated barcode byte array length: {barcodeBytes.Length}");
    }

    /// <summary>
    /// Generates a barcode image asynchronously and returns the image data as a PNG byte array.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the PNG image bytes.</returns>
    public static Task<byte[]> GenerateBarcodeAsync(string symbologyName, string codeText)
    {
        // Run the barcode generation on a background thread to avoid blocking the caller
        return Task.Run(() =>
        {
            // Resolve the symbology name to the corresponding BaseEncodeType using reflection
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                // If the symbology is not found, log the issue and return an empty byte array
                Console.WriteLine($"Unknown symbology: {symbologyName}");
                return new byte[0];
            }

            // Cast the reflected value to BaseEncodeType
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a BarcodeGenerator with the resolved encode type and provided text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Return the stream contents as a byte array
                    return ms.ToArray();
                }
            }
        });
    }
}