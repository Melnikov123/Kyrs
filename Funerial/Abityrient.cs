//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Funerial
{
    using System;
    using System.Collections.Generic;
    
    public partial class Abityrient
    {
        public Abityrient()
        {
            this.Documents = new HashSet<Documents>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Data_spach { get; set; }
    
        public virtual ICollection<Documents> Documents { get; set; }
    }
}