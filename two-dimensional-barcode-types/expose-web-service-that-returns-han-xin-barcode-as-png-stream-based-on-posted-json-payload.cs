// Title: Generate Han Xin Barcode from JSON Payload and Save as PNG
// Description: Demonstrates creating a Han Xin (Chinese 2D) barcode using Aspose.BarCode based on a JSON request and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as error correction level, encode mode, and version for Han Xin symbology. It uses BarcodeGenerator, HanXinErrorLevel, HanXinEncodeMode, and HanXinVersion classes to produce a PNG image, a common task for developers building QR‑like barcode services.
// Prompt: Expose a web service that returns Han Xin barcode as PNG stream based on posted JSON payload.
// Tags: hanxin,barcode,generation,json,aspose.barcode,png,base64,2d symbology

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Han Xin barcode from a JSON payload,
/// saves it as a PNG file, and optionally outputs a Base64 string.
/// </summary>
class Program
{
    // Model representing the expected JSON payload
    private class HanXinRequest
    {
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; }      // e.g., "L2"
        public string EncodeMode { get; set; }      // e.g., "ECI"
        public string Version { get; set; }         // e.g., "Version24"
    }

    /// <summary>
    /// Entry point: parses a simulated JSON request, configures the barcode generator,
    /// and writes the resulting PNG to disk (and Base64 to console).
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload (replace with actual request body in a real web service)
        string jsonPayload = @"{
            ""CodeText"": ""Sample123"",
            ""ErrorLevel"": ""L2"",
            ""EncodeMode"": ""ECI"",
            ""Version"": ""Version24""
        }";

        // Deserialize payload into a strongly‑typed request object
        HanXinRequest request;
        try
        {
            request = JsonSerializer.Deserialize<HanXinRequest>(jsonPayload);
            if (request == null || string.IsNullOrEmpty(request.CodeText))
                throw new ArgumentException("Invalid request payload.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse request: {ex.Message}");
            return;
        }

        // Create barcode generator for Han Xin symbology with the supplied text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, request.CodeText))
        {
            // Apply error correction level if specified
            if (!string.IsNullOrEmpty(request.ErrorLevel))
            {
                switch (request.ErrorLevel.ToUpperInvariant())
                {
                    case "L1":
                        generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L1;
                        break;
                    case "L2":
                        generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
                        break;
                    case "L3":
                        generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L3;
                        break;
                    case "L4":
                        generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L4;
                        break;
                }
            }

            // Apply encode mode if specified
            if (!string.IsNullOrEmpty(request.EncodeMode))
            {
                switch (request.EncodeMode.ToUpperInvariant())
                {
                    case "AUTO":
                        generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
                        break;
                    case "BINARY":
                        generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Binary;
                        break;
                    case "ECI":
                        generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
                        generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;
                        break;
                    case "UNICODE":
                        generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Unicode;
                        break;
                    case "URI":
                        generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.URI;
                        break;
                    case "EXTENDED":
                        generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Extended;
                        break;
                }
            }

            // Apply version if specified (using reflection to map string to enum)
            if (!string.IsNullOrEmpty(request.Version))
            {
                var field = typeof(HanXinVersion).GetField(request.Version);
                if (field != null)
                {
                    var versionValue = (HanXinVersion)field.GetValue(null);
                    generator.Parameters.Barcode.HanXin.Version = versionValue;
                }
            }

            // Render barcode to a memory stream in PNG format
            using (var pngStream = new MemoryStream())
            {
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0;

                // Write PNG to a file for demonstration purposes
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "hanxin.png");
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Han Xin barcode saved to: {outputPath}");

                // Output Base64 representation of the PNG (optional)
                string base64 = Convert.ToBase64String(pngStream.ToArray());
                Console.WriteLine($"Base64 PNG: {base64}");
            }
        }
    }
}