using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Dto.Systems
{
    public class OrganizasyonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public bool? IsBirimMudur { get; set; }
        public string Code { get; set; }
        public short NumberOfStaff { get; set; }
    }
}
