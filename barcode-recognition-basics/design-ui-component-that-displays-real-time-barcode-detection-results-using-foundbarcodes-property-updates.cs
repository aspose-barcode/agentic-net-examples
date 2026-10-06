// Title: Real‑time barcode detection demo using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, detecting it with BarCodeReader, and outputting detection details, simulating real‑time UI updates.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeGenerator for barcode creation and BarCodeReader for detection. Developers commonly use these APIs to integrate barcode scanning into applications, retrieve barcode type, text, and location, and handle multiple symbologies in image processing scenarios.
// Prompt: Design a UI component that displays real‑time barcode detection results using FoundBarCodes property updates.
// Tags: barcode, code128, detection, recognition, realtime, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and real‑time detection using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it, and prints detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode image path.
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample Code128 barcode and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo12345"))
        {
            // Optional visual settings.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the temporary location.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists before attempting to read.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read barcodes from the generated image using all supported symbologies.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition.
            reader.ReadBarCodes();

            // Output the number of detected barcodes.
            Console.WriteLine($"Barcodes detected: {reader.FoundCount}");

            // Iterate over the FoundBarCodes collection to display each result.
            foreach (var result in reader.FoundBarCodes)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");

                // Display the barcode region (position and size).
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                Console.WriteLine(new string('-', 40));
            }
        }

        // Clean up temporary files (optional).
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the demo.
        }
    }
}