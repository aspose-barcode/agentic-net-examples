using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static async Task Main(string[] args)
    {
        await GenerateHibcLicAsync();
        Console.WriteLine("HIBC LIC barcode generated: hibc_lic.png");
    }

    private static async Task GenerateHibcLicAsync()
    {
        // Prepare secondary and additional data for HIBC LIC
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = 'L',
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now,
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 30,
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                DateOfManufacture = DateTime.Now
            }
        };

        // Run generation on a background thread to keep UI responsive
        await Task.Run(() =>
        {
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Save barcode image to memory stream in PNG format
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // Write the image bytes to a file
                    File.WriteAllBytes("hibc_lic.png", memoryStream.ToArray());
                }
            }
        });
    }
}