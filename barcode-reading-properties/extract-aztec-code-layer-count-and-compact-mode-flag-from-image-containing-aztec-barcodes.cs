// Title: Extract Aztec Code layer count and compact mode flag
// Description: Demonstrates how to read an image with Aztec barcodes and retrieve the layer count and compact mode flag using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on Aztec symbology. It shows how to use BarCodeReader with DecodeType.Aztec, access extended Aztec parameters via the AztecExtendedParameters class, and handle property availability via reflection. Developers often need to extract detailed Aztec metadata such as layer count and compact mode for validation or analytics.
// Prompt: Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcodes.
// Tags: aztec, barcode, extraction, layer count, compact mode, aspose.barcode, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates extraction of Aztec barcode layer count and compact mode flag from an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the specified image, detects Aztec barcodes, and prints their metadata.
    /// </summary>
    static void Main()
    {
        // Path to the image containing Aztec barcode(s)
        string imagePath = "aztec.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {Path.GetFullPath(imagePath)}");
            return;
        }

        // Initialize a BarCodeReader configured for Aztec symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;

                // Retrieve Aztec‑specific extended parameters from the result
                AztecExtendedParameters aztecParams = result.Extended.Aztec;

                // ----- Extract layer count (if the property exists) -----
                int layersCount = 0;
                bool hasLayers = false;
                var layersProp = typeof(AztecExtendedParameters).GetProperty("LayersCount");
                if (layersProp != null && layersProp.PropertyType == typeof(int))
                {
                    layersCount = (int)layersProp.GetValue(aztecParams);
                    hasLayers = true;
                }

                // ----- Extract compact mode flag (if the property exists) -----
                bool isCompact = false;
                bool hasCompact = false;
                var compactProp = typeof(AztecExtendedParameters).GetProperty("IsCompact");
                if (compactProp != null && compactProp.PropertyType == typeof(bool))
                {
                    isCompact = (bool)compactProp.GetValue(aztecParams);
                    hasCompact = true;
                }

                // Output basic barcode information
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");

                // Output extracted Aztec‑specific metadata
                if (hasLayers)
                    Console.WriteLine($"Layers Count: {layersCount}");
                else
                    Console.WriteLine("Layers Count: (property not available)");

                if (hasCompact)
                    Console.WriteLine($"Compact Mode: {isCompact}");
                else
                    Console.WriteLine("Compact Mode: (property not available)");

                Console.WriteLine(new string('-', 40));
            }

            // Inform the user if no Aztec barcodes were found
            if (!anyFound)
            {
                Console.WriteLine("No Aztec barcodes were detected in the image.");
            }
        }
    }
}