using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data;

public class ParcelHistory : BaseEntity
{
    public TimeSpan Time { get; set; }
    
    //Change to enum
    public string Status { get; set; }
    
    public int ParcelId { get; set; }
    public Parcel Parcel { get; set; }
}