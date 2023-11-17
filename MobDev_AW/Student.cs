// Student.cs
using SQLite;

public class Student
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string StudentNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public string Phone { get; set; }
    public string Department { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string State { get; set; }
    public string ZipCode { get; set; }

}
