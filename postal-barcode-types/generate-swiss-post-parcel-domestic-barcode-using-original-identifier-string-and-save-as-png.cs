using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Original identifier string for Swiss Post Parcel domestic barcode
        string identifier = "12345678901234567890";

        // Create the barcode generator with Swiss Post Parcel symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, identifier))
        {
            // Optional: set image resolution (dpi)
            generator.Parameters.Resolution = 300;

            // Save the barcode as a PNG file
            generator.Save("SwissPostParcel.png");
        }

        Console.WriteLine("Swiss Post Parcel barcode saved as SwissPostParcel.png");
    }
}