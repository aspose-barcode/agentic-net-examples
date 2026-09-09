// Title: Generate GS1 Composite Barcode from JSON Payload
// Description: Demonstrates parsing a JSON payload that defines barcode parameters and generating a GS1 Composite barcode saved as a PNG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on composite barcode creation. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and TwoDComponentType, illustrating typical use cases like dynamic barcode generation in microservices. Developers often need to convert structured data into barcodes for inventory, shipping, and tracking, and this snippet provides a clear pattern for handling JSON input and configuring composite symbologies.
// Prompt: Develop a microservice that receives JSON payload and returns generated GS1 Composite barcode as PNG.
// Tags: gs1 composite barcode, json, generation, png, aspose.barcode, encode types, microservice

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that parses a JSON payload describing GS1 Composite barcode settings,
/// generates the barcode using Aspose.BarCode, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Parses JSON, configures the barcode generator,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload containing barcode parameters
        string json = @"{
            ""linear"": ""(01)12345678901231"",
            ""twod"": ""(01)00123456789012"",
            ""linearType"": ""GS1Code128"",
            ""twoDType"": ""CC_B"",
            ""allowOnlyGS1"": false,
            ""xDimension"": 2,
            ""output"": ""gs1composite.png""
        }";

        // Parse the JSON payload
        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            JsonElement root = doc.RootElement;

            // Extract individual properties from the JSON
            string linear = root.GetProperty("linear").GetString();
            string twod = root.GetProperty("twod").GetString();
            string linearTypeName = root.GetProperty("linearType").GetString();
            string twoDTypeName = root.GetProperty("twoDType").GetString();
            bool allowOnlyGS1 = root.GetProperty("allowOnlyGS1").GetBoolean();
            int xDim = root.GetProperty("xDimension").GetInt32();
            string outputPath = root.GetProperty("output").GetString();

            // Resolve the linear component symbology from its name
            var field = typeof(EncodeTypes).GetField(linearTypeName);
            if (field == null)
            {
                Console.WriteLine($"Unknown linear symbology: {linearTypeName}");
                return;
            }
            BaseEncodeType linearEncodeType = (BaseEncodeType)field.GetValue(null);

            // Resolve the 2D component type from its name
            if (!Enum.TryParse<TwoDComponentType>(twoDTypeName, true, out TwoDComponentType twoDComponentType))
            {
                Console.WriteLine($"Unknown 2D component type: {twoDTypeName}");
                return;
            }

            // Combine linear and 2D code texts using the pipe separator required for composite barcodes
            string combinedCodeText = $"{linear}|{twod}";

            // Create and configure the barcode generator for a GS1 Composite barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = (float)xDim;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncodeType;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = twoDComponentType;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = allowOnlyGS1;

                // Ensure the output directory exists before saving the image
                string directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
            }
        }
    }
}