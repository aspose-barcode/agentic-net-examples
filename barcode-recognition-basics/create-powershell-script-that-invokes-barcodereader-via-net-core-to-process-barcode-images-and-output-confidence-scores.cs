// Title: Barcode Confidence Score Demo
// Description: Demonstrates using Aspose.BarCode to read barcodes from images and display confidence and reading quality scores.
// Prompt: Create a PowerShell script that invokes BarCodeReader via .NET Core to process barcode images and output confidence scores.
// Tags: barcode symbology, reading, confidence, console, aspnet

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeConfidenceDemo
{
    /// <summary>
    /// Entry point for the barcode confidence demonstration application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method processes command‑line arguments, generates a sample barcode if none are provided,
        /// reads each image, and outputs barcode type, text, confidence, and reading quality.
        /// </summary>
        /// <param name="args">Array of image file paths supplied via the command line.</param>
        static void Main(string[] args)
        {
            // Collect image file paths from command‑line arguments.
            // If none are provided, generate a sample barcode image to demonstrate the workflow.
            var imagePaths = new List<string>();

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        imagePaths.Add(arg);
                    }
                }
            }
            else
            {
                // No input files – create a temporary sample barcode image (Code128).
                const string sampleFile = "sample_barcode.png";
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    // Save the generated barcode to a PNG file.
                    generator.Save(sampleFile);
                }

                imagePaths.Add(sampleFile);
                Console.WriteLine($"No input files supplied. Generated sample image: {sampleFile}");
            }

            // Process each image file.
            foreach (var imagePath in imagePaths)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"Warning: File not found – {imagePath}");
                    continue;
                }

                // Initialize the reader for all supported symbologies.
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Use the default NormalQuality preset.
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Read all barcodes found in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output barcode details, including confidence and reading quality.
                        Console.WriteLine($"File: {imagePath}");
                        Console.WriteLine($"  Type            : {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText        : {result.CodeText}");
                        Console.WriteLine($"  Confidence      : {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality  : {result.ReadingQuality}");
                        Console.WriteLine();
                    }
                }
            }

            // Program completes automatically; no user interaction required.
        }
    }
}