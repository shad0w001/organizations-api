using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.DTOs.OrganizationDTOs
{
    public class OrganizationRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Founded { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public int NumberOfEmployees { get; set; }
    }
}
