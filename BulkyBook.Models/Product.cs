using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models;

public class Product
{
    public int  Id { get; set; }
    [Required]
    public string Title  { get; set; }

    public string Description { get; set; }
    [Required]
    public string  ISBN { get; set; }
    [Required]
    public string  Author { get; set; }
    [Required]
    [Range(1, 10000)]
    [Display(Name = "List Price")]
    public double ListPrice { get; set; }
    
    [Required]
    [Range(1,10000)]
    public double Price { get; set; }
    [Required]
    [Range(1,10000)]
    public double Price50 { get; set; }
    [Required]
    [Range(1,10000)]
    public double  Price100 { get; set; }
    [ValidateNever]
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    [ValidateNever]
    public Category Category { get; set; }
    
    public int CoverTypeId { get; set; }
    [ValidateNever]
    public CoverType CoverType { get; set; }
}