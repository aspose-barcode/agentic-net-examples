using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary file path
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Create a Code128 barcode with a specific XDimension (2 points)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f; // actual XDimension
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Save(tempFile);
        }

        // Read the barcode with default settings (should succeed)
        Console.WriteLine("Reading with default XDimension mode:");
        using (var readerDefault = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            foreach (BarCodeResult result in readerDefault.ReadBarCodes())
            {
                Console.WriteLine($"  Detected: {result.CodeText}");
            }
        }

        // Read the barcode with MinimalXDimension set higher than actual XDimension
        Console.WriteLine("\nReading with MinimalXDimension higher than actual XDimension:");
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            // Use MinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Set MinimalXDimension to a value larger than the generated XDimension (e.g., 5 points)
            // The property name is MinimalXDimension; if the API uses a different type, adjust accordingly.
            reader.QualitySettings.MinimalXDimension = 5;

            bool anyFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"  Detected: {result.CodeText}");
            }

            if (!anyFound)
            {
                Console.WriteLine("  No barcode detected – MinimalXDimension filtered out a valid barcode.");
            }
        }

        // Clean up temporary file
        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
        }
    }
}