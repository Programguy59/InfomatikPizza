using InfomatikPizza.Tables;
using System.Collections.Generic;
using System.Linq;

namespace informatikPizza.Tables.Database;

public static partial class Database
{
    public static readonly List<Address> Address = new();

    public static Address? GetAddressById(int id)
    {
        return Address.FirstOrDefault(Address => Address.Id == id);
    }
    
    public static List<Address> GetAllAddress()
    {
        return Address;
    }
}