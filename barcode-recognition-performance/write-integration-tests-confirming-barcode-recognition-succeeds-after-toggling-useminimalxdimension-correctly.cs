// Title: Barcode generation and recognition with XDimension mode testing
// Description: This example generates a Code128 barcode image, then reads it using different XDimension modes, including UseMinimalXDimension, to verify successful recognition.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to create barcodes, BarCodeReader with QualitySettings.XDimension to control X-dimension handling, and XDimensionMode enumeration. Typical scenarios include testing barcode readability under varying printing resolutions or minimal module widths. Developers often need to toggle UseMinimalXDimension and set MinimalXDimension to ensure reliable scanning.
// Prompt: Write integration tests confirming barcode recognition succeeds after toggling UseMinimalXDimension correctly.
// Tags: barcode, code128, generation, recognition, xdimension, minimalxdimension, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and testing recognition across XDimension modes,
/// including UseMinimalXDimension, to ensure successful barcode scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, runs recognition tests for each XDimension mode,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for test files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define barcode parameters
        string barcodeText = "AsposeTest123";
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Generate a Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Define the XDimension modes to test
        var modes = new (string Name, XDimensionMode Mode)[]
        {
            ("Normal", XDimensionMode.Normal),
            ("Small", XDimensionMode.Small),
            ("UseMinimalXDimension", XDimensionMode.UseMinimalXDimension)
        };

        // Iterate over each XDimension mode and perform recognition
        foreach (var (name, mode) in modes)
        {
            Console.WriteLine($"Testing XDimension mode: {name}");

            // Create a reader configured for Code128 decoding
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Apply the current XDimension mode
                reader.QualitySettings.XDimension = mode;

                // When using UseMinimalXDimension, also set MinimalXDimension
                if (mode == XDimensionMode.UseMinimalXDimension)
                {
                    reader.QualitySettings.MinimalXDimension = 1f;
                }

                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // Determine if recognition succeeded
                bool success = results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                Console.WriteLine($"Barcodes read: {results?.Length ?? 0}");
                Console.WriteLine($"Success: {(success ? "Yes" : "No")}");

                // Output each recognized barcode's type and text
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }

            Console.WriteLine();
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }
}