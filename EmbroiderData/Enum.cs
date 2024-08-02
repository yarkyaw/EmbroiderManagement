using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData
{ 
    public enum Gender
    {
        None,
        Female,
        Male,
    }
    public enum MaterialType
    {
        Gold = 5,
        Jewel = 10, // 0x0000000A
    }

    public enum OrderType
    {
        Shop = 5,
        Ordered = 10, // 0x0000000A
        Jewellery = 15, // 0x0000000F
    }

    public enum Status
    {
        Saved = 5,
        Processing = 10, // 0x0000000A
        Completed = 15, // 0x0000000F
        Void = 20, // 0x00000014
    }

    public enum EmbroiderInvoiceDetailType
    {
        Processing = 5,
        Retrun = 10, // 0x0000000A
        Completed = 15, // 0x0000000F
        Void = 20, // 0x00000014
    }

public enum ActiveStatus
{
    Active = 5,
    InActive = 10,
}
}
