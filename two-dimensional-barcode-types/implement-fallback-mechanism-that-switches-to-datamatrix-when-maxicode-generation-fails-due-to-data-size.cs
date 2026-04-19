using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample data that may be too large for MaxiCode
        string data = new string('A', 500); // 500 characters

        // Attempt to generate MaxiCode (Mode 4)
        try
        {
            MaxiCodeStandardCodetext maxiCodetext = new MaxiCodeStandardCodetext();
            maxiCodetext.Mode = MaxiCodeMode.Mode4;
            maxiCodetext.Message = data;

            using (ComplexBarcodeGenerator complexGenerator = new ComplexBarcodeGenerator(maxiCodetext))
            {
                // Optional: set image resolution
                complexGenerator.Parameters.Resolution = 300;
                // Save MaxiCode image
                string maxiPath = "maxicode.png";
                complexGenerator.Save(maxiPath, BarCodeImageFormat.Png);
                Console.WriteLine($"MaxiCode generated successfully: {maxiPath}");
                return; // Success, no need for fallback
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MaxiCode generation failed: {ex.Message}");
            Console.WriteLine("Falling back to DataMatrix.");
        }

        // Fallback to DataMatrix
        try
        {
            using (BarcodeGenerator dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
            {
                // Example of setting a DataMatrix version fallback (optional)
                dmGenerator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_144x144;
                dmGenerator.Parameters.Resolution = 300;
                string dmPath = "datamatrix.png";
                dmGenerator.Save(dmPath, BarCodeImageFormat.Png);
                Console.WriteLine($"DataMatrix generated as fallback: {dmPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DataMatrix generation also failed: {ex.Message}");
        }
    }
}