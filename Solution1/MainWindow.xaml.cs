using Microsoft.EntityFrameworkCore;
using Solution1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solution1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PRN221_Spr22Context _context;
        public MainWindow(PRN221_Spr22Context context)
        {
            InitializeComponent();
            _context = context;
            HandleBeforeLoad();


        }
        private Employee GetEmployeeObject()
        {
            Employee employee = null;
            try
            {
                employee = new Employee
                {
                    Id = string.IsNullOrEmpty(employeeID.Text)
                        ? 0
                        : int.Parse(employeeID.Text),
                    Name = employeeName.Text,
                    Gender = male.IsChecked == true ? "Male" : "Female",
                    Phone = phone.Text,
                    Dob = dob.SelectedDate,
                    Idnumber = idNumber.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get employee");
            }
            return employee;
        }
        private void UpdateGridView()
        {
            listEmployee.ItemsSource = null;
            listEmployee.ItemsSource = _context.Employees.ToList();
        }
        private void HandleBeforeLoad()
        {
            UpdateGridView();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var gender = ((Employee)item).Gender;
                if (!string.IsNullOrEmpty(gender))
                {
                    if (gender.ToLower() == "female")
                    {
                        female.IsChecked = true;
                    }
                    else
                    {
                        male.IsChecked = true;
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            employeeID.Text = string.Empty;
            employeeName.Text = string.Empty;
            male.IsChecked = true;
            phone.Text = string.Empty;
            dob.Text = string.Empty;
            idNumber.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emplopyee = GetEmployeeObject();
                emplopyee.Id = 0;
                _context.Employees.Add(emplopyee);
                _context.SaveChanges();
                UpdateGridView();
                MessageBox.Show($"{emplopyee.Name} insert succesfully", "Insert Employee");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Employee");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emplopyee = GetEmployeeObject();
                var oldObj = _context.Employees.FirstOrDefault(x => x.Id == emplopyee.Id);
                if (oldObj != null)
                {
                    oldObj.Dob = emplopyee.Dob;
                    oldObj.Phone = emplopyee.Phone;
                    oldObj.Name = emplopyee.Name;
                    oldObj.Idnumber = emplopyee.Idnumber;
                    oldObj.Gender = emplopyee.Gender;
                    _context.Employees.Update(oldObj);
                    _context.SaveChanges();
                    UpdateGridView();
                    MessageBox.Show($"{emplopyee.Name} insert succesfully", "Edit Employee");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Edit Employee");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employeeId = employeeID.Text;
                if (string.IsNullOrEmpty(employeeId))
                {
                    MessageBox.Show("Please choose employee to delete");
                }
                else
                {
                    Employee emplopyee = GetEmployeeObject();
                    var oldObj = _context.Employees.FirstOrDefault(x => x.Id == emplopyee.Id);
                    if (oldObj != null)
                    {
                        _context.Employees.Remove(oldObj);
                        _context.SaveChanges();
                        UpdateGridView();
                        MessageBox.Show(
                            $"{emplopyee.Name} delete succesfully",
                            "Delete Employee"
                        );
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Employee");
            }
        }
    }
}
