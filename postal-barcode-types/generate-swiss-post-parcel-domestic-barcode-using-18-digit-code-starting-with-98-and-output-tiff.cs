using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // 18‑digit Swiss Post Parcel domestic code, must start with "98"
        string code = "981234567890123456";

        // Simple validation
        if (code.Length != 18 || !code.StartsWith("98"))
        {
            Console.WriteLine("Error: Code must be exactly 18 digits and start with '98'.");
            return;
        }

        // Create the barcode generator for Swiss Post Parcel
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
        {
            // Optional: increase resolution for better quality
            generator.Parameters.Resolution = 300;

            // Save the barcode as a TIFF image
            generator.Save("SwissPostParcel.tif");
        }

        Console.WriteLine("Swiss Post Parcel barcode saved as SwissPostParcel.tif");
    }
}