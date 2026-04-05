using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "123456789";

            // Enable checksum calculation and display it
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Set custom margins (padding) of 10 points on each side
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;

            // Save the barcode as a TIFF image (lossless compression)
            generator.Save("code128.tiff");
        }
    }
}