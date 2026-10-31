// Title: Generate a temporary PowerShell module that wraps Aspose.BarCode for barcode creation
// Description: Demonstrates how to programmatically create a PowerShell module which exposes a New-Barcode function that uses Aspose.BarCode to generate PNG barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to integrate the Aspose.BarCode .NET API into scripting environments. It highlights key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to produce barcodes in various formats for automation, CI pipelines, or quick prototyping.
// Prompt: Provide a PowerShell module that wraps .NET barcode generation methods for quick scripting use.
// Tags: barcode, symbology, generation, powershell, module, aspose.barcode

using System;
using System.IO;
using System.Text;

/// <summary>
/// Demonstrates creating a temporary PowerShell module that wraps Aspose.BarCode generation methods.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the PowerShell script file and writes it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the PowerShell module
        string moduleFolder = Path.Combine(Path.GetTempPath(), "PsBarcodeModule_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(moduleFolder);

        // Define the full path for the module file
        string modulePath = Path.Combine(moduleFolder, "BarcodeModule.psm1");

        // PowerShell script content that wraps Aspose.BarCode generation
        string psScript = @"
# PowerShell module for barcode generation using Aspose.BarCode
function New-Barcode {
    param(
        [Parameter(Mandatory=$true)][string]$Symbology,
        [Parameter(Mandatory=$true)][string]$CodeText,
        [Parameter(Mandatory=$true)][string]$OutputPath
    )
    # Load Aspose.BarCode assembly if not already loaded
    if (-not ([System.AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GetName().Name -eq 'Aspose.BarCode' })) {
        $assemblyPath = Join-Path -Path $PSScriptRoot -ChildPath 'Aspose.BarCode.dll'
        Add-Type -Path $assemblyPath
    }

    # Resolve symbology name to EncodeTypes field via reflection
    $type = [Aspose.BarCode.Generation.EncodeTypes]
    $field = $type.GetField($Symbology)
    if ($null -eq $field) {
        throw ""Unknown symbology: $Symbology""
    }
    $encodeType = $field.GetValue($null)

    # Create generator and save barcode
    $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator($encodeType, $CodeText)
    $generator.Save($OutputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)
    $generator.Dispose()
}
";

        // Write the PowerShell module file to disk using UTF-8 encoding
        File.WriteAllText(modulePath, psScript, Encoding.UTF8);

        // Inform the user where the module was created
        Console.WriteLine("PowerShell module created at: " + modulePath);
    }
}