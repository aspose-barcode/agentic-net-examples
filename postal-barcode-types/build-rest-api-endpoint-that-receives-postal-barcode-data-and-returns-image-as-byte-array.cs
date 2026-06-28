using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of a Postnet postal barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a postal barcode image (Postnet) and returns it as a byte array in PNG format.
    /// </summary>
    /// <param name="codeText">The numeric code to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    static byte[] GeneratePostalBarcode(string codeText)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        // Create a barcode generator for Postnet encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Optional: configure barcode appearance if needed
            // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image bytes
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the application. Generates a sample Postnet barcode and outputs it as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Sample postal code data for Postnet barcode
        string sampleCode = "12345";

        try
        {
            // Generate barcode bytes
            byte[] barcodeBytes = GeneratePostalBarcode(sampleCode);

            // Convert the image bytes to a Base64 string for demonstration
            string base64 = Convert.ToBase64String(barcodeBytes);
            Console.WriteLine("Generated barcode (Base64 PNG):");
            Console.WriteLine(base64);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}