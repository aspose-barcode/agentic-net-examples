using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to a PNG image,
/// and outputting the image bytes as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a memory stream, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure the image resolution (dots per inch). Optional but improves quality.
            generator.Parameters.Resolution = 300f;

            // Create a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array for further processing or transmission.
                byte[] barcodeBytes = memoryStream.ToArray();

                // Encode the byte array as a Base64 string for easy display or transport.
                string base64 = Convert.ToBase64String(barcodeBytes);

                // Output the Base64 string to the console.
                Console.WriteLine(base64);
            }
        }
    }
}