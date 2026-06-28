using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image in PNG format based on the provided symbology name and code text.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image data.</returns>
    static byte[] GenerateBarcodeImage(string symbologyName, string codeText)
    {
        // Resolve the symbology name to the corresponding EncodeTypes static field using reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            // Throw if the requested symbology is not supported.
            throw new ArgumentException($"Unsupported symbology: {symbologyName}");
        }

        // EncodeTypes members are of type BaseEncodeType; retrieve the value.
        var encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a BarcodeGenerator with the resolved type and the provided code text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image bytes.
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the application. Simulates receiving input parameters and outputs a Base64-encoded PNG barcode.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET MVC integration cannot be hosted in the snippet runner.
        // The core barcode generation logic is demonstrated below.

        // Simulate receiving multipart/form-data with fields "symbology" and "codetext".
        string receivedSymbology = "Code128"; // sample symbology
        string receivedCodeText = "Sample123"; // sample code text

        try
        {
            // Generate the barcode image bytes.
            byte[] imageBytes = GenerateBarcodeImage(receivedSymbology, receivedCodeText);

            // Convert the image bytes to a Base64 string to simulate an HTTP response body.
            string base64Image = Convert.ToBase64String(imageBytes);
            Console.WriteLine(base64Image);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}