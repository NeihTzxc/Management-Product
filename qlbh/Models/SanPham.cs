//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace qlbh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.CTHDs = new HashSet<CTHD>();
        }
    
        public System.Guid MaSP { get; set; }
        public string TenSP { get; set; }
        public string Donvitinh { get; set; }
        public Nullable<decimal> Dongia { get; set; }
        public Nullable<System.Guid> MaLoaiSP { get; set; }
        public string HinhSP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        public virtual LoaiSP LoaiSP { get; set; }
    }
}
