// Title: Generate Code 128 barcode with checksum and save as JPEG
// Description: Demonstrates how to create a Code 128 barcode, enable its checksum, and export the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BarcodeParameters, and EncodeTypes to produce barcodes. Typical scenarios include creating product labels, shipping tags, or inventory codes where a checksum ensures data integrity. Developers often need to configure barcode properties and export images in various formats, making this a common reference for barcode creation tasks.
// Prompt: Instantiate BarcodeParameters, enable checksum, generate a Code 128 barcode, and export it as JPEG.
// Tags: code128, checksum, jpeg, barcode generation, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 128 barcode with checksum enabled and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, configures checksum, and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image as a JPEG file
            generator.Save("code128.jpg");
        }
    }
}