using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("user")]
    public class User : IEntity
    {
        [Key]
        [Column("UserId")]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string FisrtName { get; set; }

        public string PhotoURL { get; set; }

        public ICollection<Pomodoro> Pomodoros { get; set; }
    }
}
