using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Simulated HTTP response stream
        using (var responseStream = new MemoryStream())
        {
            // Prepare complex codetext for HIBC LIC QR with secondary data
            var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext();
            complexCodetext.BarcodeType = EncodeTypes.HIBCQRLIC;
            complexCodetext.LinkCharacter = 'L';
            complexCodetext.Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                ExpiryDate = DateTime.Now,
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 30,
                DateOfManufacture = DateTime.Now
            };

            // Generate the barcode image and write it to the response stream
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Set QR error correction level (optional)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save directly to the simulated HTTP response stream as PNG
                generator.Save(responseStream, BarCodeImageFormat.Png);
            }

            // At this point, responseStream contains the PNG image.
            // For demonstration, output the size of the generated image.
            Console.WriteLine($"Generated HIBC QR LIC barcode image size: {responseStream.Length} bytes");
        }
    }
}