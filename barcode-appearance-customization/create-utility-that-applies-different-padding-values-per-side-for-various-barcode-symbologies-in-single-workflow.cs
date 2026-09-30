// Title: Barcode Padding Utility Demonstration
// Description: Shows how to generate barcodes with custom padding values for each side using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating per‑side padding configuration across multiple symbologies. It uses BarcodeGenerator, EncodeTypes, and the Padding properties to control whitespace around the barcode. Developers often need to fine‑tune padding for layout or printing requirements, and this snippet provides a reusable pattern for such scenarios.
// Prompt: Create a utility that applies different padding values per side for various barcode symbologies in a single workflow.
// Tags: barcode symbology, padding, generation, aspose.barcode, png output, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodePaddingUtility
{
    /// <summary>
    /// Configuration for a single barcode generation, including symbology, encoded text, per‑side padding, and output file name.
    /// </summary>
    class BarcodeConfig
    {
        public string SymbologyName { get; set; }      // e.g., "Code128", "QR", "DataMatrix"
        public string CodeText { get; set; }           // text to encode
        public float PaddingLeft { get; set; }         // points
        public float PaddingTop { get; set; }
        public float PaddingRight { get; set; }
        public float PaddingBottom { get; set; }
        public string OutputFileName { get; set; }     // without extension
    }

    /// <summary>
    /// Demonstrates generating multiple barcodes with individual side padding values and saving them as PNG files.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Prepares output directory, defines barcode configurations, generates each barcode with specified padding, and saves the images.
        /// </summary>
        static void Main()
        {
            // Prepare output directory
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesOutput");
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Define sample configurations for different symbologies
            var configs = new List<BarcodeConfig>
            {
                new BarcodeConfig
                {
                    SymbologyName = "Code128",
                    CodeText = "ABC123",
                    PaddingLeft = 10f,
                    PaddingTop = 5f,
                    PaddingRight = 15f,
                    PaddingBottom = 5f,
                    OutputFileName = "Code128_Padding"
                },
                new BarcodeConfig
                {
                    SymbologyName = "QR",
                    CodeText = "https://example.com",
                    PaddingLeft = 2f,
                    PaddingTop = 2f,
                    PaddingRight = 2f,
                    PaddingBottom = 2f,
                    OutputFileName = "QR_Padding"
                },
                new BarcodeConfig
                {
                    SymbologyName = "DataMatrix",
                    CodeText = "DM12345",
                    PaddingLeft = 0f,
                    PaddingTop = 0f,
                    PaddingRight = 0f,
                    PaddingBottom = 0f,
                    OutputFileName = "DataMatrix_NoPadding"
                }
            };

            // Iterate over each configuration and generate the corresponding barcode
            foreach (var cfg in configs)
            {
                // Resolve symbology name to BaseEncodeType via reflection
                var field = typeof(EncodeTypes).GetField(cfg.SymbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology: {cfg.SymbologyName}. Skipping.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Create generator with resolved symbology and code text
                using (var generator = new BarcodeGenerator(encodeType, cfg.CodeText))
                {
                    // Apply individual padding values (points) to each side
                    generator.Parameters.Barcode.Padding.Left.Point = cfg.PaddingLeft;
                    generator.Parameters.Barcode.Padding.Top.Point = cfg.PaddingTop;
                    generator.Parameters.Barcode.Padding.Right.Point = cfg.PaddingRight;
                    generator.Parameters.Barcode.Padding.Bottom.Point = cfg.PaddingBottom;

                    // Optional: set a modest XDimension for visibility
                    generator.Parameters.Barcode.XDimension.Point = 1.5f;

                    // Build output file path
                    string outputPath = Path.Combine(outputDir, cfg.OutputFileName + ".png");

                    // Save barcode as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved {cfg.SymbologyName} barcode to {outputPath}");
                }
            }

            Console.WriteLine("Barcode generation completed.");
        }
    }
}