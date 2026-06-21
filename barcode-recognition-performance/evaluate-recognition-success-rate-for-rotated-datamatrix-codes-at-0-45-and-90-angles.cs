using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of rotated DataMatrix barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates DataMatrix barcodes at various rotation angles,
    /// attempts to read them back, and reports the success rate.
    /// </summary>
    static void Main()
    {
        // Sample DataMatrix text to encode
        const string codeText = "AsposeDataMatrixTest";

        // Rotation angles (in degrees) to test
        float[] angles = new float[] { 0f, 45f, 90f };

        // Counters for total tests and successful reads
        int totalTests = angles.Length;
        int successfulReads = 0;

        // Iterate over each rotation angle
        foreach (float angle in angles)
        {
            // Build a temporary file path for the generated barcode image
            string tempFile = Path.Combine(Path.GetTempPath(), $"DataMatrix_{angle}.png");

            // -------------------------------------------------
            // Generate a rotated DataMatrix barcode and save it
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Apply the desired rotation angle
                generator.Parameters.RotationAngle = angle;

                // Save the barcode image to the temporary file
                generator.Save(tempFile);
            }

            // Verify that the image file was successfully created
            if (!File.Exists(tempFile))
            {
                Console.WriteLine($"Failed to create barcode image for angle {angle}°.");
                continue;
            }

            // -------------------------------------------------
            // Attempt to read the barcode from the generated image
            // -------------------------------------------------
            using (var reader = new BarCodeReader(tempFile, DecodeType.DataMatrix))
            {
                bool readSuccess = false;

                // Iterate through all detected barcodes (should be only one)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Check if the decoded text matches the original text
                    if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == codeText)
                    {
                        readSuccess = true;
                        break;
                    }
                }

                // Update counters and output result for the current angle
                if (readSuccess)
                {
                    successfulReads++;
                    Console.WriteLine($"Angle {angle}°: SUCCESS");
                }
                else
                {
                    Console.WriteLine($"Angle {angle}°: FAILURE");
                }
            }

            // -------------------------------------------------
            // Clean up: delete the temporary barcode image file
            // -------------------------------------------------
            try
            {
                File.Delete(tempFile);
            }
            catch
            {
                // Suppress any exceptions during cleanup
            }
        }

        // Calculate the overall success rate and display it
        double successRate = totalTests > 0 ? (double)successfulReads / totalTests * 100.0 : 0.0;
        Console.WriteLine($"Recognition success rate: {successRate:F2}% ({successfulReads}/{totalTests})");
    }
}