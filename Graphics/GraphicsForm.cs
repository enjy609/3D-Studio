using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
namespace Graphics
{
    
    public partial class GraphicsForm : Form
    {
     
     public static bool selected = false;
        Renderer renderer = new Renderer();
        Thread MainLoopThread;
        //float[] vertices;
        vertex v = new vertex();
        ListViewItem vert = new ListViewItem();
        int count = 0;
        public GraphicsForm()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            initialize();
            MainLoopThread = new Thread(MainLoop);
            MainLoopThread.Start();

        }
        void initialize()
        {
            renderer.Initialize();
            comboBox1.Items.Add("Triangles");
            comboBox1.Items.Add("Points");
            comboBox1.Items.Add("Lines");
            comboBox1.Items.Add("line Strip");
            comboBox1.Items.Add("Line Loop");
            comboBox1.Items.Add("Triangles strip");
            comboBox1.Items.Add("Triangle Fan");
            button4.Enabled = false;
            button2.Enabled = false;
        }
        
        void MainLoop()
        {
            while (true)
            {
                renderer.Update();
                renderer.Draw();
              simpleOpenGlControl1.Refresh(); ///........................................................................................
            }
        }
        private void GraphicsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderer.CleanUp();
            MainLoopThread.Abort();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw();
        }

        private void label46_Click(object sender, System.EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, System.EventArgs e)
        {

        }

        private void button11_Click(object sender, System.EventArgs e)
        {
           if (xVertex.Text.Length > 0 && yVertex.Text.Length > 0 && zVertex.Text.Length > 0 && xnor.Text.Length > 0 && ynor.Text.Length > 0 && znor.Text.Length > 0 &&RColor.Text.Length > 0 && GColor.Text.Length > 0 && BColor.Text.Length > 0) { 
            float x = float.Parse(xVertex.Text);
            float y = float.Parse(yVertex.Text);
            float z = float.Parse(zVertex.Text);
           vertex.vertices.Add(x);
           vertex.vertices.Add(y);
           vertex.vertices.Add(z);


           float r = float.Parse(RColor.Text);
           float g = float.Parse(GColor.Text);
           float b = float.Parse(BColor.Text);
               vertex.vertices.Add(r);
               vertex.vertices.Add(g);
               vertex.vertices.Add(b);


               float xNor = float.Parse(xnor.Text);
               float yNor = float.Parse(ynor.Text);
               float zNor = float.Parse(znor.Text);
               vertex.vertices.Add(xNor);
               vertex.vertices.Add(yNor);
               vertex.vertices.Add(zNor);
               listView4.Items.Add("vec" + count);
               //listView4             
               count++;

               //clear all textbox
               xVertex.Clear();
               yVertex.Clear();
               zVertex.Clear();
               RColor.Clear();
               GColor.Clear();
               BColor.Clear();
               xnor.Clear();
               ynor.Clear();
               znor.Clear();


           }


           else
           {
               MessageBox.Show("Please enter all data required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
           }
           
        }

        private void button2_Click(object sender, System.EventArgs e)
        {

        }

        private void textBox33_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void listView4_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            selected = true;
            button4.Enabled = true;
        }

        private void listView4_MouseClick(object sender, MouseEventArgs e)
        {
            xVertex.Clear();
            yVertex.Clear();
            zVertex.Clear();
            RColor.Clear();
            GColor.Clear();
            BColor.Clear();
            xnor.Clear();
            ynor.Clear();
            znor.Clear();

            int index = listView4.SelectedIndices[0];
            vertex.selectedIndex = index;
            //listView4.select
            //if (listView4.Items.Count > 0)
            //{
            //    listView4.Items[0].Selected = true;
            //    listView4.Select();

            //}
            //vertex.vertices
            
            int end = (index * 9) + 9;
            float[] data = new float[9];
            int ind = 0;
            for (int i = index * 9; i < end; i++)
            {

                data[ind] = vertex.vertices[i];
                ind++;
            }



            xVertex.Text = data[0].ToString();
            yVertex.Text = data[1].ToString();
            zVertex.Text = data[2].ToString();
            RColor.Text = data[3].ToString();
            GColor.Text = data[4].ToString();
            BColor.Text = data[5].ToString();
            xnor.Text = data[6].ToString();
            ynor.Text = data[7].ToString();
            znor.Text = data[8].ToString();
            //renderer.Draw();
            

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            xVertex.Clear();
            yVertex.Clear();
            zVertex.Clear();
            RColor.Clear();
            GColor.Clear();
            BColor.Clear();
            xnor.Clear();
            ynor.Clear();
            znor.Clear();
        }
        //xyz vertex ................................................................................
        private void xVertex_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8 )
            {
                e.Handled = true;
            }
        }

        private void yVertex_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }
        private void zVertex_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        // RGB Color....................................................................

        private void RColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void GColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }
        private void BColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }
       
        //xyzNormal.................................................................................
        private void xNormal_PressKey(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void yNor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void zNor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }
        //.................hhhhhhhhhhhhhhhhhhhhhhereeeeeeeeeeeeeeeeee..........................................................

        private void button16_Click(object sender, System.EventArgs e)
        {
            renderer.Draw();
            Renderer.click = true;
            string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            
            if (start_txt.Text.Length > 0 && count_txt.Text.Length > 0 && selectedItem.Length>0)
            {
                  vertex.start = int.Parse(start_txt.Text);
                  vertex.count = int.Parse(count_txt.Text);
                  vertex.primitive = selectedItem;
                  if (vertex.primitive == "Triangles")
                  {
                      listView5.Items.Add("Triangles");

                  }
                  else if (vertex.primitive == "Lines")
                  {

                      listView5.Items.Add("Lines");
                  }


                              }
            else
            {
                MessageBox.Show("Please enter the Start and the count of your mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }


        }

        private void simpleOpenGlControl1_Load(object sender, System.EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
        private void BColor_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            
            
            int startLoop = (vertex.selectedIndex * 9) + 9;
            int endLoop = (vertex.selectedIndex * 9);
            for (int i = startLoop-1; i >= endLoop; i--)
            {
                vertex.vertices.RemoveAt(i);
                
           }
            count--;
            for (int i = listView4.Items.Count - 1; i >= 0; i--)
            {
                if (listView4.Items[i].Selected)
                {
                    listView4.Items[i].Remove();
                }
            }
            button4.Enabled = false;
            xVertex.Clear();
            yVertex.Clear();
            zVertex.Clear();
            RColor.Clear();
            GColor.Clear();
            BColor.Clear();
            xnor.Clear();
            ynor.Clear();
            znor.Clear();

        }

       

        

        

    }
}
