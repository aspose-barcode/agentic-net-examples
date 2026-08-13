// Title: Demonstrate toggling UseMinimalXDimension for barcode reading
// Description: Shows how to enable and then deactivate the UseMinimalXDimension setting when reading a Code128 barcode, ensuring default element size handling is restored.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and its QualitySettings to control X‑dimension handling. Developers often need to adjust minimal element sizes for small or low‑resolution barcodes and then revert to automatic sizing for subsequent operations. The snippet highlights key classes such as BarcodeGenerator, BarCodeReader, and related enums, serving as a reference for similar use‑case examples.
// Prompt: Deactivate UseMinimalXDimension after processing to restore default element size handling.
// Tags: barcode, code128, useminimalxdimension, qualitysettings, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with minimal X dimension enabled,
/// then deactivates the setting to revert to default handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with different X‑dimension settings,
    /// and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string barcodePath = "barcode.png";

        // -------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use explicit size settings (no automatic scaling)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the barcode image to the specified path
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // First read: enable UseMinimalXDimension to handle small elements
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Activate minimal X dimension mode with a custom minimal size
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 5f; // example minimal size in points

            Console.WriteLine("Reading with UseMinimalXDimension enabled:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Confidence: {result.Confidence}");
            }

            // -------------------------------------------------
            // Deactivate UseMinimalXDimension to restore default handling
            // -------------------------------------------------
            reader.QualitySettings.XDimension = XDimensionMode.Auto; // revert to default mode
            reader.QualitySettings.MinimalXDimension = 0f;           // reset minimal size

            Console.WriteLine("Reading after deactivating UseMinimalXDimension:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Confidence: {result.Confidence}");
            }
        }
    }
}