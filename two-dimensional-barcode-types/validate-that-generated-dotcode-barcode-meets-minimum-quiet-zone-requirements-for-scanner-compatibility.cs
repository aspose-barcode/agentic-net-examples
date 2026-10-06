// Title: Validate DotCode Barcode Quiet Zone Padding
// Description: Demonstrates generating a DotCode barcode with varying quiet zone padding and verifies scanner compatibility by attempting recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a DotCode barcode and BarCodeReader to decode it. Developers often need to ensure that the quiet zone around a barcode meets minimum requirements for reliable scanning; this snippet illustrates how to programmatically adjust padding and validate the result.
// Prompt: Validate that generated DotCode barcode meets minimum quiet zone requirements for scanner compatibility.
// Tags: dotcode, quiet zone, barcode generation, barcode recognition, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DotCode barcode with incremental quiet zone padding
/// and validates that the barcode can be recognized, ensuring scanner compatibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode, tests recognition, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeQuietZone_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Full path for the generated PNG barcode
        string barcodePath = Path.Combine(tempFolder, "dotcode.png");

        // Start with a minimal quiet zone (padding) and define the maximum to try
        int paddingPoints = 2;
        const int maxPaddingPoints = 10;
        bool recognized = false;

        // Incrementally increase quiet zone until the barcode is recognized or the limit is reached
        while (paddingPoints <= maxPaddingPoints && !recognized)
        {
            // Generate DotCode barcode with the current padding (quiet zone) settings
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 10f;

                float pad = paddingPoints;
                generator.Parameters.Barcode.Padding.Left.Point = pad;
                generator.Parameters.Barcode.Padding.Right.Point = pad;
                generator.Parameters.Barcode.Padding.Top.Point = pad;
                generator.Parameters.Barcode.Padding.Bottom.Point = pad;

                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // Attempt to read the generated barcode using the DotCode decoder
            using (var reader = new BarCodeReader(barcodePath, DecodeType.DotCode))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                {
                    recognized = true;
                    Console.WriteLine($"Barcode recognized with quiet zone padding of {paddingPoints} points.");
                }
                else
                {
                    Console.WriteLine($"Failed to recognize barcode with padding {paddingPoints} points.");
                }
            }

            // If not recognized, increase the quiet zone and retry
            if (!recognized)
            {
                paddingPoints += 2;
            }
        }

        // Report final outcome if barcode could not be recognized within the maximum padding
        if (!recognized)
        {
            Console.WriteLine("Barcode could not be recognized even with maximum quiet zone padding.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
            {
                File.Delete(barcodePath);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}