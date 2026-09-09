// Title: Generate QR Code with Zero Margin and Save as JPEG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set all margins to zero, and save the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce QR Code images. Typical scenarios include creating QR codes for URLs, product information, or authentication tokens where precise layout control (e.g., zero margins) is required. Developers often need to adjust padding, module size, and output format to integrate barcodes into UI designs or print workflows.
// Prompt: Generate a QR Code barcode with margin set to zero and save as JPEG image.
// Tags: qr code, barcode generation, margin, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with zero margins and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_zero_margin.jpg");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set all padding (margins) to zero points.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Optional: define the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}