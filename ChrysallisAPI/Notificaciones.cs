//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChrysallisAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notificaciones
    {
        public int id { get; set; }
        public System.DateTime fechaHora { get; set; }
        public Nullable<short> idEvento { get; set; }
    
        public virtual Eventos Eventos { get; set; }
    }
}
