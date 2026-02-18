using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}