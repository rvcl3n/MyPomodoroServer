﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("pomodoro")]
    public class Pomodoro : IEntity
    {
        [Key]
        [Column("PomodoroId")]
        public Guid Id { get; set; }

        public string StartTime { get; set; }

        public string FinishTime { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
