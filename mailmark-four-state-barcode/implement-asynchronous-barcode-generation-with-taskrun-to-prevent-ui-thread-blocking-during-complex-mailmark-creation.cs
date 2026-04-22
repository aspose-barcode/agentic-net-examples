using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

class Program
{
    static async Task Main(string[] args)
    {
        // Output file path
        string outputPath = "mailmark.png";

        // Run barcode generation on a background thread
        await Task.Run(() =>
        {
            // Prepare Mailmark codetext with required values
            var mailmark = new MailmarkCodetext
            {
                // Format must be integer 4 for 4‑state Mailmark
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
            };

            // Create ComplexBarcodeGenerator with the Mailmark codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode image to a file (PNG format)
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });

        Console.WriteLine($"Mailmark barcode saved to '{Path.GetFullPath(outputPath)}'");
    }
}