using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample code text for Swiss Post Parcel (International) barcode.
        // In a real scenario, use a valid Swiss Post Parcel code.
        const string codeText = "1234567890123456";

        // Create the barcode generator for Swiss Post Parcel.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Enable checksum generation (automatic correction if needed).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Optional: set image resolution (dpi) if desired.
            generator.Parameters.Resolution = 300f;

            // Save the barcode as JPEG.
            const string outputFile = "SwissPostParcel.jpg";
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine("Swiss Post Parcel barcode saved as JPEG.");
    }
}