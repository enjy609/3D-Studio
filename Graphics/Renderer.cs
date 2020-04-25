using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;

using System.IO;

namespace Graphics
{
    class Renderer
    {
        vertex rendererVer;
        public static bool click = false;
        Shader sh;
        uint vertexBufferID;
        uint vertexBufferID1;
        //3D Drawing
        int MVPID;
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;
        mat4 MVP;
        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            Gl.glClearColor(0.0f, 0.0f, 0.4f, 1);

            float[] verts = { 
                 

		        //Axis
		        //x
		        0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, //R
                0.0f,0.0f,0.0f,
		        5.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, //R
                0.0f,0.0f,0.0f,
		        //y
	            0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, //G
                0.0f,0.0f,0.0f,
		        0.0f, 5.0f, 0.0f,
                0.0f, 1.0f, 0.0f, //G
                0.0f,0.0f,0.0f,
		        //z
	            0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f,  //B
                0.0f,0.0f,0.0f,
		        0.0f, 0.0f, -5.0f,
                0.0f, 0.0f, 1.0f,  //B
                0.0f,0.0f,0.0f,
            };
            
            
            vertexBufferID = GPU.GenerateBuffer(verts);
            List<mat4> Transformations = new List<mat4>();
            Transformations.Add(glm.scale(new mat4(1), new vec3(1, 2, 1)));
            Transformations.Add(glm.rotate(-30.0f / 180.0f * 3.127f, new vec3(0, 1, 0)));
            ModelMatrix = MathHelper.MultiplyMatrices(Transformations);
            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            
            // View matrix 
            ViewMatrix = glm.lookAt(
                    new vec3 (0,5,5),
                    new vec3 (0,2,0),
                    new vec3 (0,1,0)
                );

            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
            List<mat4> mvp = new List<mat4>();
            mvp.Add(ModelMatrix);
            mvp.Add(ViewMatrix);
            mvp.Add(ProjectionMatrix);
            MVP = MathHelper.MultiplyMatrices(mvp);
           
            // Model matrix: apply transformations to the model

            // Our MVP matrix which is a multiplication of our 3 matrices 


            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            MVPID = Gl.glGetUniformLocation(sh.ID, "mvp");
            //pass the value of the MVP you just filled to the vertex shader
            Gl.glUniformMatrix4fv(MVPID, 1, Gl.GL_FALSE, MVP.to_array());
        }

        public void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            GPU.BindBuffer(vertexBufferID);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 9*sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 9 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 9 * sizeof(float), (IntPtr)(6 * sizeof(float)));
            Gl.glDrawArrays(Gl.GL_LINES, 0, 6 );

            if (click == true)// draw is clickeds
            {
                vertexBufferID1 = GPU.GenerateBuffer(vertex.vertices.ToArray());
                GPU.BindBuffer(vertexBufferID1);
                Gl.glEnableVertexAttribArray(0);
                Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 9 * sizeof(float), (IntPtr)0);
                Gl.glEnableVertexAttribArray(1);
                Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 9 * sizeof(float), (IntPtr)(3 * sizeof(float)));
                Gl.glEnableVertexAttribArray(2);
                Gl.glVertexAttribPointer(2, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 9 * sizeof(float), (IntPtr)(6 * sizeof(float)));
                if (vertex.primitive == "Triangles")
                {
                    Gl.glDrawArrays(Gl.GL_TRIANGLES, vertex.start, vertex.count);

                }
                else if (vertex.primitive == "Lines")
                {
                    Gl.glDrawArrays(Gl.GL_LINES, vertex.start, vertex.count);

                }
                //Gl.glDrawArrays(Gl.GL_LINES, 6, 6);
                if (GraphicsForm.selected == true)
                {
                    Gl.glDrawArrays(Gl.GL_POINTS, vertex.selectedIndex, 1);
                    Gl.glPointSize(8);

                }
            }
            


            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            Gl.glDisableVertexAttribArray(2);
        }
        public void Update()
        {
        }
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
