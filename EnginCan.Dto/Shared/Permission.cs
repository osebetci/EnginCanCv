using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Dto.Shared
{
    public class CustomPostPagePermission
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public bool Forbidden { get; set; }
        public List<int> PageIds { get; set; }
    }

    public class CustomPostPathPermission
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public bool Forbidden { get; set; }
        public List<int> PathIds { get; set; }
    }
}
