namespace Lab1;

public class Student : EntityBase
{
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual char Sex { get; set; }
    public virtual int Year { get; set; }
    public virtual Group Group { get; set; }
}
