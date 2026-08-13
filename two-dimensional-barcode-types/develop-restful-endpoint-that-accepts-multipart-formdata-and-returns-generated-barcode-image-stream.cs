// Title: Generate barcode image and output as PNG stream
// Description: Demonstrates creating a barcode using Aspose.BarCode, saving it to a MemoryStream, and optionally writing to a file or Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and image format classes to produce barcode images. Typical use cases include RESTful services that accept input data and return barcode images for labeling, inventory, or ticketing systems. Developers often need to convert the generated image to streams for HTTP responses or further processing.
// Prompt: Develop a RESTful endpoint that accepts multipart/form-data and returns generated barcode image stream.
// Tags: barcode, generation, png, memorystream, aspnetcore, aspose.barcode, encode-types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides methods to generate barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image based on the specified symbology name and code text.
    /// Returns a <see cref="MemoryStream"/> containing the PNG image.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A memory stream with the generated PNG image, or null if the symbology is unknown.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string codeText)
    {
        // Resolve symbology name to an EncodeTypes field via reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return null;
        }

        // Retrieve the corresponding BaseEncodeType value.
        var encodeType = (BaseEncodeType)field.GetValue(null);
        var ms = new MemoryStream();

        // Use a using block for BarcodeGenerator (IDisposable) to ensure resources are released.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set basic visual parameters.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode directly to the memory stream in PNG format.
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset the stream position so it can be read from the beginning.
        ms.Position = 0;
        return ms;
    }

    /// <summary>
    /// Entry point of the console demonstration. Generates a barcode based on command‑line arguments
    /// and writes the image to a file and Base64 output.
    /// </summary>
    /// <param name="args">Command‑line arguments: symbology name and code text.</param>
    static void Main(string[] args)
    {
        // In a real RESTful service this would be populated from multipart/form-data.
        // For this console demo we use command‑line arguments with defaults.
        string symbology = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "12345";

        using (var barcodeStream = GenerateBarcode(symbology, codeText))
        {
            if (barcodeStream == null)
            {
                // Generation failed; exit with error code.
                Environment.Exit(1);
            }

            // Save to a file for verification.
            const string outputPath = "barcode.png";
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Barcode generated and saved to {outputPath}");

            // Also output Base64 representation (simulating HTTP response body).
            string base64 = Convert.ToBase64String(barcodeStream.ToArray());
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(base64);
        }

        // Note: In a full ASP.NET Core application this logic would be placed
        // inside a controller action that reads the multipart request and returns
        // the image stream as the HTTP response. The console program demonstrates
        // the core barcode generation logic required for such an endpoint.
    }
}