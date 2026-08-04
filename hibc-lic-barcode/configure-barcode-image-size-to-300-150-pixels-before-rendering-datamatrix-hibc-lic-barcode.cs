// Title: Generate HIBC DataMatrix LIC barcode with custom image size
// Description: Demonstrates how to set a specific image size (300 × 150 pixels) and generate a DataMatrix HIBC LIC barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image dimensions, colors, and save the result as a PNG. It showcases the BarcodeGenerator class with EncodeTypes.HIBCDataMatrixLIC, a common scenario for developers needing HIBC‑compliant DataMatrix barcodes in healthcare labeling.
// Prompt: Configure barcode image size to 300 × 150 pixels before rendering a DataMatrix HIBC LIC barcode.
// Tags: barcode, datamatrix, hibc, image-size, png, generation, aspose.barcodes, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring image size and generating a HIBC DataMatrix LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Sample HIBC DataMatrix LIC codetext (adjust as needed for a valid HIBC string)
        string codeText = "A12345";

        // Initialize the barcode generator for HIBC DataMatrix LIC symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCDataMatrixLIC, codeText))
        {
            // Set the desired image size: 300 × 150 pixels
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Optional: define foreground (bar) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG file
            generator.Save("HIBCDataMatrixLIC.png");
        }

        Console.WriteLine("Barcode generated and saved as HIBCDataMatrixLIC.png");
    }
}