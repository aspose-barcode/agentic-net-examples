using System;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDIExample
{
    // Service contract for barcode generation
    public interface IBarcodeService
    {
        void GenerateBarcode(string codeText, string outputPath);
    }

    // Concrete implementation using Aspose.BarCode
    public class BarcodeService : IBarcodeService
    {
        public void GenerateBarcode(string codeText, string outputPath)
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Example: set image resolution and background color
                generator.Parameters.Resolution = 300;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the barcode image as PNG
                generator.Save(outputPath);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Set up a simple DI container
            var services = new ServiceCollection();
            services.AddSingleton<IBarcodeService, BarcodeService>();
            var provider = services.BuildServiceProvider();

            // Resolve the barcode service
            var barcodeService = provider.GetRequiredService<IBarcodeService>();

            // Sample data
            string sampleText = "ABC123456";
            string outputFile = "sample_barcode.png";

            // Generate the barcode
            barcodeService.GenerateBarcode(sampleText, outputFile);

            Console.WriteLine($"Barcode generated and saved to '{outputFile}'.");
        }
    }
}