using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data;

public class ParcelHistory : BaseEntity
{
    public DateTime Time { get; set; }
    public ParcelStatusEnum Status { get; set; }
    
    public int ParcelId { get; set; }
    public Parcel Parcel { get; set; }
}