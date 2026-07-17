// Title: Validate Han Xin barcode quiet zone dimensions
// Description: Demonstrates generating a Han Xin barcode with explicit quiet zone padding and verifies that the barcode can be decoded, ensuring scanner reliability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to configure barcode parameters such as padding, module size, and error correction using BarcodeGenerator, and then validates the output with BarCodeReader. Developers often need to adjust quiet zones to meet scanner specifications, and this snippet illustrates the typical workflow for creating and testing barcodes in .NET applications.
// Prompt: Validate that generated Han Xin barcode meets required quiet zone dimensions for scanner reliability.
// Tags: hanxin,quietzone,padding,barcode,generation,recognition,aspose.barcode,csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Han Xin barcode with defined quiet zone padding,
/// saves it to an image file, and validates that the barcode can be decoded.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures padding,
    /// saves the image, and verifies decoding to ensure scanner reliability.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "HanXin.png";

        // Remove any existing file to avoid conflicts.
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a Han Xin barcode generator with sample code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "SampleHanXin123"))
        {
            // Set quiet zone (padding) – typical scanners require at least 10 points on each side.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set module size and error correction level.
            generator.Parameters.Barcode.XDimension.Point = 2f; // module width
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Use black bars on a white background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the Han Xin barcode image.");
            return;
        }

        // Output the configured quiet zone dimensions for verification.
        using (var verifier = new BarcodeGenerator(EncodeTypes.HanXin))
        {
            Console.WriteLine("Configured quiet zone (padding) values (points):");
            Console.WriteLine($"Left:   {verifier.Parameters.Barcode.Padding.Left.Point}");
            Console.WriteLine($"Top:    {verifier.Parameters.Barcode.Padding.Top.Point}");
            Console.WriteLine($"Right:  {verifier.Parameters.Barcode.Padding.Right.Point}");
            Console.WriteLine($"Bottom: {verifier.Parameters.Barcode.Padding.Bottom.Point}");
        }

        // Attempt to read the barcode to ensure scanner reliability.
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                found = true;
                break; // Stop after the first successful decode.
            }

            if (!found)
            {
                Console.WriteLine("Barcode could not be decoded – quiet zone may be insufficient.");
            }
            else
            {
                Console.WriteLine("Barcode decoded successfully – quiet zone meets requirements.");
            }
        }
    }
}