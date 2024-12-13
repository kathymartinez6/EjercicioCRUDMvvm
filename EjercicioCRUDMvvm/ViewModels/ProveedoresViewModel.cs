
using EjercicioCRUDMvvm.Data;
using EjercicioCRUDMvvm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioCRUDMvvm.ViewModels
{

    public class ProveedoresViewModel : INotifyPropertyChanged
    {
        private Models.ProveedorDbContext _context;
        public ObservableCollection<Proveedor> Proveedores { get => proveedores; set => proveedores = value; }
        public Command AgregarProveedorCommand { get; }

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _direccion;
        public string Direccion
        {
            get => _direccion;
            set
            {
                if (_direccion != value)
                {
                    _direccion = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _telefono;
        public string Telefono
        {
            get => _telefono;
            set
            {
                if (_telefono != value)
                {
                    _telefono = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _email;
        private ObservableCollection<Proveedor> proveedores;

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProveedoresViewModel()
        {
            _context = new Models.ProveedorDbContext();
            _context.Database.EnsureCreated();
            Proveedores = new ObservableCollection<Proveedor>(_context.Proveedores);
            AgregarProveedorCommand = new Command(AgregarProveedor);
        }

        private void AgregarProveedor()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Direccion) || string.IsNullOrWhiteSpace(Telefono) || string.IsNullOrWhiteSpace(Email))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            var nuevoProveedor = new Proveedor { Nombre = Nombre, Direccion = Direccion, Telefono = Telefono, Email = Email };
            _context.Proveedores.Add(nuevoProveedor);
            _context.SaveChanges();

            Proveedores.Add(nuevoProveedor);
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            Nombre = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            Email = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



