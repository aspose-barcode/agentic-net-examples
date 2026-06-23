using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to XML,
/// importing it back, and verifying that the settings are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path with an .xml extension for storing the exported settings.
        string tempXmlPath = Path.ChangeExtension(Path.GetTempFileName(), ".xml");

        // Create a barcode generator, configure its parameters, and export the configuration to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Disable automatic sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the X-dimension (module width) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set the barcode height to 40 points.
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Write the current configuration to the temporary XML file.
            generator.ExportToXml(tempXmlPath);
        }

        // Attempt to import a barcode generator from the previously saved XML file.
        BarcodeGenerator importedGenerator = null;
        try
        {
            importedGenerator = BarcodeGenerator.ImportFromXml(tempXmlPath);
        }
        catch (Exception ex)
        {
            // Report any import errors and clean up the temporary file before exiting.
            Console.WriteLine("ImportFromXml threw an exception: " + ex.Message);
            Cleanup(tempXmlPath);
            return;
        }

        // Verify that the imported generator's settings match the original configuration.
        bool isCodeTextEqual = importedGenerator.CodeText == "Test123";
        bool isSymbologyEqual = importedGenerator.BarcodeType.TypeName == EncodeTypes.Code128.TypeName;
        bool isXDimensionEqual = Math.Abs(importedGenerator.Parameters.Barcode.XDimension.Point - 2f) < 0.001f;
        bool isBarHeightEqual = Math.Abs(importedGenerator.Parameters.Barcode.BarHeight.Point - 40f) < 0.001f;
        bool isAutoSizeModeEqual = importedGenerator.Parameters.AutoSizeMode == AutoSizeMode.None;

        if (isCodeTextEqual && isSymbologyEqual && isXDimensionEqual && isBarHeightEqual && isAutoSizeModeEqual)
        {
            Console.WriteLine("Test Passed: Imported settings match the exported ones.");
        }
        else
        {
            // Output detailed mismatch information for debugging.
            Console.WriteLine("Test Failed: Mismatch in imported settings.");
            Console.WriteLine($"CodeText: Expected 'Test123', Actual '{importedGenerator.CodeText}'");
            Console.WriteLine($"Symbology: Expected '{EncodeTypes.Code128.TypeName}', Actual '{importedGenerator.BarcodeType.TypeName}'");
            Console.WriteLine($"XDimension: Expected 2, Actual {importedGenerator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"BarHeight: Expected 40, Actual {importedGenerator.Parameters.Barcode.BarHeight.Point}");
            Console.WriteLine($"AutoSizeMode: Expected None, Actual {importedGenerator.Parameters.AutoSizeMode}");
        }

        // Release resources used by the imported generator and delete the temporary XML file.
        importedGenerator?.Dispose();
        Cleanup(tempXmlPath);
    }

    /// <summary>
    /// Deletes the specified file if it exists, suppressing any exceptions.
    /// </summary>
    /// <param name="filePath">The full path of the file to delete.</param>
    static void Cleanup(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch
        {
            // Intentionally ignore any errors that occur during cleanup.
        }
    }
}