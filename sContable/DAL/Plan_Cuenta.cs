//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sContable.DAL
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Plan_Cuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plan_Cuenta()
        {
            this.Asiento_Contable = new HashSet<Asiento_Contable>();
            this.Cuentas = new HashSet<Cuenta>();
        }
    
        public int Cod { get; set; }
        public string nombre { get; set; }
        public Nullable<int> id_empresa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Asiento_Contable> Asiento_Contable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Cuenta> Cuentas { get; set; }
        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }
    }
}
