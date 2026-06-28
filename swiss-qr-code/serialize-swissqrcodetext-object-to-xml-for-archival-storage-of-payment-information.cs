using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creation, serialization, and barcode generation for a Swiss QR Bill using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a SwissQRCodetext instance with sample payment details.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Prepare XML serializer for the SwissQRCodetext type.
        var serializer = new XmlSerializer(typeof(SwissQRCodetext));
        string xmlPath = "SwissQR.xml";

        // Serialize the object to an XML file for archival storage.
        using (var fileStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            serializer.Serialize(fileStream, swissQr);
        }

        Console.WriteLine($"SwissQRCodetext serialized to '{Path.GetFullPath(xmlPath)}'.");

        // Generate a barcode image from the SwissQRCodetext instance.
        string imagePath = "SwissQR.png";
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Save the barcode as a PNG file.
            generator.Save(imagePath, Aspose.BarCode.Generation.BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to '{Path.GetFullPath(imagePath)}'.");
    }
}