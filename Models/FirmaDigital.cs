using Gestion_Fimas_Digitales.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Fimas_Digitales.Models
{
    [Table("firmadigital")]
    public class FirmaDigital
    {
        [Key]
        [Column("id_firma")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Column("tipo_firma")]
        public char TipoFirma { get; set; } // 1: Firma Digital, 2: Rúbrica Escaneada, 3: Firma Electrónica

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(200)]
        [Column("razon_social")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(200)]
        [Column("representante_legal")]
        public string RepresentanteLegal { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(200)]
        [Column("empresa_acreditadora")]
        public string EmpresaAcreditadora { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Column("fecha_emision")]
        [DataType(DataType.Date)]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Column("fecha_vencimiento")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "La fecha de vencimiento debe ser mayor que la fecha de emisión.")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Column("ruta_rubrica")]
        public string RutaRubrica { get; set; }

        [Column("certificado_digital")]
        public string CertificadoDigital { get; set; }
        [NotMapped]
        public string RubricaPath { get; set; } // Ruta del archivo de rúbrica
        [NotMapped]
        public string CertificadoPath { get; set; } // Ruta del archivo de certificado

    }
}
