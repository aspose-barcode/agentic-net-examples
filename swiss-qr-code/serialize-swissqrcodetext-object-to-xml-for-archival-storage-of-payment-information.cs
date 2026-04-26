using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create and populate a SwissQRCodetext instance
        var swissQRCodetext = new SwissQRCodetext();
        swissQRCodetext.Bill.Account = "CH9300762011623852957";
        swissQRCodetext.Bill.Amount = 199.95m;
        swissQRCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCodetext.Bill.Creditor.CountryCode = "CH";
        swissQRCodetext.Bill.Currency = "CHF";
        swissQRCodetext.Bill.Reference = "RF18539007547034";

        // Serialize the object to XML
        var serializer = new XmlSerializer(typeof(SwissQRCodetext));
        const string xmlFile = "SwissQR.xml";

        using (var stream = new FileStream(xmlFile, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            serializer.Serialize(stream, swissQRCodetext);
        }

        Console.WriteLine($"SwissQRCodetext has been serialized to '{xmlFile}'.");
    }
}