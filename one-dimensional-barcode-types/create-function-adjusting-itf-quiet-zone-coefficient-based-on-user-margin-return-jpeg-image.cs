using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating an ITF14 barcode with a custom quiet zone margin
/// and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the data to encode and the desired quiet zone margin (in points).
        string codeText = "123456789012";
        float margin = 15f;

        // Generate the barcode image as a JPEG byte array.
        byte[] jpegData = GenerateItfBarcode(codeText, margin);

        // Specify the output file path.
        string outputPath = "itf_barcode.jpg";

        // Write the JPEG bytes to the file system.
        File.WriteAllBytes(outputPath, jpegData);

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }

    /// <summary>
    /// Generates an ITF14 barcode, adjusts the quiet zone coefficient based on the provided margin,
    /// and returns the barcode image as a JPEG byte array.
    /// </summary>
    /// <param name="codeText">The data to encode (must be valid for ITF14).</param>
    /// <param name="margin">Desired quiet zone margin in points.</param>
    /// <returns>JPEG image bytes of the generated barcode.</returns>
    static byte[] GenerateItfBarcode(string codeText, float margin)
    {
        // Validate input: code text must be non‑null and non‑empty.
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text cannot be null or empty.", nameof(codeText));

        // Validate input: margin must be a positive value.
        if (margin <= 0f)
            throw new ArgumentOutOfRangeException(nameof(margin), "Margin must be greater than zero.");

        // QuietZoneCoef must be at least 10 according to the API.
        // Convert the margin to an integer coefficient, rounding up.
        int quietZoneCoef = (int)Math.Ceiling(margin);
        if (quietZoneCoef < 10)
            throw new ArgumentException("Quiet zone coefficient must be at least 10.", nameof(margin));

        // Create a barcode generator for the ITF14 symbology with the supplied data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Apply the calculated quiet zone coefficient.
            generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef;

            // Use a memory stream to capture the generated JPEG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in JPEG format.
                generator.Save(ms, BarCodeImageFormat.Jpeg);

                // Return the image data as a byte array.
                return ms.ToArray();
            }
        }
    }
}