// Title: Dependency Injection Example for Aspose.BarCode Generation
// Description: Demonstrates how to use a simple DI container to inject a barcode generation service and create a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and related classes. It illustrates typical scenarios where developers need to generate barcodes dynamically via a service layer, employing dependency injection for better testability and modularity. Ideal for applications requiring runtime barcode creation such as inventory, shipping, or ticketing systems.
// Prompt: Implement dependency injection to provide a barcode generation service throughout the application.
// Tags: barcode generation, dependency injection, code128, png, aspose.barcode, service locator

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDIExample
{
    // Service contract for barcode generation
    public interface IBarcodeService
    {
        void Generate(string text, string symbologyName, string outputPath);
    }

    // Concrete implementation of the barcode service
    public class BarcodeService : IBarcodeService
    {
        public void Generate(string text, string symbologyName, string outputPath)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Code text must not be empty.", nameof(text));

            if (string.IsNullOrWhiteSpace(symbologyName))
                throw new ArgumentException("Symbology name must not be empty.", nameof(symbologyName));

            // Resolve the EncodeTypes enum value from the provided symbology name
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Generate and save the barcode image
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Example of setting a simple parameter: font size
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    // Very small service container for registering and resolving dependencies
    public class ServiceProvider
    {
        private readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        // Register a concrete implementation for an interface
        public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface, new()
        {
            _registrations[typeof(TInterface)] = () => new TImplementation();
        }

        // Resolve an instance of the requested service
        public TInterface Resolve<TInterface>()
        {
            if (_registrations.TryGetValue(typeof(TInterface), out var factory))
                return (TInterface)factory();

            throw new InvalidOperationException($"Service of type {typeof(TInterface).FullName} is not registered.");
        }
    }

    /// <summary>
    /// Entry point of the application demonstrating DI with a barcode generation service.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the service provider, resolves the barcode service, and generates a sample barcode image.
        /// </summary>
        static void Main()
        {
            // Set up the simple DI container
            var services = new ServiceProvider();
            services.Register<IBarcodeService, BarcodeService>();

            // Resolve the barcode service from the container
            var barcodeService = services.Resolve<IBarcodeService>();

            // Sample data for barcode generation
            string sampleText = "1234567890";
            string symbology = "Code128"; // matches EncodeTypes.Code128
            string outputFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

            try
            {
                // Generate the barcode and write the output path to console
                barcodeService.Generate(sampleText, symbology, outputFile);
                Console.WriteLine($"Barcode generated at: {outputFile}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during generation
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}