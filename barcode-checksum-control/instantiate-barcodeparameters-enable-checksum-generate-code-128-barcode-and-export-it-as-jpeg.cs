// Title: Generate Code 128 Barcode with Checksum and Save as JPEG
// Description: Demonstrates creating a Code 128 barcode, enabling checksum, and exporting it to a JPEG file using Aspose.BarCode.
// Prompt: Instantiate BarcodeParameters, enable checksum, generate a Code 128 barcode, and export it as JPEG.
// Tags: barcode, code128, checksum, jpeg, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code 128 barcode with checksum enabled
/// and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code 128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image as a JPEG file
            generator.Save("code128.jpg");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as code128.jpg");
    }
}