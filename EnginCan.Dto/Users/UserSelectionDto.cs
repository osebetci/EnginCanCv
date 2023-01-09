using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Dto.Users
{
    /// <summary>
    /// Select box için userleri döndürür
    /// </summary>
    public class UserSelectionDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
    }
}
