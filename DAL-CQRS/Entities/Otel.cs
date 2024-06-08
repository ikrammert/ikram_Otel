using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Entities
{
    public class Otel
    {
        public Guid OtelId { get; set; }
        [Required(ErrorMessage = "Hotel name is required.")]
        public string Name { get; set; }
        [Required]
        public required string OtelAdress { get; set; }
        public string? OtelWebSite { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; } = null;

        public Otel()
        {
            CreationTime = DateTime.Now;
        }
    }

    
}
