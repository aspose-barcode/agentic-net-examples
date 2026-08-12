// Title: Dependency Injection for Aspose.BarCode Generator in ASP.NET Core
// Description: Demonstrates how to register and resolve a barcode generator factory using ASP.NET Core's built‑in DI container, then generate and save a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing typical use of the BarcodeGenerator, EncodeTypes, and related parameter classes. Developers often need to create barcodes dynamically in web or service applications; using dependency injection promotes testability and decouples barcode creation logic from consuming services. The pattern shown is common for ASP.NET Core projects that require flexible barcode generation.
// Prompt: Provide sample code that uses dependency injection to supply barcode generator instances in ASP.NET Core.
// Tags: barcode, symbology, generation, aspnet core, dependency injection, aspose.barcode, encode types, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Microsoft.Extensions.DependencyInjection;

namespace AsposeBarcodeDiDemo
{
    /// <summary>
    /// Factory interface for creating <see cref="BarcodeGenerator"/> instances.
    /// </summary>
    public interface IBarcodeGeneratorFactory
    {
        /// <summary>
        /// Creates a new <see cref="BarcodeGenerator"/> for the specified symbology and code text.
        /// </summary>
        /// <param name="symbologyName">The name of the symbology (must match a field in <see cref="EncodeTypes"/>).</param>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>A configured <see cref="BarcodeGenerator"/> instance.</returns>
        BarcodeGenerator Create(string symbologyName, string codeText);
    }

    /// <summary>
    /// Implementation of <see cref="IBarcodeGeneratorFactory"/> that resolves symbology via reflection.
    /// </summary>
    public class BarcodeGeneratorFactory : IBarcodeGeneratorFactory
    {
        /// <inheritdoc/>
        public BarcodeGenerator Create(string symbologyName, string codeText)
        {
            if (string.IsNullOrWhiteSpace(symbologyName))
                throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));

            // Resolve the EncodeTypes field that matches the provided symbology name.
            var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

            var encodeType = (BaseEncodeType)field.GetValue(null);
            // Instantiate the generator with the resolved encode type and the supplied code text.
            return new BarcodeGenerator(encodeType, codeText);
        }
    }

    /// <summary>
    /// Service that uses <see cref="IBarcodeGeneratorFactory"/> to generate and persist barcode images.
    /// </summary>
    public class BarcodeService
    {
        private readonly IBarcodeGeneratorFactory _factory;

        /// <summary>
        /// Initializes a new instance of <see cref="BarcodeService"/>.
        /// </summary>
        /// <param name="factory">Factory used to create barcode generators.</param>
        public BarcodeService(IBarcodeGeneratorFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Generates a barcode using the specified symbology and text, then saves it to the given path.
        /// </summary>
        /// <param name="symbology">Symbology name (must match a field in <see cref="EncodeTypes"/>).</param>
        /// <param name="text">Text to encode.</param>
        /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
        public void GenerateAndSave(string symbology, string text, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Ensure the target directory exists.
            var directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Create the generator, optionally adjust parameters, and save the image.
            using (var generator = _factory.Create(symbology, text))
            {
                // Example of setting a barcode parameter (optional).
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(outputPath);
            }
        }
    }

    /// <summary>
    /// Entry point for the console application demonstrating DI with Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the DI container, resolves <see cref="BarcodeService"/>, and generates a sample barcode.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Build the service collection and register dependencies.
            var services = new ServiceCollection();
            services.AddTransient<IBarcodeGeneratorFactory, BarcodeGeneratorFactory>();
            services.AddTransient<BarcodeService>();
            var provider = services.BuildServiceProvider();

            // Resolve the BarcodeService from the DI container.
            var barcodeService = provider.GetRequiredService<BarcodeService>();

            // Sample input data.
            string symbology = "Code128"; // Must match a field name in EncodeTypes.
            string codeText = "123ABC";
            string outputFile = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDiDemo", "code128.png");

            // Generate the barcode and save it to the specified location.
            barcodeService.GenerateAndSave(symbology, codeText, outputFile);

            Console.WriteLine($"Barcode saved to: {outputFile}");
        }
    }
}