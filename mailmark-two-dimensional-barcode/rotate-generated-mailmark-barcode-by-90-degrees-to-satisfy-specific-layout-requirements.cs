using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare Mailmark 4‑state codetext with valid sample data
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                                 // 4‑state format
        mailmark.VersionID = 1;                              // version
        mailmark.Class = "0";                                // test class
        mailmark.SupplychainID = 384224;                     // sample supply‑chain ID
        mailmark.ItemID = 16563762;                          // sample item ID
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";   // known‑valid postcode + DPS (trailing space)

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Rotate the image by 90 degrees as required
            generator.Parameters.RotationAngle = 90f;

            // Save the barcode image to a PNG file
            generator.Save("mailmark.png");
        }

        Console.WriteLine("Mailmark barcode generated and saved as 'mailmark.png'.");
    }
}