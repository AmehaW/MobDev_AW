using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MobDev_AW
{
    public partial class MainPage : ContentPage
    {
        //Database service to perform CRUD operations
        private DatabaseService _databaseServiceSQL;

        //CSV Database service to perform CRUD operations
        private DatabaseServiceCSV _databaseServiceCSV;

        private List<Student> _students;

        public MainPage()
        {
            InitializeComponent();

            //Initialize the database service for SQLite
            _databaseServiceSQL = new DatabaseService();

            //Initialize the database service for CSV
            _databaseServiceCSV = new DatabaseServiceCSV();

            //Manual clear
            //_databaseServiceSQL.ClearDatabase();

            //Load Students
            LoadStudentsAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load students from the database every time the page appears
            LoadStudentsAsync();
        }

        private async void UpdateStudent_Clicked(object sender, EventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).BindingContext;
            //Added CSV -> sends both database services 
            await Navigation.PushAsync(new UpdateStudent(selectedStudent, _databaseServiceSQL, _databaseServiceCSV));
        }

        private async void DeleteStudent_Clicked(object sender, EventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).BindingContext;
            bool result = await DisplayAlert("Delete Student", "Are you sure you want to delete this student?", "Yes", "No");

            if (result)
            {
                //SQLiteVersion
                await _databaseServiceSQL.DeleteStudentAsync(selectedStudent);

                //CSV Version
                //await _databaseServiceCSV.DeleteStudentAsync(selectedStudent);

                //await DisplayAlert("Delete Student", "You Deleted a student", "Ok");
                // Reload the students list after deletion
                LoadStudentsAsync();
            }
        }

        private async void ViewDetails_Clicked(object sender, EventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).BindingContext;
            await Navigation.PushAsync(new StudentDetails(selectedStudent));
        }

        private async void LoadStudentsAsync()
        {
            try
            {
                //SQLite Version
                _students = await _databaseServiceSQL.GetStudentsAsync();

                //await DisplayAlert("Loading Students", "Loading Check", "Ok");
                //CSV Version
                //_students = await _databaseServiceCSV.GetStudentsAsync();

                StudentListView.ItemsSource = _students;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        // Event handler for adding a new student
        //Visual bug, Consider moving to new Add Student page.
        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            var newStudent = new Student
            {
                GivenName = GivenNameEntry.Text,
                FamilyName = FamilyNameEntry.Text,
                Country = StudentCountry.Text,
                EnrollmentDate = EnrollmentDatePicker.Date,

                Phone = PhoneEntry.Text,
                Department = DepartmentEntry.Text,
                Street = StreetEntry.Text,
                City = CityEntry.Text,
                State = StateEntry.Text,
                ZipCode = ZipCodeEntry.Text
             
        };

            //SQLite Version
            await _databaseServiceSQL.AddStudentAsync(newStudent);

            //CSV Version
            //await _databaseServiceCSV.AddStudentAsync(newStudent);

            //Un-comment for pop-ups/troubleshooting
            //await DisplayAlert("Add Student","You Added a student","Ok");

            GivenNameEntry.Text = FamilyNameEntry.Text = StudentCountry.Text = string.Empty;
            LoadStudentsAsync();
            //Add UI refresh command here
        }
    }
}
