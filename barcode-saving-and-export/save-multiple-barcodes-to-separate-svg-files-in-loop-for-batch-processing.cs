// Title: Batch generate multiple barcodes and save each as an SVG file
// Description: Demonstrates how to generate several barcodes of different symbologies in a loop and save each one to a separate SVG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and BarCodeImageFormat classes for batch barcode creation. Typical scenarios include generating large sets of barcodes for inventory, shipping labels, or QR codes for marketing materials. Developers often need to automate barcode production and export them to vector formats like SVG for high‑quality rendering.
// Prompt: Save multiple barcodes to separate SVG files in a loop for batch processing.
// Tags: barcode, symbology, batch processing, svg, aspose.barcode, generation, encodetypes, barcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a collection of barcodes and saves each as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcodes for a predefined list of symbologies and writes each to a separate SVG file in a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated SVG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define sample data: a list of (symbology name, code text) pairs
        var barcodes = new List<(string Symbology, string CodeText)>
        {
            ("Code128", "ABC123"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DM12345"),
            ("Pdf417", "PDF417 Sample"),
            ("Aztec", "AztecCode")
        };

        int index = 1;

        // Iterate over each barcode definition, generate the barcode, and save it as SVG
        foreach (var (symbologyName, codeText) in barcodes)
        {
            // Resolve the symbology name to a BaseEncodeType using reflection
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symbologyName}");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string fileName = $"barcode_{index}_{symbologyName}.svg";
            string filePath = Path.Combine(outputFolder, fileName);

            // Generate and save the barcode as an SVG file
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                try
                {
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved {filePath}");
                }
                catch (Exception ex)
                {
                    // Evaluation license may restrict SVG export for some symbologies
                    Console.WriteLine($"Failed to save {filePath}: {ex.Message}");
                }
            }

            index++;
        }

        Console.WriteLine("Batch processing completed.");
    }
}