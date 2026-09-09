// Title: DataMatrix Rotation Recognition Test
// Description: Generates DataMatrix barcodes rotated at 0°, 45°, and 90°, then evaluates recognition success for each angle.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create rotated DataMatrix symbols and BarCodeReader to decode them. Developers often need to test barcode readability under various orientations, especially for automated scanning solutions, and this snippet shows how to measure success rates using key API classes like BarcodeGenerator, BarCodeReader, and related parameters.
// Prompt: Evaluate recognition success rate for rotated DataMatrix codes at 0°, 45°, and 90° angles.
// Tags: datamatrix, rotation, recognition, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating rotated DataMatrix barcodes and measuring recognition success rates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates rotated barcodes, reads them, and reports success statistics.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixRotationTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define rotation angles (as text for filenames and as float values for the generator).
        string[] anglesText = { "0", "45", "90" };
        float[] angles = { 0f, 45f, 90f };
        string barcodeText = "Sample123";
        int successCount = 0;

        // Iterate over each angle, generate a barcode, and attempt to read it back.
        for (int i = 0; i < angles.Length; i++)
        {
            // Build the file path for the current angle's image.
            string filePath = Path.Combine(tempFolder, $"DataMatrix_{anglesText[i]}.png");

            // Generate a DataMatrix barcode with the specified rotation.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, barcodeText))
            {
                generator.Parameters.RotationAngle = angles[i];
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the image file was created successfully.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            bool success = false;

            // Attempt to read the barcode from the generated image.
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.DataMatrix))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    success = true;
                }
            }

            // Output the result for the current angle.
            Console.WriteLine($"Angle {anglesText[i]}°: {(success ? "Success" : "Failure")}");
            if (success) successCount++;
        }

        // Calculate and display the overall recognition success rate.
        double successRate = (double)successCount / angles.Length * 100.0;
        Console.WriteLine($"Recognition success rate: {successRate}% ({successCount}/{angles.Length})");
    }
}