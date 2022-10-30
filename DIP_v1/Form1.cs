using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP_v1
{
    public partial class step : Form
    {
        //Golbal varience
        Bitmap openImg;
        Bitmap Img_stack;
        //Stack
        Stack<Bitmap> Img_temp = new Stack<Bitmap>();
        Stack<string> num_function = new Stack<string>();
        int num_step;
        
        bool Isundo = false;
        //Image struct Define
        public struct Image_temp
        {
            public Bitmap Image;
            public int step;
        }
        //Creat Form
        public step()
        {
            InitializeComponent();
            clear();
        }
        //clear function
        public void clear()
        {
            
            histogram_before_chart.Visible = false;
            histogram_after_chart.Visible = false;
            histogram_after_chart_R.Visible = false;
            histogram_after_chart_G.Visible = false;
            histogram_after_chart_B.Visible = false;
            num_step = 0;
        }        
        //clearall
        private void clearall_Click(object sender, EventArgs e)
        {
            clear();
        }        
        //Open File
        private void openfile_Click(object sender, EventArgs e)
        {
            // 選擇我們需要開檔的類型
            openFileDialog1.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File (.jpg)|*.jpg";
            //Opem Initial
            pictureBox1.Visible = true;
            label1.Visible = true;
            label1.Text = "Waiting...";
            function.Visible = true;
            function.Text = num_step + "steps";
            
            // 如果成功開檔  
            if ( openFileDialog1.ShowDialog() == DialogResult.OK )
            {
                // 宣告存取影像的 bitmap
                openImg = new Bitmap(openFileDialog1.FileName);
                // 讀取的影像放到 pictureBox     
                pictureBox1.Image = openImg;
                label1.Text = "Source";
            }
                                   
        }
        //Save File
        private void savefile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";
           
            //pictureBox2.Image.Save(@"Path", System.Drawing.Imaging.ImageFormat.Jpeg);

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox2.Image.Save(sfd.FileName);
                //openImg.Save(sfd.FileName);
            }
        }
        //problem 1
        private void problem1_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            function.Visible = true;
            function.Text = "Waiting...";
            R_button.Visible = true;
            G_button.Visible = true;
            B_button.Visible = true;
            gray.Visible = true;
        }
        //RGB-R Extraction
        private void rgb_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                //若開啟失敗
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    //若是沒有經過處理，以原圖存入
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    //先將上一步完成的圖片存入stack
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("RGB B");
                    Console.WriteLine("Img_B", num_step);
                }
                //讀取pictureBox1的Image
                Bitmap Img = new Bitmap(pictureBox1.Image);
                //讀取Bitmap的RGB
                Bitmap Img_R = new Bitmap(Img);
                //RGB Pixel處理
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        //讀取影像平面上(x,y)的RGB資訊
                        Color RGB = Img.GetPixel(x,y);
                        //RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue
                        Img_R.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.R, RGB.R));
                    }
                }
                                
                pictureBox1.Visible = true;
                label1.Visible = true;

                pictureBox2.Visible = true;
                pictureBox2.Image = Img_R;
                function.Text = "RGB R";
                
            }
        }
        //RGB-G Extraction
        private void G_button_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                //若開啟失敗
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("RGB G");
                    Console.WriteLine("Img_G", num_step);
                }
                //讀取pictureBox1的Image
                Bitmap Img = new Bitmap(pictureBox1.Image);
                //讀取Bitmap的RGB
                Bitmap Img_G = new Bitmap(Img);
                //RGB Pixel處理
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        //讀取影像平面上(x,y)的RGB資訊
                        Color RGB = Img.GetPixel(x, y);
                        //RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue                        
                        Img_G.SetPixel(x, y, Color.FromArgb(RGB.G, RGB.G, RGB.G));

                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;

                pictureBox2.Visible = true;
                pictureBox2.Image = Img_G;
                function.Text = "RGB G";

                
                
            }
        }
        //RGB-B Extraction
        private void B_button_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                //若開啟失敗
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("Img_B");
                    Console.WriteLine("Img_B", num_step);
                }
                //讀取pictureBox1的Image
                Bitmap Img = new Bitmap(pictureBox1.Image);
                //讀取Bitmap的RGB
                Bitmap Img_B = new Bitmap(Img);
                //RGB Pixel處理
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        //讀取影像平面上(x,y)的RGB資訊
                        Color RGB = Img.GetPixel(x, y);
                        //RGB 是 VS 內建的 class 可以直接讀取影像的色彩資訊 R = Red, G = Green, B =Blue                        
                        Img_B.SetPixel(x, y, Color.FromArgb(RGB.B, RGB.B, RGB.B));

                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;

                pictureBox2.Visible = true;
                pictureBox2.Image = Img_B;
                function.Text = "RGB B";
                
                
            }
        }
        //1-Gray
        private void gray_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                //若開啟失敗
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("Gray");
                    Console.WriteLine("Gray", num_function, num_step);
                }
                //讀取pictureBox1的Image
                Bitmap Img = new Bitmap(pictureBox1.Image);
                //建立Gray圖片
                Bitmap Img_Gray = new Bitmap(Img);
                //Gray Pixel處理
                for (int j = 0; j < Img_Gray.Height; j++)
                {
                    for (int i = 0; i < Img_Gray.Width; i++)
                    {
                        int gray = (int)(
                            Img_Gray.GetPixel(i, j).R * 0.299 +
                            Img_Gray.GetPixel(i, j).G * 0.587 +
                            Img_Gray.GetPixel(i, j).B * 0.114);
                        Color color = Color.FromArgb(gray, gray, gray);
                        Img_Gray.SetPixel(i, j, color);
                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Visible = true;
                pictureBox2.Image = Img_Gray;
                function.Text = "Gray";

                
            }
        }              
        private void undo_Click(object sender, EventArgs e)
        {
            //拿出Stack                  
            try
            {
                pictureBox2.Image = Img_temp.Pop();
                num_step--;
                step_num.Text = num_step + " step";
                Console.WriteLine(num_function.Pop());

            }
            catch (InvalidOperationException) 
            {
                // Take some action.
                step_num.Text = num_step + " step is null !!";
                MessageBox.Show("There is no image undo!");
                
            }            
            Console.WriteLine(num_step);

        }        
        private void mean_button_Click(object sender, EventArgs e)
        {
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("mean_filter");
                    Console.WriteLine("mean_filter", num_function, num_step);
                }
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap mean_filter = new Bitmap(Img);
                //Bitmap median_filter = new Bitmap(Img);
                //Mean filter
                //以3*3的 「遮罩」為例，透過「Spatial Convolution」對整張影像進行處理。
                //取得遮罩之像素矩陣後加總除以總數(計算平均值)。                
                int sum_red;
                int sum_green;
                int sum_blue;
                //Single-Dimensional Arrays
                //int[] pix_red = new int[9];
                //int[] pix_green = new int[9];
                //int[] pix_blue = new int[9];
                //for each pixel
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        //for Kernal:9宮格
                        const int total = 9;
                        sum_red = 0;
                        sum_green = 0;
                        sum_blue = 0;                        
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                //若該Pixel在圖片外，皆為補0
                                if (((x + i) < 0) || ((x + i) >= Img.Width) || ((y + j) < 0) || ((y + j) >= Img.Height))
                                {
                                    sum_red += 0;
                                    sum_green += 0;
                                    sum_blue += 0;
                                    //total -= 1;                                    
                                }
                                else
                                {
                                    Color RGB = Img.GetPixel(x + i, y + j);
                                    sum_red += RGB.R;
                                    sum_green += RGB.G;
                                    sum_blue += RGB.B;
                                }
                            }
                        }
                        sum_red = ((sum_red / total) > 255) ? 255 : (sum_red / total);
                        sum_green = ((sum_green / total) > 255) ? 255 : (sum_green / total);
                        sum_blue = ((sum_blue / total) > 255) ? 255 : (sum_blue / total);

                        mean_filter.SetPixel(x, y, Color.FromArgb(sum_red, sum_green, sum_blue));
                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = mean_filter;
                pictureBox2.Visible = true;
                function.Text = "Mean filter";
                function.Visible = true;
                
            }
        }

        private void median_button_Click(object sender, EventArgs e)
        {
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("median_filter");
                    Console.WriteLine("median_filter", num_function, num_step);
                }
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap median_filter = new Bitmap(Img);
                //Median_filter
                //以3*3的 「遮罩」為例，透過「Spatial Convolution」對整張影像進行處理。
                //取得遮罩之像素矩陣後透過排序演算法排序後，取其中間的像素值。               
                //Single-Dimensional Arrays
                //每個kernal的RGB所包含的9個值，再取中間值[4]
                int[] pix_red = new int[9];
                int[] pix_green = new int[9];
                int[] pix_blue = new int[9];
                //for each pixel
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        //9宮格檢查
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                if (((x + i) < 0) || ((x + i) >= Img.Width) || ((y + j) < 0) || ((y + j) >= Img.Height))
                                {
                                    pix_red[(i + 1) + (j + 1) * 3] = 0;
                                    pix_green[(i + 1) + (j + 1) * 3] = 0;
                                    pix_blue[(i + 1) + (j + 1) * 3] = 0;
                                    //total -= 1;
                                }
                                else
                                {
                                    Color RGB = Img.GetPixel(x + i, y + j);
                                    pix_red[(i + 1) + (j + 1) * 3] = RGB.R;
                                    pix_green[(i + 1) + (j + 1) * 3] = RGB.G;
                                    pix_blue[(i + 1) + (j + 1) * 3] = RGB.B;
                                }
                            }
                        }
                        //排序每個Kernal的RGB通道
                        Array.Sort(pix_red);
                        Array.Sort(pix_green);
                        Array.Sort(pix_blue);
                        //取中間值
                        median_filter.SetPixel(x, y, Color.FromArgb(pix_red[4], pix_green[4], pix_blue[4]));
                    }
                }
                
                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";
                
                pictureBox2.Image = median_filter;
                pictureBox2.Visible = true;
                function.Text = "Median filter";
                function.Visible = true;               

            }
        }
        //Histogram
        private void histogram_button_Click(object sender, EventArgs e)
        {
            //清除畫面
            clear();
            histogram_before_chart.Visible = true;
            histogram_after_chart.Visible = true;
            histogram_after_chart_R.Visible = true;
            histogram_after_chart_G.Visible = true;
            histogram_after_chart_B.Visible = true;
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("median_filter");
                    Console.WriteLine("median_filter", num_function, num_step);
                }

                //清除原有的histogram
                histogram_before_chart.Series[0].Points.Clear();
                histogram_after_chart.Series[0].Points.Clear();
                //所需物品
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap histogram_filter = new Bitmap(Img);
                //pixel = 0~255
                int max = -1;
                int min = 256;
                int max_R = -1;
                int min_R = 256;
                int max_G = -1;
                int min_G = 256;
                int max_B = -1;
                int min_B = 256;
                int[] pixel_count = new int[256];
                int[] pixel_count_R = new int[256];
                int[] pixel_count_G = new int[256];
                int[] pixel_count_B = new int[256];
                int[] cdf = new int[256];
                int[] pdf = new int[256];
                int[] histogram = new int[256];
                int[] cdf_R = new int[256];
                int[] pdf_R = new int[256];
                int[] histogram_R = new int[256];
                int[] cdf_G = new int[256];
                int[] pdf_G = new int[256];
                int[] histogram_G = new int[256];
                int[] cdf_B = new int[256];
                int[] pdf_B = new int[256];
                int[] histogram_B = new int[256];
                //將array歸零
                for (int i = 0; i < 256; i++)
                {
                    pixel_count[i] = 0;
                    pixel_count_R[i] = 0;
                    pixel_count_G[i] = 0;
                    pixel_count_B[i] = 0;
                }
                for (int i = 0; i < 256; i++)
                {
                    histogram[i] = 0;
                    histogram_R[i] = 0;
                    histogram_G[i] = 0;
                    histogram_B[i] = 0;
                }
                //利用 Histogram 算出各個RGB的 PDF
                //for each pixel
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color RGB = Img.GetPixel(x, y);
                        //取Gray
                        int pixel = (int)(
                            RGB.R * 0.299 +
                            RGB.G * 0.587 +
                            RGB.B * 0.114);
                        pixel_count[pixel]++;
                        //找最大最小
                        if (pixel < min)
                            min = pixel;
                        if (pixel > max)
                            max = pixel;
                        //取R
                        int pixel_R = (int)RGB.R;                        
                        pixel_count_R[pixel_R]++;
                        //找最大最小
                        if (pixel_R < min_R)
                            min_R = pixel_R;
                        if (pixel_R > max_R)
                            max_R = pixel_R;
                        //取G
                        int pixel_G = (int)RGB.G;
                        pixel_count_G[pixel_G]++;
                        //找最大最小
                        if (pixel_G < min_G)
                            min_G = pixel_G;
                        if (pixel_G > max_G)
                            max_G = pixel_G;
                        //取B
                        int pixel_B = (int)RGB.B;
                        pixel_count_B[pixel_B]++;
                        //找最大最小
                        if (pixel_B < min_B)
                            min_B = pixel_B;
                        if (pixel_B > max_B)
                            max_B = pixel_B;
                    }
                }
                //將PDF 做累加求出 CDF
                //C(r)把小於等於r的P(r)相加，譬如C(3) = P(0) + P(1) + P(2) + P(3)
                for (int i = 0; i < 256; i++)
                {
                    if( i == 0)
                    {
                        cdf[0] = pixel_count[0];
                        histogram_before_chart.Series[0].Points.AddXY(0, pixel_count[0]);
                    }
                    else
                    {
                        cdf[i] = cdf[i- 1] + pixel_count[i];
                        histogram_before_chart.Series[0].Points.AddXY(i, pixel_count[i]);
                    }                    
                }
                for (int i = 0; i < 256; i++)
                {
                    if (i == 0)
                    {
                        cdf_R[0] = pixel_count_R[0];
                        //histogram_before_chart.Series[0].Points.AddXY(0, pixel_count_R[0]);
                    }
                    else
                    {
                        cdf_R[i] = cdf_R[i - 1] + pixel_count_R[i];
                        //histogram_before_chart.Series[0].Points.AddXY(i, pixel_count_R[i]);
                    }
                }
                for (int i = 0; i < 256; i++)
                {
                    if (i == 0)
                    {
                        cdf_G[0] = pixel_count_G[0];
                        histogram_before_chart.Series[0].Points.AddXY(0, pixel_count_G[0]);
                    }
                    else
                    {
                        cdf_G[i] = cdf_G[i - 1] + pixel_count_G[i];
                        histogram_before_chart.Series[0].Points.AddXY(i, pixel_count_G[i]);
                    }
                }
                for (int i = 0; i < 256; i++)
                {
                    if (i == 0)
                    {
                        cdf_B[0] = pixel_count_B[0];
                        histogram_before_chart.Series[0].Points.AddXY(0, pixel_count_B[0]);
                    }
                    else
                    {
                        cdf_B[i] = cdf_B[i - 1] + pixel_count_B[i];
                        histogram_before_chart.Series[0].Points.AddXY(i, pixel_count_B[i]);
                    }
                }
                //將CDF 的結果 4捨5入後做出對照表
                for (int i = 0; i < 256; i++)
                {
                    pdf[i] = (int)Math.Round((((double)(cdf[i] - cdf[min]) / (cdf[max] - cdf[min])) * 255));
                    pdf_R[i] = (int)Math.Round((((double)(cdf_R[i] - cdf_R[min_R]) / (cdf_R[max_R] - cdf_R[min_R])) * 255));
                    pdf_G[i] = (int)Math.Round((((double)(cdf_G[i] - cdf_G[min_G]) / (cdf_G[max_G] - cdf_G[min_G])) * 255));
                    pdf_B[i] = (int)Math.Round((((double)(cdf_B[i] - cdf_B[min_B]) / (cdf_B[max_B] - cdf_B[min_B])) * 255));
                    Console.WriteLine(pdf[i]);
                    Console.WriteLine(pdf_R[i]);
                    Console.WriteLine(pdf_G[i]);
                    Console.WriteLine(pdf_B[i]);
                }
                //透過查詢剛剛建立的對照表，決定Transition 完各個灰階值的機率
                //建立平方化後的圖片
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color RGB = Img.GetPixel(x, y);
                        //取灰階
                        int pixel = (int)(
                            RGB.R * 0.299 +
                            RGB.G * 0.587 +
                            RGB.B * 0.114);
                        
                        histogram_filter.SetPixel(x, y, Color.FromArgb(pdf[pixel], pdf[pixel], pdf[pixel]));
                        histogram[pdf[pixel]]++;
                        histogram_R[pdf_R[(int)RGB.R]]++;
                        histogram_G[pdf_G[(int)RGB.G]]++;
                        histogram_B[pdf_B[(int)RGB.B]]++;
                    }
                }
                //劃出histogram到chart上
                for (int i = 0; i < 256; i++)
                {                       
                    histogram_after_chart.Series[0].Points.AddXY(i, histogram[i]);
                    histogram_after_chart_R.Series[0].Points.AddXY(i, histogram_R[i]);
                    histogram_after_chart_G.Series[0].Points.AddXY(i, histogram_G[i]);
                    histogram_after_chart_B.Series[0].Points.AddXY(i, histogram_B[i]);
                }
                
                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = histogram_filter;
                pictureBox2.Visible = true;
                function.Text = "Histogram filter";
                function.Visible = true;

            }
        }
        //User-defined Threshold
        private void thresholding_button_Click(object sender, EventArgs e)
        {
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("threshold");
                    Console.WriteLine("threshold", num_function, num_step);
                }
                //建立圖片
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap threshold_img = new Bitmap(Img);
                int threshold_value = 100;
                int pixel;
                //Threshold
                //string threshold_value = threshold_textBox.Text;
                try
                {
                    threshold_value = Int32.Parse(threshold_textBox.Text);
                    Console.WriteLine(threshold_value);
                }
                catch (FormatException)
                {
                    //若輸入的數值無法轉成整數失敗
                    MessageBox.Show("Your input number is unable top parse into integer! \n Please input integer");
                    Console.WriteLine($"Unable to parse '{threshold_textBox.Text}'");
                }
                //for each pixel
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color RGB = Img.GetPixel(x, y);
                        //取Gray
                            pixel = (int)(
                            RGB.R * 0.299 +
                            RGB.G * 0.587 +
                            RGB.B * 0.114);
                        Console.WriteLine(pixel);
                        if( pixel > threshold_value)
                        {
                            pixel = 255;
                        }
                        else
                        {
                            pixel = 0;
                        }                        
                        threshold_img.SetPixel(x, y, Color.FromArgb(pixel, pixel, pixel));
                    }
                }

                

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = threshold_img;
                pictureBox2.Visible = true;
                function.Text = "Threshold";
                function.Visible = true;

            }
        }
        //sobel vertical
        //https://soubhihadri.medium.com/image-processing-best-practices-c-part-2-c0988b2d3e0c
        private void vertical_button_Click(object sender, EventArgs e)
        {
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("sobel_vertical");
                    Console.WriteLine("sobel vertical", num_function, num_step);
                }
                //建立圖片
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap sobel_vertical_img = new Bitmap(Img);
                //Sobel edge
                int[] sobel_x = new int[9] { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
                int[] sobel_y = new int[9] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
                
                //Convolution : for each pixel
                for (int y = 0; y < Img.Height; y++)//行
                {
                    for (int x = 0; x < Img.Width; x++)//列
                    {
                        //sobel_x的結果
                        int Gx = 0;
                        int Gy = 0;
                        
                        //pixel的9宮格
                        for (int j = -1; j <= 1; j++)//行
                        {
                            for (int i = -1; i <= 1; i++)//列
                            {
                                //外框
                                if( ((x + i) < 0) || ((y + j) < 0) || ((x + i) >= Img.Width) || ((y + j) >= Img.Height))
                                {
                                    int pixel = 0;
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }
                                //內部
                                else
                                {
                                    //每個pixel的RGB像素
                                    Color RGB = Img.GetPixel(x + i, y + j);
                                    //取Gray
                                    int pixel = (int)(RGB.R * 0.299 + RGB.G * 0.587 + RGB.B * 0.114);
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }
                                
                            }
                        }

                        Gx = Math.Abs(Gx);
                        Gy = Math.Abs(Gy);
                        Console.WriteLine(Gx);
                        if (Gx < 0)
                            Gx = 0;
                        else if (Gx > 255)
                            Gx = 255;

                        if (Gy < 0)
                            Gy = 0;
                        else if (Gy > 255)
                            Gy = 255;

                        sobel_vertical_img.SetPixel(x, y, Color.FromArgb(Gx, Gx, Gx));
                    }
                }

                

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = sobel_vertical_img;
                pictureBox2.Visible = true;
                function.Text = "Threshold";
                function.Visible = true;

            }
        }
        //Sobel horizontal
        private void horizontal_button_Click(object sender, EventArgs e)
        {
            //https://soubhihadri.medium.com/image-processing-best-practices-c-part-2-c0988b2d3e0c
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("sobel_horizontal");
                    Console.WriteLine("sobel horizontal", num_function, num_step);
                }
                //建立圖片
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap sobel_horizontal_img = new Bitmap(Img);
                //Sobel edge
                int[] sobel_x = new int[9] { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
                int[] sobel_y = new int[9] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };

                //Convolution : for each pixel
                for (int y = 0; y < Img.Height; y++)//行
                {
                    for (int x = 0; x < Img.Width; x++)//列
                    {
                        //sobel_x的結果
                        int Gx = 0;
                        int Gy = 0;

                        //pixel的9宮格
                        for (int j = -1; j <= 1; j++)//行
                        {
                            for (int i = -1; i <= 1; i++)//列
                            {
                                //外框
                                if (((x + i) < 0) || ((y + j) < 0) || ((x + i) >= Img.Width) || ((y + j) >= Img.Height))
                                {
                                    int pixel = 0;
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }
                                //內部
                                else
                                {
                                    //每個pixel的RGB像素
                                    Color RGB = Img.GetPixel(x + i, y + j);
                                    //取Gray
                                    int pixel = (int)(RGB.R * 0.299 + RGB.G * 0.587 + RGB.B * 0.114);
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }

                            }
                        }
                        //Sobel Filter through Y axis
                        Gx = Math.Abs(Gx);
                        Gy = Math.Abs(Gy);
                        Console.WriteLine(Gy);

                        if (Gy < 0)
                            Gy = 0;
                        else if (Gy > 255)
                            Gy = 255;

                        sobel_horizontal_img.SetPixel(x, y, Color.FromArgb(Gy, Gy, Gy));
                    }
                }
                               
                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = sobel_horizontal_img;
                pictureBox2.Visible = true;
                function.Text = "sobel_horizontal";
                function.Visible = true;

            }
        }
        //Sobel combined
        private void combined_button_Click(object sender, EventArgs e)
        {
            //https://soubhihadri.medium.com/image-processing-best-practices-c-part-2-c0988b2d3e0c
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("sobel_combined");
                    Console.WriteLine("sobel combined", num_function, num_step);
                }
                //建立圖片
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap sobel_combined_img = new Bitmap(Img);
                //Sobel edge
                int[] sobel_x = new int[9] { 1, 0, -1,
                                             2, 0, -2,
                                             1, 0, -1 };
                int[] sobel_y = new int[9] { 1, 2, 1,
                                             0, 0, 0,
                                            -1, -2, -1 };

                //Convolution : for each pixel
                for (int y = 0; y < Img.Height; y++)//行
                {
                    for (int x = 0; x < Img.Width; x++)//列
                    {
                        //sobel_x的結果
                        int Gx = 0;
                        int Gy = 0;

                        //pixel的9宮格
                        for (int j = -1; j <= 1; j++)//行
                        {
                            for (int i = -1; i <= 1; i++)//列
                            {
                                //外框
                                if (((x + i) < 0) || ((y + j) < 0) || ((x + i) >= Img.Width) || ((y + j) >= Img.Height))
                                {
                                    int pixel = 0;
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }
                                //內部
                                else
                                {
                                    //每個pixel的RGB像素
                                    Color RGB = Img.GetPixel(x + i, y + j);
                                    //取Gray
                                    int pixel = (int)(RGB.R * 0.299 + RGB.G * 0.587 + RGB.B * 0.114);
                                    //int pixel = (int)(RGB);
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }

                            }
                        }
                        //Sobel Filter through Y axis
                        Gx = Math.Abs(Gx);
                        Gy = Math.Abs(Gy);
                        //https://ithelp.ithome.com.tw/articles/10214418
                        if (Gx < 0)
                            Gx = 0;
                        else if (Gx > 255)
                            Gx = 255;
                        if (Gy < 0)
                            Gy = 0;
                        else if (Gy > 255)
                            Gy = 255;
                        //we use both kernels to calculate the gradient horizontally and vertically. Then, we calculate the gradient magnitude using the following
                        int G = (int)Math.Sqrt( Math.Pow(Gx,2) + Math.Pow(Gy, 2) );
                        G = Math.Abs(G);
                        Console.WriteLine(G);

                        if (G < 0)
                            G = 0;
                        else if (G > 255)
                            G = 255;

                        sobel_combined_img.SetPixel(x, y, Color.FromArgb(G, G, G));
                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = sobel_combined_img;
                pictureBox2.Visible = true;
                function.Text = "sobel_combined";
                function.Visible = true;
                
            }
        }
        //Overlap
        private void overlap_button_Click(object sender, EventArgs e)
        {
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("overlap");
                    Console.WriteLine("overlap", num_function, num_step);
                }
                //建立圖片
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap overlap_img = new Bitmap(Img);
                Bitmap sobel_combined_img = new Bitmap(Img);
                int pixel;

                //Sobel edge
                int[] sobel_x = new int[9] { 1, 0, -1,
                                             2, 0, -2,
                                             1, 0, -1 };
                int[] sobel_y = new int[9] { 1, 2, 1,
                                             0, 0, 0,
                                            -1, -2, -1 };
                //Convolution : for each pixel
                for (int y = 0; y < Img.Height; y++)//行
                {
                    for (int x = 0; x < Img.Width; x++)//列
                    {
                        //sobel_x的結果
                        int Gx = 0;
                        int Gy = 0;

                        //pixel的9宮格
                        for (int j = -1; j <= 1; j++)//行
                        {
                            for (int i = -1; i <= 1; i++)//列
                            {
                                //外框
                                if (((x + i) < 0) || ((y + j) < 0) || ((x + i) >= Img.Width) || ((y + j) >= Img.Height))
                                {
                                    pixel = 0;
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }
                                //內部
                                else
                                {
                                    //每個pixel的RGB像素
                                    Color RGB = Img.GetPixel(x + i, y + j);
                                    //取Gray
                                    pixel = (int)(RGB.R * 0.299 + RGB.G * 0.587 + RGB.B * 0.114);
                                    //int pixel = (int)(RGB);
                                    Gx = Gx + pixel * sobel_x[(i + 1) + (j + 1) * 3];
                                    Gy = Gy + pixel * sobel_y[(i + 1) + (j + 1) * 3];
                                }

                            }
                        }
                        //Sobel Filter through Y axis
                        Gx = Math.Abs(Gx);
                        Gy = Math.Abs(Gy);
                        //https://ithelp.ithome.com.tw/articles/10214418
                        if (Gx < 0)
                            Gx = 0;
                        else if (Gx > 255)
                            Gx = 255;
                        if (Gy < 0)
                            Gy = 0;
                        else if (Gy > 255)
                            Gy = 255;
                        //we use both kernels to calculate the gradient horizontally and vertically. Then, we calculate the gradient magnitude using the following
                        int G = (int)Math.Sqrt(Math.Pow(Gx, 2) + Math.Pow(Gy, 2));
                        G = Math.Abs(G);
                        Console.WriteLine(G);

                        if (G < 0)
                            G = 0;
                        else if (G > 255)
                            G = 255;

                        sobel_combined_img.SetPixel(x, y, Color.FromArgb(G, G, G));
                    }
                }
                //threshold
                int overlap_value = 100;
                //檢查Threshold的值
                //string threshold_value = threshold_textBox.Text;
                try
                {
                    overlap_value = Int32.Parse(overlap_textbox.Text);
                    Console.WriteLine(overlap_value);
                }
                catch (FormatException)
                {
                    //若輸入的數值無法轉成整數失敗
                    MessageBox.Show("Your input number is unable top parse into integer! \n Please input integer");
                    Console.WriteLine($"Unable to parse '{overlap_textbox.Text}'");
                }

                //for each pixel
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color RGB = sobel_combined_img.GetPixel(x, y);
                        //取Gray
                        pixel = (int)( RGB.R * 0.299 + RGB.G * 0.587 + RGB.B * 0.114);
                        Console.WriteLine(pixel);

                        if (pixel > overlap_value)
                        {
                            overlap_img.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                        }
                        else
                        {
                            overlap_img.SetPixel(x, y, Img.GetPixel(x, y));
                        }
                        
                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = overlap_img;
                pictureBox2.Visible = true;
                function.Text = "overlap";
                function.Visible = true;

            }
        }

        //
        private void region_button_Click(object sender, EventArgs e)
        {
            Random crandom = new Random();
            clear();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                //若開啟成功
                //先將未處理的照片存入Stack
                if (pictureBox2.Image == null)
                {
                    Img_stack = new Bitmap(pictureBox1.Image);
                    Img_temp.Push(Img_stack);
                }
                else
                {
                    Img_stack = new Bitmap(pictureBox2.Image);
                    Img_temp.Push(Img_stack);
                    //做完存入Stack
                    num_step++;
                    step_num.Text = num_step + " step";
                    num_function.Push("sobel_combined");
                    Console.WriteLine("sobel combined", num_function, num_step);
                }
                //建立圖片
                Bitmap Img = new Bitmap(pictureBox1.Image);
                Bitmap Img_label = new Bitmap(pictureBox1.Image);
                Bitmap region_img = new Bitmap(Img);
                var text = new StringBuilder("");
                var text2 = new StringBuilder("");
                var text3 = new StringBuilder("");
                int label = 0;
                int same_compoment = 0;
                int total_compoment = 0;
                //inverse
                for (int y = 0; y < Img.Height; y++)//行
                {
                    for (int x = 0; x < Img.Width; x++)//列
                    {
                        if (Img.GetPixel(x, y).R == 255)
                        {
                            Img_label.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            Img_label.SetPixel(x, y, Color.FromArgb(1, 1, 1));
                        }
                    }
                }

                //Sequential algorithm 
                //Convolution : for each pixel 由左而右，由上而下
                List<List<int>> label_list = new List<List<int>>();
                int list_num = 0;
                bool list_is = false;
                for (int y = 0; y < Img_label.Height; y++)//行
                {

                    for (int x = 0; x < Img_label.Width; x++)//列
                    {
                        

                        int pixel_q = (y == 0 || x == 0) ? 0 : (int)(Img_label.GetPixel(x - 1, y - 1).R);
                        int pixel_r = (y == 0) ? 0 : (int)(Img_label.GetPixel(x, y - 1).R);
                        int pixel_s = (y == 0 || x == Img_label.Width - 1) ? 0 : (int)(Img_label.GetPixel(x + 1, y - 1).R);
                        int pixel_t = (x == 0) ? 0 : (int)(Img_label.GetPixel(x - 1, y).R);
                        int pixel_p = (int)(Img_label.GetPixel(x, y).R);

                        //1為有物體，則掃描下一點。                        
                        if (pixel_p == 1)
                        {
                            List<int> label_list_x = new List<int>();
                            //非邊界                            
                            //如果這四個方向的值都是0，那麼該位置就創建一個新的標號（在原標號上加1）；
                            if ((pixel_q + pixel_r + pixel_s + pixel_t) == 0)
                            {
                                label++;
                                /*
                                if (label >= 10)
                                    label = 1;
                                */
                                Img_label.SetPixel(x, y, Color.FromArgb(label, label, label));
                                //Img_label.SetPixel(x, y, Color.FromArgb(2, 2, 2));      

                            }
                            //如果這四個方向的非0值（即標號）都一樣，那麼該位置標號就是其領域的非0標號；
                            else if ((pixel_q == pixel_r) && (pixel_r == pixel_s) && (pixel_s == pixel_t) && (pixel_t == pixel_q))
                            {
                                Img_label.SetPixel(x, y, Color.FromArgb(pixel_q, pixel_q, pixel_q));
                                //Img_label.SetPixel(x, y, Color.FromArgb(3, 3, 3));
                            }
                            //如果這四個方向的非0值有兩個不同的標號，那麼該位置標號就選其中之一，並記錄這兩個不同的標號（因為這兩個標號是連通的，故視為等同的標號）；
                            else if ((pixel_q != 0) | (pixel_r != 0) | (pixel_s != 0) | (pixel_t != 0))
                            {
                                if ((pixel_q >= pixel_r) && (pixel_q >= pixel_s) && (pixel_q >= pixel_t))
                                {
                                    Img_label.SetPixel(x, y, Color.FromArgb(pixel_q, pixel_q, pixel_q));
                                    if (!(label_list_x.Contains(pixel_q)) && pixel_q != 0)
                                    {
                                        if (!label_list_x.Contains(pixel_q))
                                            label_list_x.Add(pixel_q);
                                        else
                                        {
                                            label_list.Add(label_list_x);
                                            label_list_x.Clear();
                                            label_list_x.Add(pixel_q);
                                        }
                                    }
                                    if (!(label_list_x.Contains(pixel_r)) && pixel_r != 0)
                                    {
                                        if (!label_list_x.Contains(pixel_r))
                                            label_list_x.Add(pixel_r);
                                        else
                                        {
                                            label_list.Add(label_list_x);
                                            label_list_x.Clear();
                                            label_list_x.Add(pixel_r);
                                        }
                                    }
                                    if (!(label_list_x.Contains(pixel_s)) && pixel_s != 0)
                                    {
                                        if (!label_list_x.Contains(pixel_s))
                                            label_list_x.Add(pixel_s);
                                        else
                                        {
                                            label_list.Add(label_list_x);
                                            label_list_x.Clear();
                                            label_list_x.Add(pixel_s);
                                        }
                                    }
                                    if (!(label_list_x.Contains(pixel_t)) && pixel_t != 0)
                                    {
                                        if (!label_list_x.Contains(pixel_t))
                                            label_list_x.Add(pixel_t);
                                        else
                                        {
                                            label_list.Add(label_list_x);
                                            label_list_x.Clear();
                                            label_list_x.Add(pixel_t);
                                        }
                                    }
                                }
                                else if ((pixel_r >= pixel_q) && (pixel_r >= pixel_s) && (pixel_r >= pixel_t))
                                {
                                    Img_label.SetPixel(x, y, Color.FromArgb(pixel_r, pixel_r, pixel_r));
                                    if (!(label_list_x.Contains(pixel_q)) && pixel_q != 0)
                                    {
                                        label_list_x.Add(pixel_q);
                                        
                                    }
                                    if (!(label_list_x.Contains(pixel_r)) && pixel_r != 0)
                                    {
                                        label_list_x.Add(pixel_r);
                                    }
                                    if (!(label_list_x.Contains(pixel_s)) && pixel_s != 0)
                                    {
                                        label_list_x.Add(pixel_s);
                                        
                                    }
                                    if (!(label_list_x.Contains(pixel_t)) && pixel_t != 0)
                                    {
                                        label_list_x.Add(pixel_t);
                                        
                                    }
                                }
                                else if ((pixel_s >= pixel_q) && (pixel_s >= pixel_r) && (pixel_s >= pixel_t))
                                {
                                    Img_label.SetPixel(x, y, Color.FromArgb(pixel_s, pixel_s, pixel_s));
                                    if (!(label_list_x.Contains(pixel_q)) && pixel_q != 0)
                                    {
                                        label_list_x.Add(pixel_q);

                                    }
                                    if (!(label_list_x.Contains(pixel_r)) && pixel_r != 0)
                                    {
                                        label_list_x.Add(pixel_r);
                                    }
                                    if (!(label_list_x.Contains(pixel_s)) && pixel_s != 0)
                                    {
                                        label_list_x.Add(pixel_s);

                                    }
                                    if (!(label_list_x.Contains(pixel_t)) && pixel_t != 0)
                                    {
                                        label_list_x.Add(pixel_t);

                                    }
                                }
                                else if ((pixel_t >= pixel_q) && (pixel_t >= pixel_r) && (pixel_t >= pixel_s))
                                {
                                    Img_label.SetPixel(x, y, Color.FromArgb(pixel_t, pixel_t, pixel_t));
                                    if (!(label_list_x.Contains(pixel_q)) && pixel_q != 0)
                                    {
                                        label_list_x.Add(pixel_q);
                                    }
                                    if (!(label_list_x.Contains(pixel_r)) && pixel_r != 0)
                                    {
                                        label_list_x.Add(pixel_r);
                                    }
                                    if (!(label_list_x.Contains(pixel_s)) && pixel_s != 0)
                                    {
                                        label_list_x.Add(pixel_s);

                                    }
                                    if (!(label_list_x.Contains(pixel_t)) && pixel_t != 0)
                                    {
                                        label_list_x.Add(pixel_t);

                                    }
                                }

                            }
                            /*
                            if (!label_list_x.Contains(list_num) && list_is == true)
                            {
                                label_list_x.Add(list_num);
                                list_is = false;
                            }
                            else if( label_list_x.Count() > 0 && list_is == true)
                            {
                                label_list.Add(label_list_x);
                                label_list_x.Clear();
                                label_list_x.Add(list_num);
                            }
                            */
                            if (label_list_x.Count > 1)
                            {
                                if (!label_list.Contains(label_list_x) )
                                {
                                    label_list.Add(label_list_x);
                                    //label_list_x.Clear();
                                }
                                label_list_x.Sort();
                                //Console.WriteLine(label_list.Count());
                                foreach (var list in label_list)
                                {                                    
                                    Console.WriteLine("{0} {1} ", list[0], list[1]);
                                }
                                //label_list.ForEach(i => Console.Write("{0},", i));

                            }
                            
                        }
                        else
                        {
                          ;
                        }
                        
                        //每個pixel的RGB像素
                        text.Append(Img_label.GetPixel(x, y).R.ToString() + ",");
                    }
                    text.Append("\r\n");                    
                }
                Console.WriteLine(" Total : {0} ",label);
                Console.WriteLine(label_list.Count());
                List<List<int>> label_list_map2 = new List<List<int>>();
                List<int> label_list_2 = new List<int>();
                int thrushold = 0;
                int max = 0;
                var last = label_list.Last();
                //label_list_map2 = label_list.Distinct().ToList();
                
                //label_list.Sort();
                foreach (var list in label_list)
                {
                    if (list.Equals(last))
                    {
                        // do something different with the last item
                        label_list_map2.Add(label_list_2);
                    }
                    if( thrushold > list[0])
                    {
                        continue;
                    }
                    if (label_list_2.Count == 0)
                    {
                        label_list_2.Add(list[0]);
                        label_list_2.Add(list[1]);
                        label_list_2.Sort();
                        //thrushold = list[0] > list[1] ? list[0] : list[1];
                        //same_compoment += 1;
                    }
                    else if ((!label_list_2.Contains(list[0])) && (!label_list_2.Contains(list[1])))
                    {                        
                        thrushold = label_list_2[0];
                        label_list_map2.Add(label_list_2);
                        //Console.WriteLine("{0} ", same_compoment);
                        //label_list_2.Clear();
                        label_list_2 = new List<int>();
                    }
                    else if (label_list_2.Contains(list[0]) && (!label_list_2.Contains(list[1])))
                    {
                        label_list_2.Add(list[1]);
                        //same_compoment += 1;
                        //label_list_2 = label_list_2.Distinct().ToList();
                    }
                    else if (!label_list_2.Contains(list[0]) && (label_list_2.Contains(list[1])))
                    {
                        label_list_2.Add(list[0]);
                        label_list_2.Sort();
                        //same_compoment += 1;
                        //label_list_2 = label_list_2.Distinct().ToList();
                    }
                    Console.WriteLine("{0}", thrushold);
                    Console.WriteLine("{0} {1} ", list[0], list[1]);
                }
                Console.WriteLine("{0} ", same_compoment);
                foreach (var list in label_list_map2)
                {
                    list.Sort();
                    foreach (var i in list)
                    {
                        max = max < i ? i : max;
                        Console.Write("{0} ", i);
                        same_compoment += 1;
                    }
                    Console.WriteLine("");
                    same_compoment -= 1;
                }
                Console.WriteLine("{0} ", same_compoment);
                Console.WriteLine("{0}", label_list_map2.Count());

                System.IO.File.WriteAllText(@"..\WriteLines.txt", text.ToString());

                int count_compoment = 0;

                //掃描第2次
                for (int y = 0; y < Img_label.Height; y++)//行
                {
                    for (int x = 0; x < Img_label.Width; x++)//列
                    {         
                        int pixel_p = (int)(Img_label.GetPixel(x, y).R);
                        //1為有物體，則掃描下一點。                        
                        if ( pixel_p != 0)
                        {
                            foreach (var list in label_list_map2)
                            {                                
                                if ( list.Contains(pixel_p) && pixel_p != list[0])
                                {    
                                    Img_label.SetPixel(x, y, Color.FromArgb(list[0], list[0], list[0]));
                                }
                                foreach (var i in list)
                                {                                   
                                    //Console.Write("{0} ", i);
                                }
                                //Console.WriteLine("");                                
                            }
                        }
                        else
                        {
                            ;
                        }
                        //每個pixel的RGB像素
                        if(Img_label.GetPixel(x, y).R < 10)
                            text2.Append(" "+Img_label.GetPixel(x, y).R.ToString() + ",");
                        text2.Append(" " + Img_label.GetPixel(x, y).R.ToString() + ",");
                    }
                    text2.Append("\r\n");
                }
                System.IO.File.WriteAllText(@"..\WriteLines2.txt", text2.ToString());

                List<int> color_map = new List<int>();
                Random rand1 = new Random(1);
                Random rand2 = new Random(100);
                Random rand3 = new Random(255);
                int pixel_color1 = 0;
                int pixel_color2 = 0;
                int pixel_color3 = 0;
                int pixel_temp = 0;


                //上色
                for (int y = 0; y < Img_label.Height; y++)//行
                {
                    for (int x = 0; x < Img_label.Width; x++)//列
                    {
                        int pixel_p = (int)(Img_label.GetPixel(x, y).R);
                        //!= 0 為有物體                        
                        if ( pixel_p != 0 && (!color_map.Contains(pixel_p)) )
                        {
                            pixel_color1 = rand1.Next(50 , 255);
                            pixel_color2 = rand2.Next(50 , 255);
                            pixel_color3 = rand3.Next(50 , 255);
                            color_map.Add(pixel_p);
                            color_map.Add(pixel_color1);
                            color_map.Add(pixel_color2);
                            color_map.Add(pixel_color3);
                            foreach (var i in color_map)
                            {
                                //Console.Write("{0} ,", i);
                            }

                            Img_label.SetPixel(x, y, Color.FromArgb(pixel_color1, pixel_color2, pixel_color3));
                            //pixel_temp = pixel_p;
                        }
                        else if( pixel_p != 0 && (color_map.Contains(pixel_p)) )
                        {
                            int location;
                            location = color_map.IndexOf(pixel_p);
                            Img_label.SetPixel(x, y, Color.FromArgb(color_map[location + 1], color_map[location + 2], color_map[location + 3]));
                            ;
                        }
                        //每個pixel的RGB像素
                        text3.Append(Img_label.GetPixel(x, y).R.ToString() + ",");
                    }
                    text3.Append("\r\n");
                }


                //反轉白色
                for (int y = 0; y < Img_label.Height; y++)//行
                {
                    for (int x = 0; x < Img_label.Width; x++)//列
                    {
                        int pixel_p = (int)(Img_label.GetPixel(x, y).R);
                        //!= 0 為有物體                        
                        if ( pixel_p == 0 )
                        {                            
                            Img_label.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                        else 
                        {
                            
                        }
                    }
                }
                Console.WriteLine("一開始總區塊: {0}",max);
                Console.WriteLine("重疊區塊: {0}", same_compoment);
                Console.WriteLine("總共區塊: {0}", max - same_compoment);

                compement_label.Text = (max - same_compoment).ToString();

                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Source";

                pictureBox2.Image = Img_label;
                pictureBox2.Visible = true;
                function.Text = "sobel_combined";
                function.Visible = true;
                compement_label.Visible = true;

            }                       
            

        }
        
        private void Write(int number)
        {
            System.IO.FileStream fs = new System.IO.FileStream("..\\test.txt", System.IO.FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            //開始寫入
            sw.Write(number);
            //清空緩衝區
            sw.Flush();
            //關閉流
            sw.Close();
            fs.Close();
        }
        //Rotate
        private void rotation_button_Click(object sender, EventArgs e)
        {
            int angle = int.Parse(rotate_angel_textBox.Text);
            double rad = angle * Math.PI / 180;
            double sin = Math.Sin(rad);
            double cos = Math.Cos(rad);

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                Bitmap img = new Bitmap(pictureBox1.Image);
                int rot_w = (int)(img.Width * Math.Abs(cos) + img.Height * Math.Abs(sin));
                int rot_h = (int)(img.Height * Math.Abs(cos) + img.Width * Math.Abs(sin));

                Bitmap rotate_img = new Bitmap(rot_w, rot_h);
                int ori_pad_x = img.Width / 2;
                int ori_pad_y = img.Height / 2;
                int rot_pad_x = rotate_img.Width / 2;
                int rot_pad_y = rotate_img.Height / 2;
                for (int y = 0; y < rotate_img.Height; y++)
                {
                    for (int x = 0; x < rotate_img.Width; x++)
                    {
                        int ori_x = (int)((x - rot_pad_x) * cos + (y - rot_pad_y) * sin + 0.5) + ori_pad_x;
                        int ori_y = (int)((y - rot_pad_y) * cos + (x - rot_pad_x) * (-sin) + 0.5) + ori_pad_y;
                        if (0 < ori_x && ori_x < img.Width && 0 < ori_y && ori_y < img.Height)
                            rotate_img.SetPixel(x, y, img.GetPixel(ori_x, ori_y));

                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;
                pictureBox2.Image = rotate_img;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Visible = true;
                

            }

        }

        private void Scaling_button_Click(object sender, EventArgs e)
        {
            float scale_x = float.Parse(scale_x_textBox.Text);
            float scale_y = float.Parse(scale_y_textBox.Text);

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("There is no image chosen!");
            }
            else
            {
                Bitmap img = new Bitmap(pictureBox1.Image);
                int scale_w = (int)(img.Width * scale_x + 0.5);
                int scale_h = (int)(img.Height * scale_y + 0.5);
                Bitmap scale_img = new Bitmap(scale_w, scale_h);
                for (int y = 0; y < scale_img.Height; y++)
                {
                    for (int x = 0; x < scale_img.Width; x++)
                    {
                        int ori_x = (int)(x / scale_x);
                        int ori_y = (int)(y / scale_y);
                        scale_img.SetPixel(x, y, img.GetPixel(ori_x, ori_y));
                    }
                }

                pictureBox1.Visible = true;
                label1.Visible = true;
                pictureBox2.Image = scale_img;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Visible = true;      
                
            }
        }

        
    }
}
