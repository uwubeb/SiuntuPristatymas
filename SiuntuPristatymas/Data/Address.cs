using Siuntos.Data.Models;
using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data;

public class Address : BaseEntity
{
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostCode { get; set; }
    
    public List<Parcel> Parcels { get; set; }
    
}