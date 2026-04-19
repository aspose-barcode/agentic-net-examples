using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleText"))
        {
            // Set caption text and make it visible
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "DataMatrix Sample";

            // Define caption font using FontUnit with Document unit
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f; // 12 points in document units

            // Optional: set barcode colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as BMP file
            generator.Save("DataMatrixSample.bmp");
        }
    }
}