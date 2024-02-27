using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable ToDoList = new DataTable();

        bool isEditing = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Create Columns
            ToDoList.Columns.Add("Title");
            ToDoList.Columns.Add("Description");

            //Point our datagridview to our datasource
            ToDoDatagrid.DataSource = ToDoList;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            TitleBox.Text = "";
            DescBox.Text = "";
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            isEditing = true;

            if (ToDoDatagrid.CurrentCell != null && ToDoDatagrid.CurrentCell.RowIndex < ToDoList.Rows.Count)
            {
                //Fill text fields with data from table
                TitleBox.Text = ToDoList.Rows[ToDoDatagrid.CurrentCell.RowIndex].ItemArray[0].ToString();
                DescBox.Text = ToDoList.Rows[ToDoDatagrid.CurrentCell.RowIndex].ItemArray[1].ToString();
            }
            else
            {
                MessageBox.Show("Düzenlenecek kayıt yok.");
                isEditing = false;
            }
            
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ToDoDatagrid.CurrentCell != null && ToDoDatagrid.CurrentCell.RowIndex < ToDoList.Rows.Count)
                {
                    ToDoList.Rows[ToDoDatagrid.CurrentCell.RowIndex].Delete();
                }
                else
                {
                    MessageBox.Show("Silinecek kayıt yok.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silinecek kayıt yok." + ex);
            }

            //Clear fields
            TitleBox.Text = "";
            DescBox.Text = "";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                ToDoList.Rows[ToDoDatagrid.CurrentCell.RowIndex]["Title"] = TitleBox.Text;
                ToDoList.Rows[ToDoDatagrid.CurrentCell.RowIndex]["Description"] = DescBox.Text;
            }
            else
            {
               ToDoList.Rows.Add(TitleBox.Text, DescBox.Text);
            }

            //Clear fields
            TitleBox.Text = "";
            DescBox.Text = "";
            isEditing = false;
        }
    }
}
