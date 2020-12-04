/* This is the first view accessed when the application is executed. It shows a list of farmers registered in the database.
   The view is linked to add a new farmer, edit a farmer and delete a farmer.
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Farmer.Web.Models
{
    public class FarmerViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }
    }
}
