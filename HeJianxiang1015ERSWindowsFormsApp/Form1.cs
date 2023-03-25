using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeJianxiang1015ERSWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            InitalizeListControl();
        }

        private void EmployeeRecordsForm_Load(object sender, EventArgs e)
        {

        }

        private void PopulateTreeView()
        {
            statusBarPanel1.Tag = "refreshing employee code.please wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor= Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader=new XmlTextReader("D:\\hejianxiang\\MyRepos\\HeJianxiang1015ERSWindowsFormsApp\\HeJianxiang1015ERSWindowsFormsApp\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        String strval = reader.Value;

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();
                        }
                        TreeNode EcodeNode = new TreeNode(strval);
                        nodeCollection.Add(EcodeNode);

                    }
                }
                statusBarPanel1.Text = "click on an employee code to see their record.";
            }
            catch (XmlException ex){
                MessageBox.Show(ex.Message);

            }
        }
        protected void InitalizeListControl() {
        listView1.Clear();
            listView1.Columns.Add("Employee Name", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Jion", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Gread", 105, HorizontalAlignment.Left);
            listView1.Columns.Add("Slary", 105, HorizontalAlignment.Left);

        }
        protected void PopulateListView(TreeNode crrNode)
        {
            InitalizeListControl();
            XmlTextReader listRead = new XmlTextReader("D:\\hejianxiang\\MyRepos\\HeJianxiang1015ERSWindowsFormsApp\\HeJianxiang1015ERSWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr=new string[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if (strNodeName == strNodePath)
                {
                    ListViewItem lvi;
                    listRead.MoveToNextAttribute();
                    name=listRead.Value;
                    lvi = listView1.Items.Add(name);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj=listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute();
                    gread=listRead.Value;
                    lvi =listView1.Items.Add(gread);

                    listRead.MoveToNextAttribute();
                    sal=listRead.Value;
                    lvi=listView1.Items.Add(sal);

                    listRead.MoveToNextAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();
                }

            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode=e.Node;
            if(tvRootNode==currNode)
            {
                statusBarPanel1.Text = "double click the employee records.";
                return;
            }
            else
            {
                statusBarPanel1.Text = "click an employee code to view individual record";

            }
            PopulateListView(currNode);
        }
    }
}
