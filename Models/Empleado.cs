using System;
using System.Collections.Generic;

namespace Perfiles_SA.Models
{
    public partial class Empleado
    {
        public string Dpi { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; } = null!;
        public DateTime FechaIngresoEmpresa { get; set; }
        public string? Direccion { get; set; }
        public string? Nit { get; set; }
        public string DepartamentoAsignado { get; set; } = null!;
        public int? Edad { get; set; }
    }
}
