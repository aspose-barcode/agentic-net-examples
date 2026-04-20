using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for ITF14 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "123456789012"))
        {
            // Configure ITF parameters
            // Set a thick frame border
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f; // thick border

            // Set quiet zone coefficient (the property expects an int, minimum 10)
            // The requested value 0.3 is below the allowed range, so we handle it gracefully.
            try
            {
                // Attempt to set the coefficient; this will throw if the value is invalid.
                generator.Parameters.Barcode.ITF.QuietZoneCoef = (int)(0.3 * 100); // 30 as an approximation
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Warning: Unable to set QuietZoneCoef to the requested value. {ex.Message}");
                // Fallback to the minimum allowed value
                generator.Parameters.Barcode.ITF.QuietZoneCoef = 10;
            }

            // Optional: set colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a TIFF image
            generator.Save("itf_barcode.tif");
        }
    }
}