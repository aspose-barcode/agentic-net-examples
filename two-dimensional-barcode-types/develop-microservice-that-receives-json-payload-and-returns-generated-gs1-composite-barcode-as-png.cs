using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace GS1CompositeBarcodeMicroservice
{
    // Model representing the incoming JSON payload
    public class BarcodeRequest
    {
        public string LinearCode { get; set; }
        public string TwoDCode { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Simulated JSON payload
            string jsonPayload = @"{ ""LinearCode"": ""(01)03212345678906"", ""TwoDCode"": ""(21)A1B2C3D4E5F6G7H8"" }";

            // Deserialize the payload
            BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null || string.IsNullOrWhiteSpace(request.LinearCode) || string.IsNullOrWhiteSpace(request.TwoDCode))
            {
                throw new ArgumentException("Invalid JSON payload: LinearCode and TwoDCode must be provided.");
            }

            // Combine linear and 2D parts using the '|' splitter as required for GS1 Composite
            string combinedCodeText = $"{request.LinearCode}|{request.TwoDCode}";

            // Create the barcode generator for GS1 Composite Bar
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
            {
                // Set linear component type (e.g., GS1Code128)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Set 2D component type (e.g., CC_A)
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Configure PDF417 aspect ratio (used for CC_A/CC_B/CC_C)
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

                // Set X-Dimension for both components
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Set height of the linear (1D) component
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Generate the barcode image into a memory stream as PNG
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // Write the PNG to a file
                    File.WriteAllBytes("gs1composite.png", memoryStream.ToArray());
                }
            }

            Console.WriteLine("GS1 Composite barcode generated successfully: gs1composite.png");
        }
    }
}