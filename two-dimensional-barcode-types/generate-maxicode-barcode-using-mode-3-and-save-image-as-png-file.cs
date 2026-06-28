using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a MaxiCode (Mode 3) barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path where the generated PNG will be saved.
        string outputPath = "maxicode_mode3.png";

        // Create a MaxiCode codetext object for Mode 3 and populate its required fields.
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",   // 6 alphanumeric characters required for Mode 3
            CountryCode = 56,       // Example country code (numeric)
            ServiceCategory = 999   // Example service category (numeric)
        };

        // Create a standard second message and assign it to the codetext.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Use ComplexBarcodeGenerator to generate the barcode image from the codetext.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Generate the barcode image; the returned Bitmap implements IDisposable.
            using (var bitmap = complexGenerator.GenerateBarCodeImage())
            {
                // Save the bitmap as a PNG file using Aspose.Drawing.Imaging.ImageFormat.
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"MaxiCode (Mode 3) barcode saved to: {outputPath}");
    }
}