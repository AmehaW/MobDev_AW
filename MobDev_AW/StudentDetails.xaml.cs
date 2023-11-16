namespace MobDev_AW;

public partial class StudentDetails : ContentPage
{
    public StudentDetails(Student student)
    {
        InitializeComponent();
        BindingContext = student;
    }
}