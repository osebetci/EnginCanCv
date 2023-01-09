using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Dto.Shared
{
    public class CanPageActivate
    {
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public bool IsForbidden { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public string BreadCrumb { get; set; }
        public bool IsAuthority { get; set; }
    }

    public class MenuPage
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string AllRouterLink { get; set; }
        public bool Forbidden { get; set; }
        public bool MenuShow { get; set; }
    }
}
