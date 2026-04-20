using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample data for Australia Post barcode.
        // Postal part (5 digits) + customer information part.
        string postalPart = "59123";

        // Customer information encoded with CTable (alphanumeric allowed).
        string customerInfoCTable = "45678ABCde";
        string codeTextCTable = postalPart + customerInfoCTable;

        // Customer information encoded with NTable (digits only).
        string customerInfoNTable = "4567890123";
        string codeTextNTable = postalPart + customerInfoNTable;

        // Generate barcode using CTable interpreting type.
        string fileCTable = "auspost_ctable.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeTextCTable))
        {
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(fileCTable);
        }

        // Generate barcode using NTable interpreting type.
        string fileNTable = "auspost_ntable.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeTextNTable))
        {
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(fileNTable);
        }

        // Assess impact on barcode capacity by comparing file sizes.
        long sizeCTable = new FileInfo(fileCTable).Length;
        long sizeNTable = new FileInfo(fileNTable).Length;

        Console.WriteLine($"CTable barcode file size: {sizeCTable} bytes");
        Console.WriteLine($"NTable barcode file size: {sizeNTable} bytes");

        // Simple capacity analysis.
        if (sizeCTable > sizeNTable)
        {
            Console.WriteLine("CTable encoding results in a larger barcode (higher capacity usage).");
        }
        else if (sizeCTable < sizeNTable)
        {
            Console.WriteLine("NTable encoding results in a larger barcode (higher capacity usage).");
        }
        else
        {
            Console.WriteLine("Both encodings produce barcodes of equal size.");
        }
    }
}