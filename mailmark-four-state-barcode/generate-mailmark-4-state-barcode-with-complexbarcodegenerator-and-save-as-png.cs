using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "mailmark4state.png";

        // Create and populate MailmarkCodetext for 4‑state barcode
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state format
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // class as string
        mailmark.SupplychainID = 384224;         // supply chain identifier
        mailmark.ItemID = 16563762;              // item identifier
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // valid postcode+DP

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Save to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Write the PNG bytes to the output file
                File.WriteAllBytes(outputPath, ms.ToArray());
            }
        }

        Console.WriteLine($"Mailmark 4‑state barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}