using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulated HTTP request payload (JSON)
        string requestBody = "{\"linear\":\"(01)03212345678906\",\"twoD\":\"(21)A12345678\"}";

        // Parse JSON payload
        using (JsonDocument doc = JsonDocument.Parse(requestBody))
        {
            JsonElement root = doc.RootElement;
            string linearPart = root.GetProperty("linear").GetString();
            string twoDPart = root.GetProperty("twoD").GetString();

            // Combine linear and 2D parts using the '|' splitter as required for GS1 Composite
            string combinedCodeText = $"{linearPart}|{twoDPart}";

            // Create barcode generator for GS1 Composite Bar
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
            {
                // Configure linear component (e.g., GS1 Code128)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Configure 2D component (e.g., CC-A MicroPDF417)
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Example of additional settings
                generator.Parameters.Barcode.XDimension.Pixels = 3f;   // X-dimension for both components
                generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the linear component

                // Save the generated barcode image to a PNG file
                string outputPath = "gs1composite.png";
                generator.Save(outputPath);
                Console.WriteLine($"GS1 Composite barcode saved to '{Path.GetFullPath(outputPath)}'.");
            }
        }
    }
}