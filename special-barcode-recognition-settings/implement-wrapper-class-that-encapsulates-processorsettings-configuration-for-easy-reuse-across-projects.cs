// Title: Demonstrate configuring Aspose.BarCode ProcessorSettings via a wrapper
// Description: Shows how to encapsulate ProcessorSettings configuration in a reusable wrapper and uses it to generate and read a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode processing configuration category, illustrating the use of BarCodeReader.ProcessorSettings and related API classes such as BarcodeGenerator, BarCodeReader, and EncodeTypes. Developers often need to adjust parallel processing settings for performance optimization when handling large volumes of barcode images; this snippet provides a reusable pattern for setting such options across projects.
// Prompt: Implement a wrapper class that encapsulates ProcessorSettings configuration for easy reuse across projects.
// Tags: barcode symbology, configuration, parallelism, processor settings, aspose.barcode, generation, recognition

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeProcessorSettingsDemo
{
    /// <summary>
    /// Provides a static wrapper to configure the ProcessorSettings used by <see cref="BarCodeReader"/>.
    /// </summary>
    public static class ProcessorSettingsWrapper
    {
        /// <summary>
        /// Configures the maximum degree of parallelism for barcode processing if the underlying setting is available.
        /// </summary>
        /// <param name="maxDegreeOfParallelism">The desired maximum number of parallel threads.</param>
        public static void Configure(int maxDegreeOfParallelism)
        {
            // Retrieve the static ProcessorSettings instance from BarCodeReader
            var settings = BarCodeReader.ProcessorSettings;
            if (settings == null)
            {
                Console.WriteLine("ProcessorSettings is null; cannot configure.");
                return;
            }

            // Attempt to set a property named MaxDegreeOfParallelism via reflection
            var prop = settings.GetType().GetProperty("MaxDegreeOfParallelism");
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(settings, maxDegreeOfParallelism);
                Console.WriteLine($"ProcessorSettings: MaxDegreeOfParallelism set to {maxDegreeOfParallelism}.");
            }
            else
            {
                Console.WriteLine("ProcessorSettings does not expose a writable MaxDegreeOfParallelism property.");
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point demonstrating barcode generation, reading, and processor settings configuration.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Configure processor settings to use 2 parallel threads
            ProcessorSettingsWrapper.Configure(2);

            // Define the output path for the generated barcode image
            const string imagePath = "sample.png";

            // Generate a simple Code128 barcode and save it to the specified file
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image saved to '{imagePath}'.");
            }

            // Read the barcode back from the saved image using BarCodeReader
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Read CodeText: {result.CodeText}");
                }
            }

            // Indicate that the demo has finished executing
            Console.WriteLine("Demo completed.");
        }
    }
}