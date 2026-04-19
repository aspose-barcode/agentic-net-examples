using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Determine the path of the source file (Program.cs) relative to the executable.
        string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs");

        // Read the source file content if it exists; otherwise use a fallback text.
        string codeText;
        if (File.Exists(sourceFilePath))
        {
            codeText = File.ReadAllText(sourceFilePath);
        }
        else
        {
            codeText = "FallbackBarcodeText";
        }

        // Define the output image path.
        string outputImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barcode.png");

        // Create a barcode generator for Code128 symbology with the obtained text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set image size (points) and resolution.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 96;

            // Set barcode appearance.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Ensure no automatic resizing interferes with explicit dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode image.
            generator.Save(outputImagePath);
        }

        Console.WriteLine($"Barcode image generated at: {outputImagePath}");
    }
}