// Title: Diagnostic barcode size logging with minimal X-dimension mode
// Description: Demonstrates generating a Code128 barcode, then reading it with UseMinimalXDimension enabled, logging each barcode element's size.
// Prompt: Create a diagnostic mode that logs each barcode element size when UseMinimalXDimension is active.
// Tags: barcode, code128, minimalxdimension, diagnostics, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, reads it using minimal X‑dimension detection,
/// and logs the size of each detected barcode element.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, then reads it back with diagnostic logging.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        const string barcodePath = "sample_barcode.png";

        // -----------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to a file
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional visual settings: black bars on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the specified path
            generator.Save(barcodePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // -----------------------------------------------------------------
        // Read the barcode using the UseMinimalXDimension mode
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Activate the minimal X-dimension detection mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Define the minimal X-dimension size (in pixels)
            reader.QualitySettings.MinimalXDimension = 2f;

            // Perform the recognition and obtain results
            var results = reader.ReadBarCodes();

            // Log whether the minimal X-dimension mode is active
            bool isMinimalActive = reader.QualitySettings.XDimension == XDimensionMode.UseMinimalXDimension;
            Console.WriteLine($"UseMinimalXDimension active: {isMinimalActive}");

            // Iterate over each detected barcode and log its size and content
            foreach (var result in results)
            {
                var rect = result.Region.Rectangle;
                double width = rect.Width;
                double height = rect.Height;

                Console.WriteLine($"Detected barcode:");
                Console.WriteLine($"  Type    : {result.CodeTypeName}");
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Size    : Width = {width}, Height = {height}");
            }
        }
    }
}