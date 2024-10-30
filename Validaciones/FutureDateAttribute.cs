namespace Gestion_Fimas_Digitales.Validaciones
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Obtiene el valor de la propiedad 'FechaEmision' en el contexto de validación
            var fechaEmisionProperty = validationContext.ObjectType.GetProperty("FechaEmision");
            if (fechaEmisionProperty == null)
            {
                return new ValidationResult("La propiedad FechaEmision no se encontró.");
            }

            // Obtiene el valor de 'FechaEmision'
            var fechaEmisionValue = (DateTime?)fechaEmisionProperty.GetValue(validationContext.ObjectInstance);
            var fechaVencimiento = (DateTime?)value;

            // Verifica si la fecha de vencimiento es mayor que la fecha de emisión
            if (fechaVencimiento.HasValue && fechaEmisionValue.HasValue)
            {
                if (fechaVencimiento.Value <= fechaEmisionValue.Value)
                {
                    return new ValidationResult(ErrorMessage ?? "La fecha de vencimiento debe ser mayor que la fecha de emisión.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
