using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode from a JSON payload and outputting it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the JSON payload containing the symbology and code text.
    /// </summary>
    private class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Entry point of the application.
    /// Parses a JSON payload, resolves the barcode symbology, generates the barcode,
    /// and writes the image as a Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload; in a real scenario this would come from an HTTP request body.
        string jsonPayload = "{\"symbology\":\"Code128\",\"codeText\":\"123ABC\"}";

        // Deserialize the JSON payload into a BarcodeRequest object.
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            // Validate required fields.
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Symbology) ||
                request.CodeText == null)
            {
                throw new ArgumentException("Invalid JSON payload.");
            }
        }
        catch (Exception ex)
        {
            // Output parsing errors and exit.
            Console.WriteLine($"Error parsing request: {ex.Message}");
            return;
        }

        // Resolve the symbology name to a BaseEncodeType using reflection.
        var field = typeof(EncodeTypes).GetField(request.Symbology, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }

        // Ensure the field value is a BaseEncodeType.
        if (!(field.GetValue(null) is BaseEncodeType encodeType))
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {request.Symbology}");
            return;
        }

        // Generate the barcode image and output it as a Base64 string.
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the image bytes to a Base64 string.
                string base64Image = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(base64Image);
            }
        }
    }
}