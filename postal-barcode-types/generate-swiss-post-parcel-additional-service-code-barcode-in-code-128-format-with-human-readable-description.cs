using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample additional service code for Swiss Post Parcel
        const string serviceCode = "1234567890";
        // Human‑readable description to be shown above the barcode
        const string description = "Additional Service: Express Delivery";

        // Create a barcode generator for Swiss Post Parcel using Code 128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, serviceCode))
        {
            // Set image size (points) and disable auto‑size interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Configure human‑readable description as a caption above the barcode
            generator.Parameters.CaptionAbove.Text = description;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.Visible = true;

            // Optional: customize barcode colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to a PNG file
            generator.Save("SwissPostParcelAdditionalService.png");
        }
    }
}