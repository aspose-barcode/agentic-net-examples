// Title: Demonstrate Dependency Injection for Barcode Generation and Reading in a Console App
// Description: Shows how to use Aspose.BarCode to generate a Code128 barcode image and read it back, using a simple DI pattern.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode creation and recognition. It illustrates the use of BarcodeGenerator, BarCodeReader, and related parameter classes, typical for developers needing to integrate barcode functionality via dependency injection in .NET applications.
// Prompt: Use dependency injection to provide barcode generator and reader services within an ASP.NET Core application.
// Tags: code128, barcode generation, barcode reading, png, aspnet core, dependency injection, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDiConsoleApp
{
    // Service contract for barcode generation
    public interface IBarcodeGeneratorService
    {
        void Generate(string codeText, string outputPath);
    }

    // Service contract for barcode reading
    public interface IBarcodeReaderService
    {
        void Read(string imagePath);
    }

    // Concrete implementation that creates a barcode image using Aspose.BarCode
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void Generate(string codeText, string outputPath)
        {
            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create and configure the barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode generated and saved to: {outputPath}");
        }
    }

    // Concrete implementation that reads a barcode image using Aspose.BarCode
    public class BarcodeReaderService : IBarcodeReaderService
    {
        public void Read(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Specify the expected symbology for decoding
            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                // Iterate through all detected barcodes (checksum validation is on by default)
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded text: {result.CodeText}");
                }
            }
        }
    }

    // Minimalistic service container to simulate ASP.NET Core DI in a console app
    public class ServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        // Register a service implementation
        public void Add<TService>(TService implementation) where TService : class
        {
            _services[typeof(TService)] = implementation;
        }

        // Resolve a registered service
        public TService Get<TService>() where TService : class
        {
            if (_services.TryGetValue(typeof(TService), out var service))
            {
                return service as TService;
            }

            throw new InvalidOperationException($"Service of type {typeof(TService).Name} not registered.");
        }
    }

    /// <summary>
    /// Simple console application demonstrating DI for barcode services.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point: sets up services, generates a barcode, and reads it.
        /// </summary>
        static void Main()
        {
            // Simulate ASP.NET Core DI with a basic ServiceProvider
            var serviceProvider = new ServiceProvider();
            serviceProvider.Add<IBarcodeGeneratorService>(new BarcodeGeneratorService());
            serviceProvider.Add<IBarcodeReaderService>(new BarcodeReaderService());

            // Resolve services
            var generator = serviceProvider.Get<IBarcodeGeneratorService>();
            var reader = serviceProvider.Get<IBarcodeReaderService>();

            // Prepare temporary folder and file path
            string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
            string barcodePath = Path.Combine(tempFolder, "sample.png");

            // Generate a Code128 barcode and then read it back
            generator.Generate("1234567890", barcodePath);
            reader.Read(barcodePath);
        }
    }
}