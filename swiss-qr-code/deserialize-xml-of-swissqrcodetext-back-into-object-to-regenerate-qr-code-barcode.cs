using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample SwissQR codetext object and fill required fields.
        var originalCodetext = new SwissQRCodetext();
        originalCodetext.Bill.Account = "CH9300762011623852957";
        originalCodetext.Bill.Creditor.CountryCode = "CH";
        originalCodetext.Bill.Creditor.Name = "John Doe";
        originalCodetext.Bill.Creditor.Street = "Main Street 1";
        originalCodetext.Bill.Creditor.PostalCode = "8000";
        originalCodetext.Bill.Creditor.Town = "Zurich";
        originalCodetext.Bill.Amount = 199.95m;
        originalCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Construct the encoded string from the object.
        string constructed = originalCodetext.GetConstructedCodetext();

        // Step 2: Serialize the constructed codetext into a simple XML string.
        string xmlContent = $"<SwissQR><CodeText>{constructed}</CodeText></SwissQR>";

        // Step 3: Parse the XML and extract the codetext.
        XDocument doc = XDocument.Parse(xmlContent);
        string extractedCodeText = doc.Root.Element("CodeText")?.Value;
        if (string.IsNullOrEmpty(extractedCodeText))
        {
            Console.WriteLine("Failed to extract codetext from XML.");
            return;
        }

        // Step 4: Initialize a new SwissQRCodetext object from the extracted string.
        var deserializedCodetext = new SwissQRCodetext();
        deserializedCodetext.InitFromString(extractedCodeText);

        // Step 5: Generate the Swiss QR barcode image using ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(deserializedCodetext))
        {
            string outputPath = "SwissQR.png";
            generator.Save(outputPath);
            Console.WriteLine($"Swiss QR barcode saved to '{Path.GetFullPath(outputPath)}'.");
        }
    }
}