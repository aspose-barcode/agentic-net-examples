using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Sample data for Mailmark 4‑state barcode
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state format
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // class
        mailmark.SupplychainID = 384224;         // supply chain ID
        mailmark.ItemID = 16563762;              // item ID
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // destination postcode + DPS (trailing space)

        // ---------- Default (filled bars) ----------
        using (var generatorFilled = new ComplexBarcodeGenerator(mailmark))
        {
            // Ensure bars are visible
            generatorFilled.Parameters.Barcode.BarHeight.Point = 10f;
            // FilledBars is true by default, no need to set explicitly
            generatorFilled.Save("mailmark_filled.png");
        }

        // ---------- Bars not filled ----------
        using (var generatorUnfilled = new ComplexBarcodeGenerator(mailmark))
        {
            generatorUnfilled.Parameters.Barcode.BarHeight.Point = 10f;
            generatorUnfilled.Parameters.Barcode.FilledBars = false; // disable bar filling
            generatorUnfilled.Save("mailmark_unfilled.png");
        }

        Console.WriteLine("Generated 'mailmark_filled.png' (default filled bars) and 'mailmark_unfilled.png' (bars not filled).");
    }
}