// Title: Generate barcode image using Aspose.BarCode and output PowerShell wrapper function
// Description: Demonstrates creating a barcode image file with a specified symbology and text, then prints a PowerShell function that calls the compiled executable.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes to produce PNG images. Typical use cases include automating barcode creation in scripts or CI pipelines, and developers often need to wrap the generator in PowerShell for easy invocation. The snippet shows directory handling, reflection for symbology lookup, and output of a ready‑to‑use PowerShell wrapper.
// Prompt: Create a PowerShell function that wraps barcode generation and writes output image to specified file path.
// Tags: barcode, generation, png, powershell, wrapper, aspose.barcode, encode-types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode and provides a PowerShell wrapper function.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image based on command‑line arguments and prints a PowerShell function that invokes this executable.
    /// </summary>
    /// <param name="args">Command‑line arguments: symbology, code text, and optional output path.</param>
    static void Main(string[] args)
    {
        // Determine symbology, code text, and output path (use defaults if not supplied)
        string symbology = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "Sample123";
        string outputPath = args.Length > 2 ? args[2] : Path.Combine(Path.GetTempPath(), "barcode.png");

        // Resolve symbology name to BaseEncodeType via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Generate the barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Build the PowerShell function that wraps this executable
        string exeName = Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        string psFunction = $@"
function Generate-Barcode {{
    param(
        [Parameter(Mandatory=$true)][string]$Symbology,
        [Parameter(Mandatory=$true)][string]$CodeText,
        [Parameter(Mandatory=$true)][string]$OutputPath
    )
    $exePath = Join-Path (Split-Path -Parent $MyInvocation.MyCommand.Path) '{exeName}'
    & $exePath $Symbology $CodeText $OutputPath
}}".Trim();

        // Output the generated PowerShell function to the console
        Console.WriteLine();
        Console.WriteLine("PowerShell function to wrap this generator:");
        Console.WriteLine(psFunction);
    }
}