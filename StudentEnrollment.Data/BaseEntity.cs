namespace StudentEnrollment.DATA;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CrearedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime ModifyDate { get; set; }
    public string ModifyBy { get; set; } = string.Empty;
}
