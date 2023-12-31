﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SpecialCharactersTest.Models
{
    [Table("TestTable")]
    public partial class TestTable
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string TestColumnMedium { get; set; }
        [StringLength(1000)]
        public string TestColumnLong { get; set; }
        public bool? TestColumnBit { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TestColumnDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TestColumnDateTime { get; set; }
    }
}