using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode and outputting its Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, encodes it as PNG, and writes the Base64 string to console.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 text, including Application Identifier (01) for GTIN.
        string codeText = "(01)12345678901231";

        // Specify the encoding type for GS1 Code 128.
        BaseEncodeType encodeType = EncodeTypes.GS1Code128;

        // Initialize the barcode generator with the chosen type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Ensure the checksum is always displayed (required for GS1 Code 128).
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Create a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Retrieve the image bytes from the memory stream.
                byte[] barcodeBytes = ms.ToArray();

                // Convert the image bytes to a Base64 string to simulate an API response.
                string base64 = Convert.ToBase64String(barcodeBytes);

                // Output the Base64 string to the console.
                Console.WriteLine(base64);
            }
        }
    }
}