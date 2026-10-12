// Title: Determine Barcode Orientation Angle in a BMP Image
// Description: This example generates a rotated Code128 barcode, saves it as a BMP file, and then detects the barcode to retrieve its orientation angle.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflow focusing on barcode orientation detection. It uses BarcodeGenerator for creating barcodes, BarCodeReader for scanning, and accesses the Region.Angle property to obtain rotation. Useful for developers needing to verify barcode placement, correct rotation, or process images with rotated barcodes.
// Prompt: Determine barcode orientation angle for each detected barcode in a BMP image.
// Tags: code128, barcode orientation, bmp, generation, recognition, aspose.barcode, rotationangle, detection

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeOrientationDemo
{
    /// <summary>
    /// Demonstrates how to generate a rotated barcode, save it as a BMP image,
    /// and then read the image to obtain the orientation angle of each detected barcode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a barcode, reads it back, and prints orientation information.
        /// </summary>
        static void Main()
        {
            // Create a unique temporary directory to store the sample image
            string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeOrientationDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
            string imagePath = Path.Combine(tempDir, "sample.bmp");

            // Generate a rotated Code128 barcode and save it as a BMP file
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                generator.Parameters.RotationAngle = 45; // Rotate the barcode by 45 degrees
                generator.Save(imagePath, BarCodeImageFormat.Bmp);
            }

            // Verify that the image was created successfully
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Failed to create barcode image.");
                return;
            }

            // Read the barcode from the BMP image and output its orientation angle
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"Orientation Angle: {result.Region.Angle} degrees");
                    }
                }
            }

            // Cleanup temporary files and directory
            try
            {
                File.Delete(imagePath);
                Directory.Delete(tempDir);
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }
}