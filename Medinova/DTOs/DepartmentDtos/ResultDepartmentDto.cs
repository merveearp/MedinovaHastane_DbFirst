using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.DepartmentDtos
{
    public class ResultDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int DoctorCount { get; set; }

    }
}