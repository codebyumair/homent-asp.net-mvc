//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class tblHouse
    {

        public int HouseID { get; set; }

        [DisplayName("Owner id")]
        [Required(ErrorMessage = "Please enter owner id")]
        public int OwnerID { get; set; }

        [DisplayName("House no")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter house no.")]
        public int HouseNo { get; set; }

        [DisplayName("House name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter house name")]
        public string HouseName { get; set; }

        [DisplayName("Type of house")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select house type")]
        public string Type { get; set; }

        [DisplayName("Room(s)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select no. of room")]
        public string Room { get; set; }

        [DisplayName("Floor area")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter floor area")]
        public int FloorArea { get; set; }

        [DisplayName("Status")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter house status")]
        public string Status { get; set; }

        [DisplayName("Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        [DisplayName("Image")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose an image")]
        public string ImagePath { get; set; }

        [DisplayName("Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter address")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter state")]
        public string State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter city")]
        public string City { get; set; }

        [DisplayName("Rent per month")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter rent amount")]
        public int Rent { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter deposit amount")]
        public int Deposit { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}