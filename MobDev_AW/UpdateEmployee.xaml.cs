namespace MobDev_AW;

public partial class UpdateEmployee : ContentPage
{
    private Employee _selectedEmployee;
    private DatabaseService _databaseServiceSQL;
    private DatabaseServiceCSV _databaseServiceCSV;

    public UpdateEmployee(Employee selectedEmployee, DatabaseService databaseService, DatabaseServiceCSV databaseServiceCSV) //DatabaseService databaseService
    {
        InitializeComponent();
        //Pass employee
        _selectedEmployee = selectedEmployee;

        //Pass database servicer
        //SQLite Passed
        _databaseServiceSQL = databaseService;

        // Populate the input fields with the existing employee details
        GivenNameEntry.Text = _selectedEmployee.GivenName;
        FamilyNameEntry.Text = _selectedEmployee.FamilyName;
        PhoneEntry.Text = _selectedEmployee.Phone;
        DepartmentEntry.Text = _selectedEmployee.Department;
        StreetEntry.Text = _selectedEmployee.Street;
        CityEntry.Text = _selectedEmployee.City;
        StateEntry.Text = _selectedEmployee.State;
        ZipCodeEntry.Text = _selectedEmployee.ZipCode;
        CountryEntry.Text = _selectedEmployee.Country;
        
        //CSV Version
        //CSV Passed
        _databaseServiceCSV = databaseServiceCSV;
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        // Update the selected student's information
        _selectedEmployee.GivenName = GivenNameEntry.Text;
        _selectedEmployee.FamilyName = FamilyNameEntry.Text;
        _selectedEmployee.Phone = PhoneEntry.Text;
        _selectedEmployee.Department = DepartmentEntry.Text;
        _selectedEmployee.Street = StreetEntry.Text;
        _selectedEmployee.City = CityEntry.Text;
        _selectedEmployee.State = StateEntry.Text;
        _selectedEmployee.ZipCode = ZipCodeEntry.Text;
        _selectedEmployee.Country = CountryEntry.Text;
       
        // SQLite Version
        // Call the database service to update the emploee
        await _databaseServiceSQL.UpdateEmployeeAsync(_selectedEmployee);

        //CSV Version
        //await _databaseServiceCSV.UpdateEmployeeAsync(_selectedEmployee);
        //await DisplayAlert("Update Employee", "You Updated a employee", "Ok");

        // Navigate back to the previous page
        await Navigation.PopAsync();
    }
}