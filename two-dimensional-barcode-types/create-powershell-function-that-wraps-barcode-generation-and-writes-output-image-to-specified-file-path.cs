// Title: Generate Barcode and Build PowerShell Wrapper with Aspose.BarCode
// Description: Demonstrates generating a barcode image using Aspose.BarCode and creating a PowerShell function that encapsulates the same logic for reuse.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical scenarios include creating barcodes for inventory, shipping, or authentication purposes, and exposing the functionality to PowerShell scripts for automation. Developers often need to map symbology names to EncodeTypes via reflection and output images in common formats such as PNG.
// Prompt: Create a PowerShell function that wraps barcode generation and writes output image to specified file path.
// Tags: barcode, symbology, generation, png, powershell, aspose.barcode, encode-types

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console example that generates a barcode image and builds a PowerShell function
/// wrapping the same generation logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode, writes it to a temporary file,
    /// and outputs a PowerShell function definition that can be used to generate barcodes from scripts.
    /// </summary>
    static void Main()
    {
        // Define sample data for barcode generation
        string sampleCodeText = "HelloWorld";
        string sampleSymbology = "Code128";
        string sampleOutputPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate the sample barcode image
        GenerateBarcode(sampleSymbology, sampleCodeText, sampleOutputPath);
        Console.WriteLine($"Sample barcode saved to: {sampleOutputPath}");

        // Build and display the PowerShell function that mirrors the C# generation logic
        string psFunction = BuildPowerShellFunction("Generate-Barcode");
        Console.WriteLine();
        Console.WriteLine("PowerShell function:");
        Console.WriteLine(psFunction);
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and code text, then saves it to the given path.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">File system path where the PNG image will be saved.</param>
    static void GenerateBarcode(string symbologyName, string codeText, string outputPath)
    {
        // Resolve the symbology name to the corresponding EncodeTypes field via reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            throw new ArgumentException($"Unknown symbology: {symbologyName}");
        }

        // Cast the reflected value to BaseEncodeType, which BarcodeGenerator expects
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the generator, encode the text, and save the image as PNG
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Constructs a PowerShell function script that wraps Aspose.BarCode barcode generation.
    /// </summary>
    /// <param name="functionName">Name of the PowerShell function to generate.</param>
    /// <returns>A string containing the PowerShell function definition.</returns>
    static string BuildPowerShellFunction(string functionName)
    {
        var sb = new StringBuilder();

        // Begin function definition
        sb.AppendLine($"function {functionName} {{");
        sb.AppendLine("    param(");
        sb.AppendLine("        [string]$CodeText,");
        sb.AppendLine("        [string]$Symbology,");
        sb.AppendLine("        [string]$OutputPath");
        sb.AppendLine("    )");
        sb.AppendLine();

        // Resolve symbology name to EncodeTypes field
        sb.AppendLine("    # Resolve symbology name to EncodeTypes field");
        sb.AppendLine("    $field = [Aspose.BarCode.Generation.EncodeTypes]::GetField($Symbology)");
        sb.AppendLine("    if ($null -eq $field) {");
        sb.AppendLine("        Write-Error \"Unknown symbology: $Symbology\"");
        sb.AppendLine("        return");
        sb.AppendLine("    }");
        sb.AppendLine("    $encodeType = $field.GetValue($null)");
        sb.AppendLine();

        // Create generator, set code text, and save the image
        sb.AppendLine("    $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator($encodeType, $null)");
        sb.AppendLine("    $generator.SetCodeText($CodeText, [System.Text.Encoding]::UTF8)");
        sb.AppendLine("    $generator.Save($OutputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)");
        sb.AppendLine("    $generator.Dispose()");
        sb.AppendLine("    Write-Host \"Barcode saved to $OutputPath\"");
        sb.AppendLine("}");
        return sb.ToString();
    }
}