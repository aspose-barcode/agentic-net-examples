using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

namespace ProcessorSettingsDemo
{
    // Wrapper class to encapsulate ProcessorSettings configuration
    public static class ProcessorSettingsWrapper
    {
        // Enable usage of all processor cores
        public static void EnableAllCores()
        {
            BarCodeReader.ProcessorSettings.UseAllCores = true;
        }

        // Configure to use a specific number of cores
        public static void UseSpecificCores(int coreCount)
        {
            if (coreCount < 1)
                throw new ArgumentOutOfRangeException(nameof(coreCount), "Core count must be at least 1.");

            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = coreCount;
        }

        // Set the maximum number of additional threads allowed for processing
        public static void SetMaxAdditionalThreads(int maxThreads)
        {
            if (maxThreads < 0)
                throw new ArgumentOutOfRangeException(nameof(maxThreads), "Maximum threads cannot be negative.");

            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxThreads;
        }
    }

    class Program
    {
        static void Main()
        {
            // Configure processor settings using the wrapper
            int halfCores = Math.Max(1, Environment.ProcessorCount / 2);
            ProcessorSettingsWrapper.UseSpecificCores(halfCores);
            ProcessorSettingsWrapper.SetMaxAdditionalThreads(Environment.ProcessorCount * 2);

            // Define a temporary file path for the generated barcode image
            string barcodePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_barcode.png");

            // Generate a simple Code128 barcode and save it to the file
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
            {
                generator.Save(barcodePath);
            }

            // Verify that the barcode image was created
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Failed to create barcode image.");
                return;
            }

            // Read the barcode using BarCodeReader with the configured processor settings
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }

            // Clean up the temporary barcode image
            try
            {
                File.Delete(barcodePath);
            }
            catch
            {
                // Ignored - cleanup failure should not affect program flow
            }
        }
    }
}