﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ActiveDirectoryManagement_API.Models.Domain.DB;

namespace ActiveDirectoryManagement_API.Models.Domain.Document
{
    public class DocumentPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PasswordId { get; set; }

        //[ForeignKey("EmpCode")]
        [StringLength(20)]
        public string EmpCode { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        //[ForeignKey("StatusCode")]

        [StringLength(20)]
        public string StatusCode { get; set; }

        [StringLength(20)]
        public string? ApproveEmpCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ApproveDate { get; set; }


        [ForeignKey(nameof(EmpCode))]
        public DbEmployee? DbEmployee { get; set; }

        [ForeignKey(nameof(StatusCode))]
        public DbStatus? DbStatus { get; set; }

    }
}
