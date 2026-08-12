// Title: Generate PowerShell module for Aspose.BarCode barcode creation
// Description: This example creates a PowerShell .psm1 file that wraps Aspose.BarCode .NET generation methods, enabling quick scripting of barcode images.
// Category-Description: Demonstrates how to use Aspose.BarCode's generation API (BarcodeGenerator, EncodeTypes, Parameters) to produce barcodes from PowerShell. Typical scenarios include automating barcode creation in scripts, CI pipelines, or admin tasks. Developers often need a lightweight wrapper to call .NET barcode functions without writing full C# code.
// Prompt: Provide a PowerShell module that wraps .NET barcode generation methods for quick scripting use.
// Tags: barcode, symbology, generation, powershell, aspose, .net, module, encoding

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PowerShell module that wraps Aspose.BarCode barcode generation methods.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the PowerShell module and writes its location to the console.
    /// </summary>
    static void Main()
    {
        try
        {
            // Create the PowerShell module and obtain its file path.
            string modulePath = WritePowerShellModule();

            // Inform the user where the module was saved.
            Console.WriteLine($"PowerShell module created at: {modulePath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occurred during module creation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Generates a simple PowerShell module (.psm1) that wraps Aspose.BarCode generation.
    static string WritePowerShellModule()
    {
        // Define the PowerShell script content that will be written to the .psm1 file.
        string psModule = @"
# Aspose.BarCode PowerShell wrapper module
# Load the Aspose.BarCode assembly (assumes the DLL is in the same directory as this module)
$assemblyPath = Join-Path $PSScriptRoot 'Aspose.BarCode.dll'
if (-not (Test-Path $assemblyPath)) {
    Write-Error ""Aspose.BarCode.dll not found at $assemblyPath""
    return
}
Add-Type -Path $assemblyPath

function Resolve-EncodeType {
    param(
        [Parameter(Mandatory=$true)][string]$SymbologyName
    )
    $field = [Aspose.BarCode.Generation.EncodeTypes].GetField($SymbologyName)
    if ($null -eq $field) {
        throw ""Unknown symbology: $SymbologyName""
    }
    return $field.GetValue($null)
}

function New-Barcode {
    param(
        [Parameter(Mandatory=$true)][string]$Symbology,
        [Parameter(Mandatory=$true)][string]$CodeText,
        [Parameter(Mandatory=$true)][string]$OutputPath
    )
    $encodeType = Resolve-EncodeType -SymbologyName $Symbology
    using ($generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator $encodeType, $CodeText) {
        # Example: set foreground color to black and background to white
        $generator.Parameters.Barcode.BarColor = [Aspose.Drawing.Color]::Black
        $generator.Parameters.BackColor = [Aspose.Drawing.Color]::White
        $generator.Save($OutputPath)
    }
}
";

        // Create a unique temporary folder for the module.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeModule_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Write the module file to the temporary folder.
        string moduleFile = Path.Combine(tempFolder, "AsposeBarcode.psm1");
        File.WriteAllText(moduleFile, psModule);

        // Return the full path to the generated module.
        return moduleFile;
    }
}