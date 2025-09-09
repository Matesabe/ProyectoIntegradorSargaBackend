using MonitoreoDeArchivos.fromXlsx;
using MonitoreoDeArchivos.fromXls;
using System;
using System.IO;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel; // Para .xls
using NPOI.XSSF.UserModel; // Para .xlsx
using NPOI.SS.UserModel;
using System.IO;
using NPOI.POIFS.FileSystem;
using NPOI.POIFS.FileSystem;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using SharedUseCase.DTOs.Product;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using MonitoreoDeArchivos.ApiCalls;
using SharedUseCase.DTOs.Purchase;


class Program
{
    static void Main(string[] args)
    {
        string rutaCarpetaDestinoProductos = @"C:\Users\mateo\OneDrive\Escritorio\facultad\pruebasModInt\carpetaInProductos"; 

        using var watcherProductos = new FileSystemWatcher(rutaCarpetaDestinoProductos);
        watcherProductos.Filter = "*.xls*"; // filtro de archivo a leer
        watcherProductos.Created += OnCreatedProducts;
        watcherProductos.EnableRaisingEvents = true;

        string rutaCarpetaDestinoPurchases = @"C:\Users\mateo\OneDrive\Escritorio\facultad\pruebasModInt\carpetaInVentas"; 

        using var watcherPurchases = new FileSystemWatcher(rutaCarpetaDestinoPurchases);
        watcherPurchases.Filter = "*.xls*"; 
        watcherPurchases.Created += OnCreatedPurchases;
        watcherPurchases.EnableRaisingEvents = true;

        Console.WriteLine("Monitoreando carpeta. Pulsa Enter para salir.");
        Console.ReadLine();
    }

    private static void OnCreatedPurchases(object sender, FileSystemEventArgs e)
    {
        IEnumerable<PurchaseDto> subProductsDtos = null;
        Console.WriteLine($"Archivo detectado: {e.FullPath}");
        string ext = Path.GetExtension(e.FullPath).ToLower();
        string xlsxPath = Path.ChangeExtension(e.FullPath, ".xlsx");

        // Ruta al archivo .xls original
        string inputPath = e.FullPath;

        // Ruta donde se guardará el archivo .xlsx corregido
        string outputDir = @"C:\Users\mateo\OneDrive\Escritorio\facultad\pruebasModInt\carpetaOutVentas";
        string libreOfficePath = @"C:\Program Files\LibreOffice\program\soffice.exe";

        // Validar existencia del archivo original
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("El archivo de entrada no existe.");
            return;
        }

        // Crear la carpeta de salida si no existe
        Directory.CreateDirectory(outputDir);

        // Armar el comando
        string arguments = $"--headless --convert-to xlsx:\"Calc MS Excel 2007 XML\" \"{inputPath}\" --outdir \"{outputDir}\"";

        var psi = new ProcessStartInfo
        {
            FileName = libreOfficePath,
            Arguments = arguments,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        Console.WriteLine("Convirtiendo el archivo con LibreOffice...");


        try
        {
            var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine("Salida:");
            Console.WriteLine(output);
            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine("Errores:");
                Console.WriteLine(error);
            }

            // Verificar si el archivo fue creado
            string convertedFile = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + ".xlsx");

            if (File.Exists(convertedFile))
            {
                Console.WriteLine("Archivo convertido exitosamente:");
                Console.WriteLine(convertedFile);
                Console.WriteLine($"Archivo xlsx detectado: {convertedFile}");
                xlsxConverterPurchases converter = new xlsxConverterPurchases();
                try
                {
                    // Procesar el archivo recién creado

                    List<PurchaseDto> purchasesDtos = converter.ConvertFromXlsx(convertedFile);
                    Console.WriteLine("Archivo procesado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No se encontró el archivo convertido. Algo falló.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar LibreOffice:");
            Console.WriteLine(ex.Message);
        }

        // Parsear y transformar a JSON, luego enviar a la API
        if (subProductsDtos != null)
        {
            Console.WriteLine("éxito al convertir archivo");
        }
    }


    private static void OnCreatedProducts(object sender, FileSystemEventArgs e)
    {
        IEnumerable<SubProductDto> subProductsDtos = null;
        Console.WriteLine($"Archivo detectado: {e.FullPath}");
        string ext = Path.GetExtension(e.FullPath).ToLower();
        string xlsxPath = Path.ChangeExtension(e.FullPath, ".xlsx");

        // Ruta al archivo .xls original
        string inputPath = e.FullPath;

        // Ruta donde se guardará el archivo .xlsx corregido
        string outputDir = @"C:\Users\mateo\OneDrive\Escritorio\facultad\pruebasModInt\carpetaOutProductos";
        string libreOfficePath = @"C:\Program Files\LibreOffice\program\soffice.exe";

        // Validar existencia del archivo original
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("El archivo de entrada no existe.");
            return;
        }

        // Crear la carpeta de salida si no existe
        Directory.CreateDirectory(outputDir);

        // Armar el comando
        string arguments = $"--headless --convert-to xlsx:\"Calc MS Excel 2007 XML\" \"{inputPath}\" --outdir \"{outputDir}\"";

        var psi = new ProcessStartInfo
        {
            FileName = libreOfficePath,
            Arguments = arguments,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        Console.WriteLine("Convirtiendo el archivo con LibreOffice...");
        
        
        try
        {
            var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine("Salida:");
            Console.WriteLine(output);
            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine("Errores:");
                Console.WriteLine(error);
            }

            // Verificar si el archivo fue creado
            string convertedFile = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + ".xlsx");

            if (File.Exists(convertedFile))
            {
                Console.WriteLine("Archivo convertido exitosamente:");
                Console.WriteLine(convertedFile);
                Console.WriteLine($"Archivo xlsx detectado: {convertedFile}");
                xlsxConverterSubProducts converter = new xlsxConverterSubProducts();
                try
                {
                    // Procesar el archivo recién creado
                    
                    subProductsDtos = converter.ConvertFromXlsx(convertedFile);
                    Console.WriteLine("Archivo procesado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No se encontró el archivo convertido. Algo falló.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar LibreOffice:");
            Console.WriteLine(ex.Message);
        }

        // Parsear y transformar a JSON, luego enviar a la API
        if (subProductsDtos!=null)
        {
            Console.WriteLine("éxito al convertir archivo");
        }
    }
    //MANDAR ALERTA SI FALLA, POR EJEMPLO MAIL
    
    //LOG FECHA CANTIDAD DE PRODUTOS 
}


