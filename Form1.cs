using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Bitnet.Client;

namespace LitecoinDustSettle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(DateTime.Now.ToString() +": Get List Started");
            try
            {
            var p = getConnection().ListUnspent();
            DataTable tab1 = new DataTable();
            tab1.Columns.Add("txid");
            //vout
            tab1.Columns.Add("vout", typeof(int));
            //scriptPubKey
            tab1.Columns.Add("scriptPubKey");
            //amount
            tab1.Columns.Add("amount", typeof(double));
            //confirmations
            tab1.Columns.Add("confirmations", typeof(int));
            //Priority
            tab1.Columns.Add("priority", typeof(double));
            int i = 0;
            while (i < p.Count())
            {
                DataRow dr = tab1.NewRow();
                dr["txid"] = (String)p[i]["txid"];
                dr["vout"] = (int)p[i]["vout"];
                dr["scriptPubKey"] = (String)p[i]["scriptPubKey"];
                dr["amount"] = (double)p[i]["amount"];
                dr["confirmations"] = (int)p[i]["confirmations"];

                dr["priority"] = ((double)p[i]["amount"]*100000000*(int)p[i]["confirmations"])/(1*148+2*34+10+1);
                tab1.Rows.Add(dr);
      
                //tab1.Rows.Add((String)p[i]["txid"]);
                //int n=dataGridView1.Rows.Add();
                //dataGridView1.Rows[n].Cells["txid"].Value = p[i]["txid"];
                i++;
            }
            tab1.DefaultView.Sort = "priority DESC";
            //dataSet1.Tables.Add(tab1);
            //Console.WriteLine(dataSet1.GetXml());
            dataGridView1.DataSource = tab1;
            textBox1.Text = p.ToString();
            }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }
            listBox1.Items.Add(DateTime.Now.ToString() +": Get List End");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            listBox1.Items.Add(DateTime.Now.ToString() +": Transaction Check Started");
            try
            {
            int i = 0;
            double sum = 0;
            int size_bytes = 0;
            double amtsum = 0;
            while (i < dataGridView1.SelectedRows.Count)
            {
                double inputVal=(double)
                dataGridView1.SelectedRows[i].Cells["amount"].Value*100000000;
                amtsum += inputVal / 100000000;
                int age = (int)dataGridView1.SelectedRows[i].Cells["confirmations"].Value;
                sum += inputVal * age;
                i++;
            }
            size_bytes = dataGridView1.SelectedRows.Count * 148 + 2 * 34 + 10 + dataGridView1.SelectedRows.Count;
            label1.Text = (sum / size_bytes).ToString();
            label2.Text = size_bytes.ToString();
            label3.Text = (((sum / size_bytes) > 57600000) && (size_bytes<10000)).ToString();
            label9.Text = (amtsum * 0.985).ToString();
            label17.Text = (amtsum * 0.015).ToString();
                 }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }

            listBox1.Items.Add(DateTime.Now.ToString() +": Transaction Check End");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            listBox1.Items.Add(DateTime.Now.ToString() +": Create Transaction Started");
            try
            {
            int i = 0;
            double sum = 0;
            //int size_bytes = 0;
            List<String[]> trxIn = new List<String[]>(); 
            while (i < dataGridView1.SelectedRows.Count)
            {
                String[] trx = new String[2];
                trx[0]=(String)dataGridView1.SelectedRows[i].Cells["txid"].Value;
                trx[1]=((int)dataGridView1.SelectedRows[i].Cells["vout"].Value).ToString();
                sum+=(double)
                dataGridView1.SelectedRows[i].Cells["amount"].Value;
                trxIn.Add(trx);
                i++;
            }

            textBox3.Text=
            getConnection().CreateRawTransaction(trxIn, textBox4.Text, sum);
                 }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }
            listBox1.Items.Add(DateTime.Now.ToString() +": Create Transaction End");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(DateTime.Now.ToString() +": Sign Transaction Started");
            try
            {
            textBox5.Text=
            getConnection().SignRawTransaction(textBox3.Text)["hex"].ToString();
                 }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }
            listBox1.Items.Add(DateTime.Now.ToString() +": Sign Transaction Ended");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(DateTime.Now.ToString() +": Unlock Wallet Started");
            try
            {
                getConnection().UnlockWallet(textBox6.Text, 60);
            }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }
            listBox1.Items.Add(DateTime.Now.ToString() +": Unlock Wallet End");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            listBox1.Items.Add(DateTime.Now.ToString() +": Send Transaction Started");
            try
            {
                getConnection().SendRawTransaction(textBox5.Text);
            }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }

            listBox1.Items.Add(DateTime.Now.ToString() +": Send Transaction End");
        }
        private string getUserName()
        {
            return textBox7.Text;
        }
        private string getPassword()
        {
            return textBox8.Text;
        }
        BitnetClient getConnection()
        {
            BitnetClient bc = new BitnetClient(textBox10.Text);
            bc.Credentials = new NetworkCredential(getUserName(), getPassword());
            return bc;
        }
        private void button7_Click(object sender, EventArgs e)
        {

            listBox1.Items.Add(DateTime.Now.ToString() +": Lock Wallet Started");
            try
            {
                getConnection().Lockwallet();
            }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }

            listBox1.Items.Add(DateTime.Now.ToString() +": Lock Wallet End");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(DateTime.Now.ToString() +": Check Connection Started");
            try
            {

                MessageBox.Show(getConnection().GetInfo().ToString());
            }
            catch
            {
                MessageBox.Show("An Error Occoured, Please check inputs.");
            }
            listBox1.Items.Add(DateTime.Now.ToString() +": Check Connection End");
        }
    }
}
