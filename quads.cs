using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Quad {
    public float x; 
    public float y; 
    public float w; 
    public float h;
    public int xdir;
    public int ydir;
    public string color;
}

public partial class AnimForm : Form {

    private Timer timer = new Timer();
    private float elapsedTime;
    
    public Graphics formGraphics;
    public SolidBrush myBrush;
    public Pen myPen;

    public Quad quad;
    public IDictionary<int, Quad> quadDict = new Dictionary<int, Quad>();
    public int i, j;

    public AnimForm() {
        
        this.ClientSize = new System.Drawing.Size(480, 320);
        formGraphics = this.CreateGraphics();
        myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        myPen = new Pen(myBrush);
        
        i = 0;
        j = 0;

        
        quad = new Quad();
        quad.x = 10.0f;
        quad.y = 20.0f;
        quad.w = 50.0f;
        quad.h = 50.0f;
        quad.xdir = 1;
        quad.ydir = 1;
        quad.color = "white";
        quadDict.Add(0, quad);
        
        quad = new Quad();
        quad.x = 180.0f;
        quad.y = 180.0f;
        quad.w = 50.0f;
        quad.h = 50.0f;
        quad.xdir = 0;
        quad.ydir = 1;
        quad.color = "white";
        quadDict.Add(1, quad);
        
        quad = new Quad();
        quad.x = 220.0f;
        quad.y = 240.0f;
        quad.w = 50.0f;
        quad.h = 50.0f;
        quad.xdir = 1;
        quad.ydir = 0;
        quad.color = "white";
        quadDict.Add(2, quad);
        
        quad = new Quad();
        quad.x = 70.0f;
        quad.y = 80.0f;
        quad.w = 30.0f;
        quad.h = 30.0f;
        quad.xdir = 0;
        quad.ydir = 1;
        quad.color = "white";
        quadDict.Add(3, quad);
        
        quad = new Quad();
        quad.x = 90.0f;
        quad.y = 100.0f;
        quad.w = 20.0f;
        quad.h = 20.0f;
        quad.xdir = 0;
        quad.ydir = 0;
        quad.color = "white";
        quadDict.Add(4, quad);
               
        timer.Enabled = true;
        timer.Tick += DancingQuads;
        timer.Interval = 10;
        timer.Start();
        
    }

    private void DancingQuads( object sender, System.EventArgs e ) {
        
            //ticks++;
            
            formGraphics.Clear(Color.Black);
            
            elapsedTime = 2.5f;
            
            while(i<quadDict.Count) {
                
                /* Move everything */
                if(quadDict[i].xdir == 1) quadDict[i].x += elapsedTime; 
                if(quadDict[i].xdir == 0) quadDict[i].x -= elapsedTime; 
                if(quadDict[i].ydir == 1) quadDict[i].y += elapsedTime; 
                if(quadDict[i].ydir == 0) quadDict[i].y -= elapsedTime;
                
                /* Collision detection */
                while(j<quadDict.Count) {                   
                    if(j==i) { j++; continue; }                    
                    if(
                        (quadDict[i].x <= quadDict[j].x + quadDict[j].w && quadDict[i].x + quadDict[i].w >= quadDict[j].x) 
                        && 
                        (quadDict[i].y <= quadDict[j].y + quadDict[j].h && quadDict[i].y + quadDict[i].h >= quadDict[j].y)
                    ) {                            
                        
                        quadDict[i].color = quadDict[j].color = "red";
                        
                        /*
                        if( quadDict[i].x <= quadDict[j].x + quadDict[j].w ) {
                            if(quadDict[i].xdir==0) quadDict[i].xdir = 1;
                            else quadDict[i].xdir = 0;
                        }
                        if( quadDict[i].y <= quadDict[j].y + quadDict[j].h ) {
                            if(quadDict[i].ydir==0) quadDict[i].ydir = 1;
                            else quadDict[i].ydir = 0;
                        }
                        */
                        
                    }  
                    j++;                    
                }
                j = 0;                
                
                /* Out of view detection */
                if(quadDict[i].xdir == 1) {                    
                    if(quadDict[i].x+quadDict[i].w >= 480.0) quadDict[i].xdir = 0;                   
                } else if(quadDict[i].xdir == 0) {                    
                    if(quadDict[i].x <= 1.0) quadDict[i].xdir = 1;                 
                }            
                if(quadDict[i].ydir == 1) {                    
                    if(quadDict[i].y+quadDict[i].h >= 320.0) quadDict[i].ydir = 0;               
                } else if(quadDict[i].ydir == 0) {                    
                    if(quadDict[i].y <= 1.0) quadDict[i].ydir = 1;                
                } 
                
                if(quadDict[i].color == "red") formGraphics.FillRectangle(Brushes.Red, quadDict[i].x, quadDict[i].y, quadDict[i].w, quadDict[i].h);
                else formGraphics.FillRectangle(Brushes.White, quadDict[i].x, quadDict[i].y, quadDict[i].w, quadDict[i].h);
                
                quadDict[i].color = "white";
                
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