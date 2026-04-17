using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        int mode = 4; // MaxiCodeMode.Mode4
        string outputPath = "maxicode.png";

        // Parse command‑line arguments
        if (args.Length > 0 && int.TryParse(args[0], out int parsedMode))
        {
            mode = parsedMode;
        }
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Generate barcode based on the requested mode
        switch (mode)
        {
            case 2:
                GenerateMode2(outputPath);
                break;
            case 3:
                GenerateMode3(outputPath);
                break;
            case 4:
            case 5:
            case 6:
                GenerateStandardMode(outputPath, (MaxiCodeMode)mode);
                break;
            default:
                Console.WriteLine($"Unsupported MaxiCode mode: {mode}. Supported modes are 2‑6.");
                break;
        }
    }

    // Generates MaxiCode Mode 2 (postal + standard second message)
    private static void GenerateMode2(string outputPath)
    {
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",          // 9‑digit US postal code
            CountryCode = 56,                  // Example country code
            ServiceCategory = 999
        };

        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        codetext.SecondMessage = secondMessage;

        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            using (var image = generator.GenerateBarCodeImage())
            {
                SaveImage(image, outputPath);
            }
        }
    }

    // Generates MaxiCode Mode 3 (postal + standard second message)
    private static void GenerateMode3(string outputPath)
    {
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",              // 6‑character alphanumeric postal code
            CountryCode = 56,
            ServiceCategory = 999
        };

        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        codetext.SecondMessage = secondMessage;

        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            using (var image = generator.GenerateBarCodeImage())
            {
                SaveImage(image, outputPath);
            }
        }
    }

    // Generates MaxiCode Modes 4, 5, 6 (standard codetext)
    private static void GenerateStandardMode(string outputPath, MaxiCodeMode mode)
    {
        var codetext = new MaxiCodeStandardCodetext
        {
            Mode = mode,
            Message = "Sample message"
        };

        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            using (var image = generator.GenerateBarCodeImage())
            {
                SaveImage(image, outputPath);
            }
        }
    }

    // Saves the generated image to a file using Aspose.Drawing
    private static void SaveImage(Image image, string path)
    {
        using (var ms = new MemoryStream())
        {
            image.Save(ms, ImageFormat.Png);
            File.WriteAllBytes(path, ms.ToArray());
        }
    }
}