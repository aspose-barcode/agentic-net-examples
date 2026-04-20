using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample codetext for Swiss Post Parcel Additional Service (replace with actual format as needed)
        const string swissPostParcelCode = "1234567890123";

        // Service description to be attached as metadata (display text)
        const string serviceDescription = "Additional Service: Express Delivery";

        // Create the barcode generator for Swiss Post Parcel
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, swissPostParcelCode))
        {
            // Attach the service description as display text (metadata)
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = serviceDescription;

            // Optional: hide the regular human‑readable code text if not needed
            // generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image
            generator.Save("SwissPostParcel.png");
        }

        Console.WriteLine("Swiss Post Parcel barcode generated successfully.");
    }
}