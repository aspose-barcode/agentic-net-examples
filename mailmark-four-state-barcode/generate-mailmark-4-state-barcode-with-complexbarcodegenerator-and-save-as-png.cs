using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare Mailmark 4‑state codetext with valid sample data
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state format
        mailmark.VersionID = 1;                  // version identifier
        mailmark.Class = "0";                    // class (null/test)
        mailmark.SupplychainID = 384224;         // supply chain identifier
        mailmark.ItemID = 16563762;              // item identifier
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // known‑valid destination

        // Create ComplexBarcodeGenerator with the Mailmark codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set a positive bar height (required for 4‑state barcodes)
            generator.Parameters.Barcode.BarHeight.Point = 10f;

            // Save the barcode as PNG
            generator.Save("Mailmark4State.png");
        }
    }
}