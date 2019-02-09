using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Problem1
{
    public partial class Form1 : Form
    {
        private Point startPoint, endPoint, startRPoint;
        bool modeRectangle = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpeg files (*.jpg)|*.jpg|(*.gif)|gif||";
            if (DialogResult.OK == dialog.ShowDialog())
            {
                this.picOrigImage.Image = new Bitmap(dialog.FileName);
                FileInfo finfo = new FileInfo(dialog.FileName);
                lblImageUnderTest.Text = "Image Under Test\n " +
                    finfo.Name;
                txtWidthOrig.Text = picOrigImage.Image.Width.ToString();
                txtHeightOrig.Text = picOrigImage.Image.Height.ToString();
            }
        }

        private void btnConvertToGray_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap copy = ImageProcessing.MConvertToGray((Bitmap)this.picOrigImage.Image);
                picProcImage.Image = null;
                picProcImage.Image = copy;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRotateFillImage_Click(object sender, EventArgs e)
        {
            try
            {
                FormRotate dlg = new FormRotate();
                dlg.RotationAngle = 0;
                Image origimg = picOrigImage.Image;
                Bitmap bmp = new Bitmap(origimg);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    ImageProcessing.MRotateFill(origimg, ref bmp, dlg.RotationAngle);
                    picProcImage.Image = null;
                    picProcImage.Image = bmp;
                    txtWidthProc.Text = picProcImage.Image.Width.ToString();
                    txtHeightProc.Text = picProcImage.Image.Height.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCropAroundMouseClick_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = this.picOrigImage.Image;
                Bitmap bmp = new Bitmap(img);
                Point p1 = new Point(startPoint.X - int.Parse(txtCropAroundW.Text) / 2,
                    startPoint.Y - int.Parse(txtCropAroundH.Text) / 2);
                Rectangle rect = new Rectangle(p1, new Size(int.Parse(txtCropAroundW.Text),
                    int.Parse(txtCropAroundH.Text)));

                bmp = new Bitmap(img);
                ImageProcessing.MCropImage(img, ref bmp, rect);
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void picOrigImage_MouseDown(object sender, MouseEventArgs e)
        {
            modeRectangle = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void picOrigImage_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint = new Point(e.X, e.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCropAroundW.Text = "14";
            txtCropAroundH.Text = "14";  // 14 x14 window to crop around
        }

        private void btnBrightenImage_Click(object sender, EventArgs e)
        {
            try
            {
                FormBrightness dlg = new FormBrightness();
                dlg.Brightness = 30;  // default brightness of 30

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    Bitmap copy = new Bitmap((Bitmap)this.picOrigImage.Image);
                    copy = ImageProcessing.MBrighten(copy, dlg.Brightness);

                    picProcImage.Image = null;
                    picProcImage.Image = copy;
                    txtWidthProc.Text = picProcImage.Image.Width.ToString();
                    txtHeightProc.Text = picProcImage.Image.Height.ToString();
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeContrast_Click(object sender, EventArgs e)
        {
            try
            {
                FormContrast dlg = new FormContrast();
                dlg.Contrast = 0;

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    Bitmap copy = new Bitmap((Bitmap)this.picOrigImage.Image);
                    copy = ImageProcessing.MContrast(copy, (sbyte)dlg.Contrast);

                    picProcImage.Image = null;
                    picProcImage.Image = copy;
                    txtWidthProc.Text = picProcImage.Image.Width.ToString();
                    txtHeightProc.Text = picProcImage.Image.Height.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveProcessedImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "jpeg files (*.jpg)|*.jpg";
            if (DialogResult.OK == dlg.ShowDialog())
                this.picProcImage.Image.Save(dlg.FileName, ImageFormat.Jpeg);
        }

        private void btnResizeImage_Click(object sender, EventArgs e)
        {
            if (modeRectangle == true)
            {
                modeRectangle = false;
                Rectangle rect = new Rectangle(new Point(0, 0), new Size(endPoint.X, endPoint.Y));
                Image img = this.picOrigImage.Image;
                Bitmap bmp = new Bitmap(img);
                ImageProcessing.MResizeImage(img, ref bmp, rect);
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
            else
                MessageBox.Show("you must click mouse to select bottom of rectangle");
        }

        private void btnResizeProportional_Click(object sender, EventArgs e)
        {
            try
            {
                modeRectangle = false;
                int Width = int.Parse(txtNewSizeW.Text);
                int Height = int.Parse(txtNewSizeH.Text);
                Rectangle rect = new Rectangle(new Point(0, 0), new Size(Width, Height));
                Image img = this.picOrigImage.Image;
                Bitmap bmp = new Bitmap(img);
                ImageProcessing.MResizeImageProportional(img, ref bmp, rect);
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCropAroundDragAndReleaseMouse_Click(object sender, EventArgs e)
        {
            try
            {
                if (modeRectangle == true)
                {
                    modeRectangle = false;
                    Rectangle rect = new Rectangle(startPoint, new Size(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y));
                    Image img = this.picOrigImage.Image;
                    Bitmap bmp = new Bitmap(img);
                    ImageProcessing.MCropImage(img, ref bmp, rect);
                    picProcImage.Image = bmp;
                    txtWidthProc.Text = picProcImage.Image.Width.ToString();
                    txtHeightProc.Text = picProcImage.Image.Height.ToString();
                }
                else
                    MessageBox.Show("you must click mouse to select bottom of rectangle");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDraRectangle_Click(object sender, EventArgs e)
        {
            if (modeRectangle == true)
            {
                modeRectangle = false;
                Rectangle rect = new Rectangle(startPoint, new Size(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y));
                Image img = this.picOrigImage.Image;
                Bitmap bmp = new Bitmap(img);

                ImageProcessing.MDrawRectangle(img, ref bmp, rect);
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
            else
                MessageBox.Show("you must drag mouse to select rectangle");
        }

        private void btnRotateClear_Click(object sender, EventArgs e)
        {
            FormRotate frmRotate = new FormRotate();
            frmRotate.RotationAngle = 0;
            Image origimg = picOrigImage.Image;
            Bitmap bmp = new Bitmap(origimg);
            if (DialogResult.OK == frmRotate.ShowDialog())
            {
                ImageProcessing.MRotateClear(origimg, ref bmp, frmRotate.RotationAngle);
                picProcImage.Image = null;
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }

        }

        private void btnDrawX_Click(object sender, EventArgs e)
        {
            Image origimg = picOrigImage.Image;
            Bitmap bmp = new Bitmap(origimg);
            ImageProcessing.MDrawX(origimg, ref bmp, startPoint);
            picProcImage.Image = null;
            picProcImage.Image = bmp;
        }

        private void btnRotateByPoints_Click(object sender, EventArgs e)
        {
            Image origimg = picOrigImage.Image;
            Bitmap bmp = new Bitmap(origimg);
            if (modeRectangle == true)
            {
                ImageProcessing.MRotateByPoints(origimg, ref bmp, startPoint, endPoint);
                picProcImage.Image = null;
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
            else
                MessageBox.Show("you must click mouse to select bottom of rectangle");
        }

        private void btnRotateFill_Click(object sender, EventArgs e)
        {
            FormRotate dlg = new FormRotate();
            dlg.RotationAngle = 0;
            Image origimg = picOrigImage.Image;
            Bitmap bmp = new Bitmap(origimg);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                ImageProcessing.MRotateFill(origimg, ref bmp, dlg.RotationAngle);
                picProcImage.Image = null;
                picProcImage.Image = bmp;
                txtWidthProc.Text = picProcImage.Image.Width.ToString();
                txtHeightProc.Text = picProcImage.Image.Height.ToString();
            }
        }

        private void btnShiftHorizontal_Click(object sender, EventArgs e)
        {
            FormShift dlg = new FormShift();
            dlg.ShiftAmount = 0;
            Image origimg = picOrigImage.Image;
            Bitmap bmp = new Bitmap(origimg);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (ImageProcessing.MShiftImageHorizontal(origimg, ref bmp, dlg.ShiftAmount))
                {
                    picProcImage.Image = null;
                    picProcImage.Image = bmp;
                    txtWidthProc.Text = picProcImage.Image.Width.ToString();
                    txtHeightProc.Text = picProcImage.Image.Height.ToString();
                 }
            }
        }

        private void btnShiftVertical_Click(object sender, EventArgs e)
        {
            FormShift dlg = new FormShift();
            dlg.ShiftAmount = 0;
            Image origimg = picOrigImage.Image;
            Bitmap bmp = new Bitmap(origimg);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (ImageProcessing.MShiftImageVertical(origimg, ref bmp, dlg.ShiftAmount))
                {
                    picProcImage.Image = null;
                    picProcImage.Image = bmp;
                    txtWidthProc.Text = picProcImage.Image.Width.ToString();
                    txtHeightProc.Text = picProcImage.Image.Height.ToString();
                 }
            }
        }
    }
}
