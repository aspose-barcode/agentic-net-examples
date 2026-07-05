// Title: DataMatrix rotation recognition test
// Description: Demonstrates generating DataMatrix barcodes at different rotation angles and measuring recognition success rate.
// Prompt: Evaluate recognition success rate for rotated DataMatrix codes at 0°, 45°, and 90° angles.
// Tags: datamatrix, rotation, recognition, success-rate, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates DataMatrix barcodes at various rotation angles,
/// attempts to recognize them, and reports the overall success rate.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the rotation test and prints results.
    /// </summary>
    static void Main()
    {
        // Text to encode in the DataMatrix barcode
        const string codeText = "TestDataMatrix12345";

        // Rotation angles (in degrees) to evaluate
        float[] angles = { 0f, 45f, 90f };
        int successful = 0;

        // Iterate over each angle, generate, recognize, and log the outcome
        foreach (float angle in angles)
        {
            // Create a barcode generator for DataMatrix with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Apply rotation to the generated barcode image
                generator.Parameters.RotationAngle = angle;

                // Generate the barcode as a bitmap image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Initialize a reader to decode DataMatrix barcodes from the bitmap
                    using (var reader = new BarCodeReader(bitmap, DecodeType.DataMatrix))
                    {
                        bool recognized = false;

                        // Scan all detected barcodes in the image
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Verify that the decoded text matches the original
                            if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == codeText)
                            {
                                recognized = true;
                                break;
                            }
                        }

                        // Output the result for the current angle
                        Console.WriteLine($"Angle {angle}°: {(recognized ? "Success" : "Failure")}");
                        if (recognized) successful++;
                    }
                }
            }
        }

        // Calculate and display the overall recognition success rate
        double successRate = (double)successful / angles.Length * 100.0;
        Console.WriteLine($"Recognition success rate: {successRate:F2}%");
    }
}