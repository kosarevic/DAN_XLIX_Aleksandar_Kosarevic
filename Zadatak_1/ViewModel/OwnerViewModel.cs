using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class OwnerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OwnerViewModel> Personnel { get; set; }

        private OwnerViewModel row;

        public OwnerViewModel Row
        {
            get { return row; }
            set
            {
                if (row != value)
                {
                    row = value;
                    OnPropertyChanged("Row");
                }
            }
        }

        public OwnerViewModel()
        {
            FillList();
        }

        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select * from tblEmploye join tblManager", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Personnel == null)
                    Personnel = new ObservableCollection<OwnerViewModel>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Personnel.Add(new OwnerWindowModel
                    {
                        Manager = new Manager
                        {

                        },
                        Employe = new Employe
                        {

                        }
                    });
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
