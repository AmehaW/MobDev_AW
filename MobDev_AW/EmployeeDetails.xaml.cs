namespace MobDev_AW;

public partial class EmployeeDetails : ContentPage
{
    public EmployeeDetails(Employee employee)
    {
        InitializeComponent();
        BindingContext = employee;
    }
}