using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: symbology name, data string, and output file path.
    /// </summary>
    /// <param name="args">
    /// args[0] – symbology name (e.g., "Code128")<br/>
    /// args[1] – data to encode (e.g., "Sample123")<br/>
    /// args[2] – output file path (e.g., "barcode.png")
    /// </param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Determine input parameters, falling back to defaults if not provided.
        // --------------------------------------------------------------------
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string data = args.Length > 1 ? args[1] : "Sample123";
        string outputPath = args.Length > 2 ? args[2] : "barcode.png";

        // --------------------------------------------------------------
        // Ensure the directory for the output file exists; create if needed.
        // --------------------------------------------------------------
        string? directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // --------------------------------------------------------------
        // Resolve the symbology name to the corresponding BaseEncodeType
        // using reflection on the EncodeTypes class.
        // --------------------------------------------------------------
        FieldInfo? field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the reflected field value to BaseEncodeType.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null)!;

        try
        {
            // --------------------------------------------------------------
            // Generate the barcode and save it to the specified file.
            // --------------------------------------------------------------
            using (var generator = new BarcodeGenerator(encodeType, data))
            {
                // Optional: set image resolution (dots per inch).
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode image.
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode generated: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}