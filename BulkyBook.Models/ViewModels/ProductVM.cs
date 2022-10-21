using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BulkyBook.Models.ViewModels
{
    public class ProductVM
    {


        //-------------------------------------------------------Create product Category and CoverType using PROJECTIONS------------------------------------------
        //having unit of work, we can have access to every DB in the program. SelectedListItem helps us access a dropdown
        //---------------Old method using viewBags -----------------------------------------------

        //IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
        //    u => new SelectListItem
        //    { //text and values are properties of select 
        //        Text = u.Name,
        //        Value = u.Id.ToString()
        //    });
        //IEnumerable<SelectListItem> CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
        //   u => new SelectListItem
        //   { //text and values are properties of select 
        //       Text = u.Name,
        //       Value = u.Id.ToString()
        //   });

        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set;}
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set;}
    }
}
