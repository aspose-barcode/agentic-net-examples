using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Barcode data and output file names
        string codeText = "1234567890";
        string file200 = "barcode200.png";
        string file300 = "barcode300.png";

        // Generate barcode with 200 DPI resolution
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator200.Parameters.Resolution = 200f; // set resolution to 200 DPI
            generator200.Save(file200); // save image
        }

        // Generate barcode with 300 DPI resolution
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // set resolution to 300 DPI
            generator300.Save(file300); // save image
        }

        // Compare file sizes
        long size200 = new FileInfo(file200).Length;
        long size300 = new FileInfo(file300).Length;

        Console.WriteLine($"200 DPI file size: {size200} bytes");
        Console.WriteLine($"300 DPI file size: {size300} bytes");

        if (size200 < size300)
            Console.WriteLine("200 DPI image is smaller.");
        else if (size200 > size300)
            Console.WriteLine("300 DPI image is smaller.");
        else
            Console.WriteLine("Both images have the same size.");
    }
}