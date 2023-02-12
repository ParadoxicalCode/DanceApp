using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DanceApp.Model.Data
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string Name { get; set; }

        public List<Judge> Judge { get; } = new();
    }
}
