using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample usage: generate a DataMatrix barcode and display the byte array length.
        byte[] barcodeBytes = GenerateDataMatrix("HelloWorld");
        Console.WriteLine($"Generated DataMatrix barcode byte array length: {barcodeBytes.Length}");
    }

    /// <summary>
    /// Generates a DataMatrix barcode image in PNG format and returns it as a byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image of the barcode.</returns>
    static byte[] GenerateDataMatrix(string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

        // Create a barcode generator for DataMatrix with the provided code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optional: ensure the version is set to automatic selection.
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.Auto;

            // Save the generated barcode to a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}