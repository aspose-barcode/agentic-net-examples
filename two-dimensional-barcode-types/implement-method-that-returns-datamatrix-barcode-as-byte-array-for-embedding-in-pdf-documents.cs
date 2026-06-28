using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a DataMatrix barcode using Aspose.BarCode and outputs it as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode image and returns it as a byte array in PNG format.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    static byte[] GenerateDataMatrixBarcode(string codeText)
    {
        // Initialize a barcode generator for the DataMatrix symbology with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optionally select a specific DataMatrix version (20x20 modules) for a square shape.
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Create a memory stream to hold the generated image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Return the image data as a byte array.
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the application. Generates a sample DataMatrix barcode and writes its Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // Define sample text to encode in the barcode.
        string sampleText = "Hello, Aspose!";

        // Generate the barcode image as a byte array.
        byte[] barcodeBytes = GenerateDataMatrixBarcode(sampleText);

        // Convert the byte array to a Base64 string for easy embedding in documents or web pages.
        string base64 = Convert.ToBase64String(barcodeBytes);

        // Output the Base64 string to the console.
        Console.WriteLine("DataMatrix barcode (Base64 PNG):");
        Console.WriteLine(base64);
    }
}