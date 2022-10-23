﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author {  get; set; }

        [Required]
        [Range(1,10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 01-50 books")]
        public double Price { get; set; } //final price

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 50-100 books")]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        //mapping the category to the product to get the foreign key
        [Required]
        [Display(Name = "Category")]
        public int CategoryId {get; set; } //FK

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }


        //CoverType
        [Required]
        [Display(Name="Cover Type")]
        public int CoverTypeId { get; set; } //FK

        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
