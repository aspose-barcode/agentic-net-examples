// Title: Demonstrates setting and logging XDimension and MinimalXDimension for Code128 barcodes
// Description: Shows how to generate Code128 barcodes with specific XDimension values, save them as PNG, and recognize them using a defined MinimalXDimension, logging both parameters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It illustrates the use of BarcodeGenerator for creating barcodes with custom dimensions and BarCodeReader with QualitySettings to control recognition sensitivity. Developers often need to fine‑tune XDimension and MinimalXDimension to ensure reliable scanning across different image resolutions and printing conditions.
// Prompt: Implement a feature that logs the exact XDimension and MinimalXDimension values used for each image.
// Tags: code128, barcode generation, barcode recognition, xdimension, minimalxdimension, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Code128 barcodes with custom XDimension values,
/// saves them as PNG images, and reads them back using a specified MinimalXDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates barcodes, logs dimension settings,
    /// and attempts to decode each image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXDimDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define a set of XDimension values (in points) to be applied during generation
        float[] xDimensions = new float[] { 2f, 3f, 4f };
        List<string> generatedFiles = new List<string>();

        // Iterate over each XDimension value, generate a barcode, and read it back
        for (int i = 0; i < xDimensions.Length; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");

            // ---------- Barcode Generation ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the specific XDimension for this barcode
                generator.Parameters.Barcode.XDimension.Point = xDimensions[i];
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            generatedFiles.Add(filePath);
            Console.WriteLine($"Generated barcode '{codeText}' with XDimension = {xDimensions[i]} point(s). Saved to: {filePath}");

            // ---------- Barcode Recognition ----------
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Define MinimalXDimension for recognition (example value)
                float minimalXDim = 1f;
                reader.QualitySettings.MinimalXDimension = minimalXDim;

                Console.WriteLine($"Set MinimalXDimension for recognition to {minimalXDim} point(s).");

                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected (this may happen if the image does not contain a recognizable barcode).");
                }
            }

            Console.WriteLine(new string('-', 60));
        }

        // Optional cleanup: delete generated files and temporary folder
        // foreach (var file in generatedFiles) File.Delete(file);
        // Directory.Delete(tempFolder);
    }
}