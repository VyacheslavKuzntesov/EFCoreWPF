using EFCoreWPF.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

namespace EFCoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _db = new AppDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        ~MainWindow()
        {
            _db.Dispose();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //await using var db = new AppDbContext();
            //await db.Database.EnsureCreatedAsync();
            //await db.Students.AddAsync(new Student() { 
            //    Name = "Иммануил Кант",Birthday = DateTime.Today 
            //});
            //await db.Students.AddAsync(new Student()
            //{
            //    Name = "Лев Толстой",
            //    Birthday = DateTime.Today.AddDays(-1)
            //});
            //await db.SaveChangesAsync();

            //var students = await db.Students.ToListAsync();
            //studentsDataGrid.ItemsSource = students;

            List<Student> students = await _db.Students.ToListAsync();
            studentsDataGrid.ItemsSource = students;

        }

        private async void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            await _db.SaveChangesAsync();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Student> students = await _db.Students.ToListAsync();
            studentsDataGrid.ItemsSource = await _db.Students.ToListAsync();
            visitsDataGrid.ItemsSource = await _db.Visits.Include(visit => visit.Student).ToListAsync();

            groupDataGrid.ItemsSource = await _db.Groups.ToArrayAsync();
        }

        private async void studentsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Email = ""
            };
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            e.NewItem = student;
        }

        private async void visitsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var visit = new Visit()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Student = (Student)studentsDataGrid.SelectedItem
            };
            await _db.Visits.AddAsync(visit);
            await _db.SaveChangesAsync();
            e.NewItem = visit;
        }

        private async void visitsDataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                var selectedItem = visitsDataGrid.SelectedItem as Visit;
                _db.Remove(selectedItem!);
                await _db.SaveChangesAsync();
                e.CanExecute = true;
            }
        }

        private async void groupDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var group = new Group()
            {
                Id = Guid.NewGuid(),
                Name = "",
                CreatedAt = DateTime.Now,
            };
            await _db.Groups.AddAsync(group);
            await _db.SaveChangesAsync();
            e.NewItem = group;
        }

        private async void groupDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            studentsDataGrid.ItemsSource = await _db.Students.Include(it => it.Group).ToListAsync();
        }
    }
}
