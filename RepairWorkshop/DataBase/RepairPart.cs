//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepairWorkshop.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class RepairPart
    {
        public int Id { get; set; }
        public int RepairId { get; set; }
        public int PartId { get; set; }
    
        public virtual Part Part { get; set; }
        public virtual Repair Repair { get; set; }
    }
}