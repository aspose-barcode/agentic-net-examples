using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeDiDemo
{
    // Service for generating barcodes
    public interface IBarcodeGeneratorService
    {
        byte[] GenerateBarcode(BaseEncodeType type, string text);
    }

    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public byte[] GenerateBarcode(BaseEncodeType type, string text)
        {
            // Create a barcode generator with the specified type and text
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Example: set resolution and auto size mode
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Save the generated barcode to a memory stream as PNG
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Return the image bytes
                    return ms.ToArray();
                }
            }
        }
    }

    // Service for reading barcodes from an image
    public interface IBarcodeReaderService
    {
        string[] ReadBarcodes(byte[] imageBytes);
    }

    public class BarcodeReaderService : IBarcodeReaderService
    {
        public string[] ReadBarcodes(byte[] imageBytes)
        {
            // Load the image bytes into a memory stream
            using (var ms = new MemoryStream(imageBytes))
            {
                // Initialize the barcode reader for all supported types
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();
                    // Extract the decoded text from each result
                    return results.Select(r => r.CodeText).ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Demo program showing DI usage for barcode generation and reading.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set up DI container
            var services = new ServiceCollection();
            services.AddSingleton<IBarcodeGeneratorService, BarcodeGeneratorService>();
            services.AddSingleton<IBarcodeReaderService, BarcodeReaderService>();
            var provider = services.BuildServiceProvider();

            // Resolve services
            var generatorService = provider.GetRequiredService<IBarcodeGeneratorService>();
            var readerService = provider.GetRequiredService<IBarcodeReaderService>();

            // Generate a Code128 barcode
            BaseEncodeType encodeType = EncodeTypes.Code128;
            string codeText = "1234567890";
            byte[] barcodeImage = generatorService.GenerateBarcode(encodeType, codeText);
            Console.WriteLine($"Generated barcode ({encodeType}) with text '{codeText}'.");

            // Read the barcode back
            string[] decodedTexts = readerService.ReadBarcodes(barcodeImage);
            foreach (var txt in decodedTexts)
            {
                Console.WriteLine($"Decoded barcode text: {txt}");
            }

            // End of demo
        }
    }
}