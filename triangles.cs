using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Triangle {
    public float x1, x2, x3; 
    public float y1, y2, y3; 
    public float angle;
}

public partial class AnimForm : Form {

    private Timer timer = new Timer();
    private float elapsedTime;
    
    public Graphics formGraphics;
    public SolidBrush myBrush;
    public Pen myPen;
    
    public Triangle tri;
    public IDictionary<int, Triangle> triDict = new Dictionary<int, Triangle>();
    public int i, j;

    public AnimForm() {
        
        this.ClientSize = new System.Drawing.Size(480, 320);
        formGraphics = this.CreateGraphics();
        myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        myPen = new Pen(myBrush);
        
        i = 0;
        j = 0;

        
        tri = new Triangle();
        tri.x1 = 50.0f;
        tri.x2 = 80.0f;
        tri.x3 = 100.0f;
        tri.y1 = 100.0f;
        tri.y2 = 50.0f;
        tri.y3 = 80.0f;
        tri.angle = 2.0f;
        triDict.Add(0, tri);
        
        tri = new Triangle();
        tri.x1 = 80.0f;
        tri.x2 = 110.0f;
        tri.x3 = 130.0f;
        tri.y1 = 130.0f;
        tri.y2 = 80.0f;
        tri.y3 = 110.0f;
        tri.angle = 2.0f;
        triDict.Add(1, tri);
        
        tri = new Triangle();
        tri.x1 = 110.0f;
        tri.x2 = 140.0f;
        tri.x3 = 160.0f;
        tri.y1 = 160.0f;
        tri.y2 = 110.0f;
        tri.y3 = 140.0f;
        tri.angle = 2.0f;
        triDict.Add(2, tri);
               
        timer.Enabled = true;
        timer.Tick += RotatingTriangles;
        timer.Interval = 10;
        timer.Start();
        
    }

    private void rotate(int i) {        
        Triangle tri = triDict[i];        
        double a1, b1, a2, b2, a3, b3;        
        float p = tri.x1;
        float q = tri.y1;         
        float angle = (tri.angle * 3.14f) / 180; // Deg 2 Rad       
        a1 = p + (tri.x1-p) * Math.Cos(angle)-(tri.y1-q) * Math.Sin(angle);
        b1 = q + (tri.x1-p) * Math.Sin(angle)+(tri.y1-q) * Math.Cos(angle);
        a2 = p + (tri.x2-p) * Math.Cos(angle)-(tri.y2-q) * Math.Sin(angle);
        b2 = q + (tri.x2-p) * Math.Sin(angle)+(tri.y2-q) * Math.Cos(angle);
        a3 = p + (tri.x3-p) * Math.Cos(angle)-(tri.y3-q) * Math.Sin(angle);
        b3 = q + (tri.x3-p) * Math.Sin(angle)+(tri.y3-q) * Math.Cos(angle);        
        tri.x1 = (float)a1;
        tri.y1 = (float)b1;        
        tri.x2 = (float)a2;
        tri.y2 = (float)b2;        
        tri.x3 = (float)a3;
        tri.y3 = (float)b3;       
        triDict[i] = tri;    
    }

    private void RotatingTriangles( object sender, System.EventArgs e ) {           
            formGraphics.Clear(Color.Black);            
            elapsedTime = 0.01f;           
            while(i<triDict.Count) {    
                rotate(i);                
                PointF[] points = { 
                    new PointF(triDict[i].x1, triDict[i].y1), 
                    new PointF(triDict[i].x2, triDict[i].y2), 
                    new PointF(triDict[i].x3, triDict[i].y3) 
                };                
                formGraphics.DrawPolygon(new Pen(Color.Red), points);
                i++;                 
            }        
            i=0;           
    }

    [System.STAThread]
    public static void Main() {
        Application.EnableVisualStyles();
        Application.Run( new AnimForm() );
    }
}