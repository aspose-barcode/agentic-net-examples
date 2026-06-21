using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes with a specific XDimension and then recognizing them,
/// logging both generation and recognition parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, saves them to temporary files, and reads them back
    /// to display recognition details.
    /// </summary>
    static void Main()
    {
        // Define a collection of barcode types and corresponding text to encode.
        var samples = new List<(BaseEncodeType EncodeType, string CodeText)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Process each sample: generate, save, and then read/recognize.
        foreach (var sample in samples)
        {
            // Build a temporary file path using the barcode type name.
            string filePath = Path.Combine(Path.GetTempPath(), $"{sample.EncodeType.TypeName}.png");

            // ---------- Barcode Generation ----------
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                // Set the XDimension (module size) explicitly to 2 points.
                generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points

                // Disable auto-sizing to preserve the explicit XDimension setting.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Save the generated barcode image to the temporary file.
                generator.Save(filePath);

                // Log generation details to the console.
                Console.WriteLine($"Generated {sample.EncodeType.TypeName} barcode at: {filePath}");
                Console.WriteLine($"  Generation XDimension: {generator.Parameters.Barcode.XDimension.Point} points");
            }

            // ---------- Barcode Recognition ----------
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Optional: configure quality settings here (e.g., deconvolution).
                // reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                // Perform the recognition operation.
                var results = reader.ReadBarCodes();

                // Retrieve and log the MinimalXDimension used by the recognizer.
                float minimalXDim = reader.QualitySettings.MinimalXDimension;
                Console.WriteLine($"  Recognition MinimalXDimension: {minimalXDim} points");

                // Output each recognized barcode's type and decoded text.
                foreach (var result in results)
                {
                    Console.WriteLine($"  Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }

            // Add a blank line for readability between samples.
            Console.WriteLine();
        }
    }
}