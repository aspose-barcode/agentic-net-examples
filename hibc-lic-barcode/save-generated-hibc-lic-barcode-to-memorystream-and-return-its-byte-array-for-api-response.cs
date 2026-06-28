using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC LIC barcode with secondary and additional data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, writes its byte length to the console, and disposes resources.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data for HIBC LIC barcode (lot number and serial number)
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN456"
        };

        // Configure the complex barcode text with the desired type, link character, and secondary data
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Generate the barcode and save it to a memory stream in PNG format
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        using (var memoryStream = new MemoryStream())
        {
            generator.Save(memoryStream, BarCodeImageFormat.Png);

            // Retrieve the generated barcode as a byte array
            byte[] barcodeBytes = memoryStream.ToArray();

            // Output the length of the byte array for verification purposes
            Console.WriteLine($"Generated barcode byte array length: {barcodeBytes.Length}");
        }
    }
}