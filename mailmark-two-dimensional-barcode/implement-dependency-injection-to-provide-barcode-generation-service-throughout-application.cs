// Title: Dependency Injection Example for Aspose.BarCode Generation
// Description: Demonstrates how to use a simple DI container to inject a barcode generation service and create a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. It illustrates a common scenario where developers need to decouple barcode creation logic from application code using dependency injection, enabling easier testing and maintenance. Suitable for tutorials, code samples, and quick-start guides on integrating Aspose.BarCode in .NET applications.
// Prompt: Implement dependency injection to provide a barcode generation service throughout the application.
// Tags: barcode symbology, generation, dependency injection, aspose.barcode, code128, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDiExample
{
    /// <summary>
    /// Service contract for generating barcodes.
    /// </summary>
    public interface IBarcodeService
    {
        /// <summary>
        /// Generates a barcode image from the specified text and saves it to the given path.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The file system path where the image will be saved.</param>
        void GenerateBarcode(string codeText, string outputPath);
    }

    /// <summary>
    /// Concrete implementation of <see cref="IBarcodeService"/> using Aspose.BarCode.
    /// </summary>
    public class BarcodeService : IBarcodeService
    {
        /// <inheritdoc/>
        public void GenerateBarcode(string codeText, string outputPath)
        {
            // Ensure the output directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create and configure the barcode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the barcode color to black (default is black, shown for illustration).
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the barcode image as PNG.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Minimalistic service collection for registering singleton services.
    /// </summary>
    public class ServiceCollection
    {
        private readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers a service type with its implementation as a singleton.
        /// </summary>
        /// <typeparam name="TService">The service contract type.</typeparam>
        /// <typeparam name="TImplementation">The concrete implementation type.</typeparam>
        public void AddSingleton<TService, TImplementation>()
            where TImplementation : TService
        {
            _registrations[typeof(TService)] = typeof(TImplementation);
        }

        /// <summary>
        /// Builds a <see cref="ServiceProvider"/> that can resolve the registered services.
        /// </summary>
        /// <returns>A new <see cref="ServiceProvider"/> instance.</returns>
        public ServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_registrations);
        }
    }

    /// <summary>
    /// Simple service provider that resolves singleton services using reflection.
    /// </summary>
    public class ServiceProvider
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Type> _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProvider"/> class.
        /// </summary>
        /// <param name="registrations">The service-to-implementation mappings.</param>
        public ServiceProvider(Dictionary<Type, Type> registrations)
        {
            _registrations = registrations;
        }

        /// <summary>
        /// Retrieves an instance of the requested service type.
        /// </summary>
        /// <typeparam name="T">The service contract type.</typeparam>
        /// <returns>An instance of the requested service.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the service type is not registered.</exception>
        public T GetService<T>()
        {
            Type serviceType = typeof(T);
            if (_instances.ContainsKey(serviceType))
            {
                return (T)_instances[serviceType];
            }

            if (_registrations.TryGetValue(serviceType, out Type implementationType))
            {
                // Assume a parameterless constructor for simplicity.
                object implementation = Activator.CreateInstance(implementationType);
                _instances[serviceType] = implementation;
                return (T)implementation;
            }

            throw new InvalidOperationException($"Service of type {serviceType.FullName} is not registered.");
        }
    }

    /// <summary>
    /// Application entry point demonstrating barcode generation via DI.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the DI container, resolves the barcode service, and generates a sample barcode image.
        /// </summary>
        static void Main()
        {
            // Set up the DI container.
            var services = new ServiceCollection();
            services.AddSingleton<IBarcodeService, BarcodeService>();
            var provider = services.BuildServiceProvider();

            // Resolve the barcode service.
            var barcodeService = provider.GetService<IBarcodeService>();

            // Generate a sample barcode.
            string sampleText = "1234567890";
            string outputFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
            barcodeService.GenerateBarcode(sampleText, outputFile);

            Console.WriteLine($"Barcode generated at: {outputFile}");
        }
    }
}