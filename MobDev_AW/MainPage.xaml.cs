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

        private List<Employee> _employee;

        public MainPage()
        {
            InitializeComponent();

            //Initialize the database service for SQLite
            _databaseServiceSQL = new DatabaseService();

            //Initialize the database service for CSV
            _databaseServiceCSV = new DatabaseServiceCSV();

            //Manual clear
            //_databaseServiceSQL.ClearDatabase();

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           
        }

        
       

    }
}
