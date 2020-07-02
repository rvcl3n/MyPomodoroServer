using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("pomodoro")]
    public class Pomodoro
    {
        [Key]
        [Column("PomodoroId")]
        public Guid Id { get; set; }

        public string StartTime { get; set; }

        public string FinishTime { get; set; }

        public string Description { get; set; }
    }
}
