//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepairRequest
{
    using System;
    using System.Collections.Generic;
    
    public partial class RepairRequests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RepairRequests()
        {
            this.RequestComments = new HashSet<RequestComments>();
        }
    
        public int RequestID { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string EquipmentName { get; set; }
        public string FaultType { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string Status { get; set; }
        public string AssignedEmployee { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestComments> RequestComments { get; set; }
    }
}
