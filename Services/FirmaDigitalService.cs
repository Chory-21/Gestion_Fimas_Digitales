using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Gestion_Fimas_Digitales.Models;
using OfficeOpenXml;

namespace Gestion_Fimas_Digitales.Services
{
    public interface IFirmaDigitalService
    {
        MemoryStream ExportToExcel(List<FirmaDigital> firmas);
        void ValidateFirmaDigital(FirmaDigital firmaDigital);
        void DeleteFiles(string rutaRubrica);
    }

    public class FirmaDigitalService : IFirmaDigitalService
    {
        public MemoryStream ExportToExcel(List<FirmaDigital> firmas)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Firmas Digitales");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Tipo de Firma";
                worksheet.Cells[1, 3].Value = "Razón Social";
                worksheet.Cells[1, 4].Value = "Representante Legal";
                worksheet.Cells[1, 5].Value = "Empresa Acreditadora";
                worksheet.Cells[1, 6].Value = "Fecha Emisión";
                worksheet.Cells[1, 7].Value = "Fecha Vencimiento";
                worksheet.Cells[1, 8].Value = "Rúbrica";
                worksheet.Cells[1, 9].Value = "Certificado Digital";

                // Add data
                int row = 2;
                foreach (var firma in firmas)
                {
                    worksheet.Cells[row, 1].Value = firma.Id;
                    worksheet.Cells[row, 2].Value = firma.TipoFirma == '1' ? "Firma Digital" : firma.TipoFirma == '2' ? "Rúbrica Escaneada" : "Firma Electrónica";
                    worksheet.Cells[row, 3].Value = firma.RazonSocial.ToUpper();
                    worksheet.Cells[row, 4].Value = firma.RepresentanteLegal;
                    worksheet.Cells[row, 5].Value = firma.EmpresaAcreditadora.ToUpper();
                    worksheet.Cells[row, 6].Value = firma.FechaEmision.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 7].Value = firma.FechaVencimiento.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 8].Value = firma.RutaRubrica;
                    worksheet.Cells[row, 9].Value = firma.CertificadoDigital;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        private void EnsureFolderExists(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            return $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        public void ValidateFirmaDigital(FirmaDigital firmaDigital)
        {
            if (firmaDigital.FechaEmision >= firmaDigital.FechaVencimiento)
            {
                throw new InvalidOperationException("La fecha de vencimiento debe ser posterior a la fecha de emisión.");
            }

            // Transformar a mayúsculas
            firmaDigital.EmpresaAcreditadora = firmaDigital.EmpresaAcreditadora.ToUpper();
            firmaDigital.RazonSocial = firmaDigital.RazonSocial.ToUpper();
        }

        public void DeleteFiles(string rutaRubrica)
        {
            throw new NotImplementedException();
        }
    }
}


