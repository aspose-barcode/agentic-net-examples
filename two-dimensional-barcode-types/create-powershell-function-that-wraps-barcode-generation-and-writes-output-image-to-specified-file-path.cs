using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        string symbologyName = "Code128";
        string codeText = "1234567890";
        string outputPath = "barcode.png";

        // Parse command‑line arguments if provided
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length >= 3 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve symbology name to EncodeTypes member via reflection (allowed)
        var encodeTypeField = typeof(EncodeTypes).GetField(symbologyName);
        if (encodeTypeField == null)
        {
            Console.Error.WriteLine($"Unsupported symbology: {symbologyName}");
            Environment.Exit(1);
        }
        var encodeType = (BaseEncodeType)encodeTypeField.GetValue(null);

        // Ensure output directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create directory '{directory}': {ex.Message}");
                Environment.Exit(1);
            }
        }

        // Generate and save the barcode
        try
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting a simple parameter (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the image; format inferred from file extension
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating barcode: {ex.Message}");
            Environment.Exit(1);
        }
    }
}