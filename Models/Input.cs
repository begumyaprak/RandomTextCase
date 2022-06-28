using System;
using System.ComponentModel.DataAnnotations;

namespace RandomTextCase.Models


{
	public class Input
	{
         public int TextID { get; set; }

         [Required(ErrorMessage ="please type a value")]
         [StringLength(2,ErrorMessage ="Please write between 1-50")]
         public string? inputText { get; set; }
    }
}

