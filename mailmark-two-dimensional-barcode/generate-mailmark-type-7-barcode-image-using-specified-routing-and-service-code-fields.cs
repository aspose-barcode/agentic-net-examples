using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Mailmark 2D codetext instance
        var mailmark2d = new Mailmark2DCodetext
        {
            VersionID = "1",                     // Version identifier (single character)
            InformationTypeID = "0",             // Domestic Sorted & Unsorted
            Class = "0",                         // Null or Test class
            SupplyChainID = 384224,              // Example supply chain ID
            ItemID = 16563762,                   // Unique item ID
            DestinationPostCodeAndDPS = "EF61AH8T ", // Valid postcode + DPS (9 chars with trailing spaces)

            // Optional fields (set to defaults)
            ReturnToSenderPostCode = string.Empty,
            RTSFlag = "0",
            UPUCountryID = "GB",

            // Specify the 2D Mailmark type (type 7 = 24x24 modules)
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Create the complex barcode generator with the configured codetext
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            // Set a positive bar height (required for Mailmark barcodes)
            generator.Parameters.Barcode.BarHeight.Pixels = 10;

            // Set image size to avoid zero‑size image error
            generator.Parameters.ImageWidth.Pixels = 300;
            generator.Parameters.ImageHeight.Pixels = 300;

            // Save the generated barcode image to a PNG file
            generator.Save("MailmarkType7.png");
        }

        Console.WriteLine("Mailmark type 7 barcode generated: MailmarkType7.png");
    }
}