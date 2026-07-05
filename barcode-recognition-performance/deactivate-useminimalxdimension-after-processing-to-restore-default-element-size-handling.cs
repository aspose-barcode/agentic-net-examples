// Title: Demonstrate Deactivating UseMinimalXDimension After Barcode Processing
// Description: Creates a Code128 barcode, reads it with minimal X dimension mode enabled, then disables the mode to revert to default handling.
// Prompt: Deactivate UseMinimalXDimension after processing to restore default element size handling.
// Tags: barcode, code128, useminimalxdimension, xdimension, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it with minimal X dimension mode,
/// then deactivates the mode to restore default element size handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with different XDimension settings,
    /// and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Path for the temporary barcode image
        string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to a PNG file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with UseMinimalXDimension enabled
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Activate minimal X dimension mode for small barcodes
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 5f; // optional minimal size

            Console.WriteLine("Reading with UseMinimalXDimension enabled:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // ------------------------------------------------------------
            // Deactivate UseMinimalXDimension to restore default handling
            // ------------------------------------------------------------
            reader.QualitySettings.XDimension = XDimensionMode.Auto; // default mode
            Console.WriteLine("UseMinimalXDimension deactivated; XDimension mode reset to default.");

            // Optional: read again using the default XDimension mode
            Console.WriteLine("Reading again with default XDimension mode:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary image file
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}