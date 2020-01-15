using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryProject.Models
{
    public class Item
    {
        //begin by declaring properties and variables

        [Display(Name = "Item Name")] [Required] public string itemName { get; set; }
        [Display(Name = "Item Number")] [Required] public string itemNum { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        [Required] public string Category { get; set; }

        [DataType(DataType.Currency)] [Range(0,500)] public double Price { get; set; }

        //constructors.
        public Item(string itemName, string itemNum, string manufacturer, string description, string category, double price)
        {
            this.itemName = itemName;
            this.itemNum = itemNum;
            this.Manufacturer = manufacturer;
            this.Description = description;
            this.Category = category;
            this.Price = price;
        }

        public Item()
        {
            this.itemName = "";
            this.itemNum = "0";
            this.Manufacturer = "";
            this.Description = "";
            this.Category = "";
            this.Price = 0.0;
        }


    }
}
