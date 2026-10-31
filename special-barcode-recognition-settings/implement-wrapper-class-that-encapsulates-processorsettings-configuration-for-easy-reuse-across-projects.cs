// Title: Configure Aspose.BarCode ProcessorSettings for single‑ and multi‑threaded recognition
// Description: Demonstrates how to set Aspose.BarCode's ProcessorSettings to control threading during barcode recognition, generating a barcode and reading it using both single‑thread and multi‑thread configurations.
// Category-Description: This example belongs to the Aspose.BarCode processing settings category, illustrating the use of BarCodeReader, BarcodeGenerator, and ProcessorSettings to optimize performance. Developers often need to adjust threading for large‑scale barcode scanning or resource‑constrained environments; this snippet shows typical configuration patterns for single‑core and multi‑core execution.
// Prompt: Implement a wrapper class that encapsulates ProcessorSettings configuration for easy reuse across projects.
// Tags: barcode, symbology, threading, processorsettings, aspose.barcode, generation, recognition, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace ProcessorSettingsDemo
{
    /// <summary>
    /// Provides static helper methods to configure <see cref="BarCodeReader.ProcessorSettings"/>
    /// for single‑threaded or multi‑threaded barcode recognition.
    /// </summary>
    public static class ProcessorSettingsWrapper
    {
        /// <summary>
        /// Configures the processor to use a single core for recognition.
        /// </summary>
        public static void ConfigureSingleThread()
        {
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }

        /// <summary>
        /// Configures the processor to use multiple cores for recognition.
        /// </summary>
        /// <param name="cores">Number of cores to utilize.</param>
        /// <param name="maxAdditionalThreads">Maximum number of additional threads allowed (optional).</param>
        public static void ConfigureMultiThread(int cores, int maxAdditionalThreads = 0)
        {
            BarCodeReader.ProcessorSettings.UseAllCores = true;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = cores;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalThreads;
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a Code128 barcode, then reads it using
        /// both single‑threaded and multi‑threaded processor settings.
        /// </summary>
        static void Main()
        {
            // Create a temporary folder for generated barcode images
            string tempFolder = Path.Combine(Path.GetTempPath(), "ProcessorSettingsDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);
            string barcodePath = Path.Combine(tempFolder, "sample.png");

            // Generate a simple Code128 barcode and save it as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE123"))
            {
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // Verify that the barcode image was successfully created
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Failed to generate barcode image.");
                return;
            }

            // -------------------- Single‑thread recognition --------------------
            ProcessorSettingsWrapper.ConfigureSingleThread();
            Console.WriteLine("Single‑thread recognition:");
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                Console.WriteLine($"Found {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms.");
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }

            // -------------------- Multi‑thread recognition (2 cores) --------------------
            ProcessorSettingsWrapper.ConfigureMultiThread(2);
            Console.WriteLine("\nMulti‑thread recognition (2 cores):");
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                Console.WriteLine($"Found {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms.");
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }

            // Clean up temporary files
            try
            {
                File.Delete(barcodePath);
                Directory.Delete(tempFolder);
            }
            catch
            {
                // Ignored – cleanup failure should not affect program exit
            }
        }
    }
}