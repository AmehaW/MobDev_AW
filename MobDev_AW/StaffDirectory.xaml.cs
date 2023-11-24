namespace MobDev_AW;

public partial class StaffDirectory : ContentPage
{

    //Database service to perform CRUD operations
    private DatabaseService _databaseServiceSQL;

    //CSV Database service to perform CRUD operations
    private DatabaseServiceCSV _databaseServiceCSV;

    private List<Employee> _employee;



    public StaffDirectory()
	{
		InitializeComponent();

        //Initialize the database service for SQLite
        _databaseServiceSQL = new DatabaseService();

        //Initialize the database service for CSV
        _databaseServiceCSV = new DatabaseServiceCSV();

        //Manual clear
        //_databaseServiceSQL.ClearDatabase();

        //Load Employee
        LoadEmployeeAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Load employees from the database every time the page appears
        LoadEmployeeAsync();
    }

    private async void UpdateEmployee_Clicked(object sender, EventArgs e)
    {
        var selectedEmployee = (Employee)((Button)sender).BindingContext;
        //Added CSV -> sends both database services 
        await Navigation.PushAsync(new UpdateEmployee(selectedEmployee, _databaseServiceSQL, _databaseServiceCSV));
    }

    private async void DeleteEmployee_Clicked(object sender, EventArgs e)
    {
        var selectedEmployee = (Employee)((Button)sender).BindingContext;
        bool result = await DisplayAlert("Delete Employee", "Are you sure you want to delete this Employee?", "Yes", "No");

        if (result)
        {
            //SQLiteVersion
            await _databaseServiceSQL.DeleteEmployeeAsync(selectedEmployee);

            //CSV Version
            //await _databaseServiceCSV.DeleteEmployeeAsync(selectedEmployee);

            //await DisplayAlert("Delete Employee", "You Deleted an employee", "Ok");
            // Reload the employees list after deletion
            LoadEmployeeAsync();
        }
    }


    private async void ViewDetails_Clicked(object sender, EventArgs e)
    {
        var selectedEmployee = (Employee)((Button)sender).BindingContext;
        await Navigation.PushAsync(new EmployeeDetails(selectedEmployee));
    }


    private async void LoadEmployeeAsync()
    {
        try
        {
            //SQLite Version
            _employee = await _databaseServiceSQL.GetEmployeeAsync();

            //await DisplayAlert("Loading Employee", "Loading Check", "Ok");
            //CSV Version
            //_employee = await _databaseServiceCSV.GetEmployeeAsync();

            EmployeeListView.ItemsSource = _employee;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
        }
    }

    // Event handler for adding a new employee
    //Visual bug, Consider moving to new Add Employee page.
    //private async void AddEmployee_Clicked(object sender, EventArgs e)
    //{
    //    var newEmployee = new Employee
    //    {
    //        GivenName = GivenNameEntry.Text,
    //        FamilyName = FamilyNameEntry.Text,
    //        Phone = PhoneEntry.Text,
    //        Department = DepartmentEntry.Text,
    //        Street = StreetEntry.Text,
    //        City = CityEntry.Text,
    //        State = StateEntry.Text,
    //        ZipCode = ZipCodeEntry.Text,
    //        Country = Country.Text,
    //        EnrollmentDate = EnrollmentDatePicker.Date

    //    };

        ////SQLite Version
        //await _databaseServiceSQL.AddEmployeeAsync(newEmployee);

        ////CSV Version
        ////await _databaseServiceCSV.AddEmployeeAsync(newEmployee);

        ////Un-comment for pop-ups/troubleshooting
        ////await DisplayAlert("Add Employee","You Added an Employee","Ok");

        //GivenNameEntry.Text = FamilyNameEntry.Text = PhoneEntry.Text = DepartmentEntry.Text =
        //    StreetEntry.Text = CityEntry.Text = StateEntry.Text = ZipCodeEntry.Text = Country.Text = string.Empty;
        //LoadEmployeeAsync();
        ////Add UI refresh command here
    //}

    // trial for image
    private async void UploadPic_Clicked(object sender, EventArgs e)
    {

    }

    private async void DeletePic_Clicked(object sender, EventArgs e)
    {

    }


}