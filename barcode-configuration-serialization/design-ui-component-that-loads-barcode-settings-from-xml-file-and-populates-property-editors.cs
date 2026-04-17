using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define the XML file that contains barcode settings.
        const string xmlFile = "barcodeSettings.xml";

        // Verify that the XML file exists; if not, exit gracefully.
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine($"XML file '{xmlFile}' not found. Skipping barcode loading.");
            return;
        }

        // Load barcode settings from the XML file using the provided ImportFromXml method.
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlFile))
        {
            // Display a few key properties as if they were populated in UI editors.
            Console.WriteLine("=== Loaded Barcode Settings ===");
            Console.WriteLine($"Barcode Type   : {generator.BarcodeType}");
            Console.WriteLine($"Code Text      : {generator.CodeText}");

            // Bar color (foreground)
            Console.WriteLine($"Bar Color      : {generator.Parameters.Barcode.BarColor}");

            // Background color
            Console.WriteLine($"Back Color     : {generator.Parameters.BackColor}");

            // X-Dimension (smallest bar width) in pixels
            Console.WriteLine($"X-Dimension    : {generator.Parameters.Barcode.XDimension.Pixels} px");

            // Bar height for 1D barcodes (if applicable) in pixels
            Console.WriteLine($"Bar Height     : {generator.Parameters.Barcode.BarHeight.Pixels} px");

            // Image size when AutoSizeMode is set to Interpolation or Nearest
            Console.WriteLine($"Image Width    : {generator.Parameters.ImageWidth.Pixels} px");
            Console.WriteLine($"Image Height   : {generator.Parameters.ImageHeight.Pixels} px");

            // Padding values (left, top, right, bottom) in points
            Console.WriteLine("Padding (pt):");
            Console.WriteLine($"  Left   : {generator.Parameters.Barcode.Padding.Left.Point} pt");
            Console.WriteLine($"  Top    : {generator.Parameters.Barcode.Padding.Top.Point} pt");
            Console.WriteLine($"  Right  : {generator.Parameters.Barcode.Padding.Right.Point} pt");
            Console.WriteLine($"  Bottom : {generator.Parameters.Barcode.Padding.Bottom.Point} pt");

            // Example of a caption property
            Console.WriteLine($"Caption Above Visible : {generator.Parameters.CaptionAbove.Visible}");
            Console.WriteLine($"Caption Below Visible : {generator.Parameters.CaptionBelow.Visible}");

            // Save a preview image to verify that settings are applied.
            const string previewFile = "preview.png";
            generator.Save(previewFile);
            Console.WriteLine($"Preview image saved to '{previewFile}'.");
        }
    }
}