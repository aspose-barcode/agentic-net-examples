using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AsposeBarcodeDiDemo
{
    // Service for generating barcodes
    public interface IBarcodeGeneratorService
    {
        void Generate(string codeText, string filePath);
    }

    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void Generate(string codeText, string filePath)
        {
            // Create generator with Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set some visual parameters
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image
                generator.Save(filePath);
            }
        }
    }

    // Service for reading barcodes
    public interface IBarcodeReaderService
    {
        string Read(string filePath);
    }

    public class BarcodeReaderService : IBarcodeReaderService
    {
        public string Read(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Barcode image not found.", filePath);

            // Initialize reader for Code128 decoding
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                if (results.Length > 0)
                    return results[0].CodeText;
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Setup DI container
            var services = new ServiceCollection();
            services.AddSingleton<IBarcodeGeneratorService, BarcodeGeneratorService>();
            services.AddSingleton<IBarcodeReaderService, BarcodeReaderService>();
            var provider = services.BuildServiceProvider();

            // Resolve services
            var generator = provider.GetRequiredService<IBarcodeGeneratorService>();
            var reader = provider.GetRequiredService<IBarcodeReaderService>();

            // Sample data
            string codeText = "123ABC456";
            string filePath = "barcode.png";

            // Generate and read barcode
            generator.Generate(codeText, filePath);
            string readText = reader.Read(filePath);

            Console.WriteLine($"Generated barcode with text: {codeText}");
            Console.WriteLine($"Read barcode text: {(readText ?? "No barcode detected")}");
        }
    }
}