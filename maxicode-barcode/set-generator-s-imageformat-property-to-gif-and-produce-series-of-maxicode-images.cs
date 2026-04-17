using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeOutputs");
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Define the MaxiCode modes to generate (Mode4, Mode5, Mode6)
        MaxiCodeMode[] modes = new[] { MaxiCodeMode.Mode4, MaxiCodeMode.Mode5, MaxiCodeMode.Mode6 };

        foreach (var mode in modes)
        {
            // Prepare standard codetext for the current mode
            var maxiCodeCodetext = new MaxiCodeStandardCodetext
            {
                Mode = mode,
                Message = $"Sample message for {mode}"
            };

            // Generate the MaxiCode image using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Save the generated image as GIF to a memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Gif);
                    ms.Position = 0;

                    // Write the GIF file to disk
                    string filePath = Path.Combine(outputDir, $"MaxiCode_{mode}.gif");
                    File.WriteAllBytes(filePath, ms.ToArray());

                    Console.WriteLine($"Generated {filePath}");
                }
            }
        }
    }
}