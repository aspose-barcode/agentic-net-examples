using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create Mailmark2DCodetext and assign routing, service, and customer data
        var mailmark2d = new Mailmark2DCodetext
        {
            // Routing / service identifiers (single-character strings)
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",
            RTSFlag = "0",

            // Supply chain and item identifiers
            SupplyChainID = 1234567,
            ItemID = 7654321,

            // Destination postcode with DPS (trailing space as required)
            DestinationPostCodeAndDPS = "EF61AH8T ",

            // Customer specific content
            CustomerContent = "CustomerData",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // Generate and save the Mailmark 2D barcode image
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            generator.Save("mailmark2d.png");
        }

        Console.WriteLine("Mailmark 2D barcode generated successfully.");
    }
}