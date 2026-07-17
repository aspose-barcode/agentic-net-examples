// Title: Generate Barcode Image from Symbology and Text
// Description: Demonstrates generating a barcode image using Aspose.BarCode and returning it as a stream, suitable for use in a RESTful endpoint.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcode images with various symbologies using the BarcodeGenerator class. Typical use cases include web services that need to produce barcodes on‑the‑fly for labels, tickets, or inventory systems. Developers often need to accept input via HTTP requests, configure barcode parameters, and return the image in a common format such as PNG.
// Prompt: Develop a RESTful endpoint that accepts multipart/form-data and returns generated barcode image stream.
// Tags: barcode, symbology, generation, png, aspose.barcode, rest, endpoint, multipart

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates core barcode generation logic that can be integrated into a RESTful service.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates a barcode based on command‑line arguments
    /// and writes the image to a file and its Base64 representation to the console.
    /// </summary>
    /// <param name="args">
    /// args[0] – optional symbology name (e.g., "Code128"); defaults to "Code128".<br/>
    /// args[1] – optional code text; defaults to "Sample123".
    /// </param>
    static void Main(string[] args)
    {
        // In a real RESTful service, symbology and code text would be extracted from a multipart/form-data request.
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "Sample123";

        try
        {
            // Generate the barcode and obtain the image as a memory stream.
            using (var imageStream = GenerateBarcode(symbologyName, codeText))
            {
                // Persist the image to a file for verification purposes.
                const string outputFile = "barcode.png";
                using (var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    imageStream.Position = 0;
                    imageStream.CopyTo(fileStream);
                }

                // Output the Base64‑encoded PNG to the console.
                imageStream.Position = 0;
                byte[] bytes = imageStream.ToArray();
                string base64 = Convert.ToBase64String(bytes);
                Console.WriteLine("Barcode generated successfully.");
                Console.WriteLine("Base64 PNG:");
                Console.WriteLine(base64);
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and code text.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (must match a field in <see cref="EncodeTypes"/>).</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the PNG image data.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string codeText)
    {
        // Resolve the symbology name to the corresponding EncodeTypes field via reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        var memoryStream = new MemoryStream();

        // Configure the barcode generator with common parameters.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;               // Width of the narrowest bar.
            generator.Parameters.Barcode.BarHeight.Point = 40f;               // Height of the barcode.
            generator.Parameters.Barcode.FilledBars = false;                  // Use unfilled bars for better readability.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false; // Suppress validation exceptions.

            // Save the generated barcode as PNG into the memory stream.
            generator.Save(memoryStream, BarCodeImageFormat.Png);
        }

        return memoryStream;
    }
}