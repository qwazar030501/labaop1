using System;

namespace WarehouseInventory.Models.Enums
{
    [Flags]
    public enum MaterialCategory
    {
        None = 0,
        Raw = 1,          
        Consumable = 2,   
        Component = 4,    
        Packaging = 8     
    }
}